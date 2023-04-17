using System.Linq;

namespace csharp_gestore_eventi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Evento nuovoEvento = CreaEvento();       

        }
        public static Evento CreaEvento()
        {
            //titolo evento
            Console.WriteLine("Inserisci il nome dell'evento: ");
            string title = Console.ReadLine();
            while(title == "" || title == null)
                Console.WriteLine("Inserisci un titolo valido");

            //data evento
            Console.WriteLine("Indica la data dell'evento (gg/MM/yyyy): ");
            DateTime date;
            while (!DateTime.TryParse(Console.ReadLine(), out date))
                Console.WriteLine("Inserisci una data valida nel formato indicato");
            string eventDate = date.ToString();

            //posti evento
            Console.WriteLine("Indica la capienza di posti dell'evento: ");
            int capacity;
            while (!int.TryParse(Console.ReadLine(), out capacity))
                Console.WriteLine("Inserisci un numero valido");

            //prenotazioni evento
            Console.WriteLine("Indica quanti posti desideri prenotare: ");
            int reserved;
            while (!int.TryParse(Console.ReadLine(), out reserved))
                Console.WriteLine("Inserisci un numero valido");


            //creazione evento
            Evento nomeEvento = new Evento(title, eventDate);
            nomeEvento.MaxCapacity = capacity;
            nomeEvento.Reserved = reserved;

            nomeEvento.StampaPosti();
            string postiLoop = "si";
            
            
            while(postiLoop == "si")
            {
                Console.WriteLine("Vuoi disdire dei posti(si/no)?");

                postiLoop = Console.ReadLine();
                if (postiLoop == "no")
                    break;
                while (postiLoop != "si" && postiLoop != "no")
                    Console.WriteLine("Comando non valido");
                nomeEvento.DisdiciPosti();
                
            }


            return nomeEvento;


        }

    }
    public class Evento
    {
        private string _titolo;
        public string Titolo
        {
            get { return _titolo; }
            set
            {
                if (value != "")
                    _titolo = value;
                else
                    throw new Exception("Titolo non può essere vuoto");
            }
        }
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                DateTime localTime = DateTime.Now;
                if (value > localTime)
                    _date = value;
                else
                    throw new Exception("un evento non può essere programmato nel passato");
            }
        }

        private int _maxCapacity;
        public int MaxCapacity
        {
            get { return _maxCapacity; }
            set
            {
                if (value > 0)
                    _maxCapacity = value;
                else
                    throw new Exception("I posti non possono essere meno di 0");
            }
        }
        private int _reserved;
        public int Reserved
        {
            get { return _reserved; }
            set
            {
                if (value > _maxCapacity)
                    throw new Exception("non si possono prenotare più posti di quelli disponibili");
                else
                    _reserved = value;
            }
        }

        public Evento(string titolo, string date)
        {
            DateTime dateEvento;
            while (!DateTime.TryParse(date, out dateEvento))
                Console.WriteLine("Data non valida");
            this.Titolo = titolo;
            this.Date = dateEvento;
            this.MaxCapacity = 150;
            this.Reserved = 0;
        }
        public int PrenotaPosti()
        {
            Console.Write($" Posti disponibili: {this.MaxCapacity - this.Reserved} - Inserisci il numero di posti che desideri prenotare: ");
            int posti;
            while (!int.TryParse(Console.ReadLine(), out posti))
                Console.WriteLine("Inserisci un numero valido");
            while (posti > this.MaxCapacity)
                Console.WriteLine("Non puoi prenotare più posti di quelli disponibili");
            return this.Reserved += posti;
        }
        public int DisdiciPosti()
        {
            Console.Write("Indica il numero di prenotazioni da cancellare: ");

            int posti;
            while (!int.TryParse(Console.ReadLine(), out posti))
                Console.WriteLine("Inserisci un numero valido");
            while (posti > this.Reserved)
                Console.WriteLine("Non puoi cancellare più prenotazioni di quelle attualmente attive");

            Console.WriteLine($"Posti disponibili: {this.MaxCapacity - (this.Reserved - posti)}");
            Console.WriteLine($"Posti prenotati: {this.Reserved - posti}");
            
            
            return this.Reserved -= posti;
        }

        public override string ToString()
        {
            string date = this.Date.ToString("dd/MM/yyyy");
            return date + " " + this.Titolo;
        }

        public void StampaPosti()
        {
            Console.WriteLine($"Posti a sedere: {this.MaxCapacity}");
            Console.WriteLine($"Posti prenotati: {this.Reserved}");
            Console.WriteLine($"Posti Liberi: {this.MaxCapacity - this.Reserved}");
        }
    }
    public class ProgrammaEventi
    {
        private string _titolo;
        public string Titolo
        {
            get { return _titolo; }
            set 
            {
                if (value != null && value != "")
                    _titolo = value;
                else
                    throw new Exception("Devi NECESSARIAMENTE specificare il titolo");
            }
        }

        public List<Evento> ListaEventi { get; set; }

        public ProgrammaEventi(string titolo) 
        { 
            _titolo = titolo;
            ListaEventi = new List<Evento>();
        }

        public void AddEvento(Evento ev)
        {
            this.ListaEventi.Add(ev);
        }
        public void EventoPerData()
        {
            Console.WriteLine("Digita una data (gg/MM/yyyy) per visualizzare tutti gli eventi previsti per quella data: ");
            
            //parametri ricerca
            DateTime dateFilter;
            DateTime todayDate = DateTime.Now;
            while( DateTime.TryParse(Console.ReadLine(), out dateFilter))
                Console.WriteLine("Digita una data nel formato valido");
            
            //stampa a schermo eventi
            foreach (Evento ev in ListaEventi)
            {
                if(ev.Date == dateFilter)
                    Console.WriteLine($"Evento {ListaEventi.IndexOf(ev)} - {ev.ToString()}");
            }
        }
        public static void StampaTuttiEventiStatic(List<Evento> ListaEventi)
        {
            foreach(Evento ev in ListaEventi)
                Console.WriteLine($"Evento {ListaEventi.IndexOf(ev)} - {ev.Titolo} - {ev.Date}");
        }
        public void EventiAttuali()
        {
            DateTime today = DateTime.Now;

            var eventoAttuale = from evento in ListaEventi 
                                         where evento.Date == today
                                         select evento;
            foreach (var evat in eventoAttuale)
            {
                evat.ToString();
            }
            
        }

        public void StampaTuttiEventi()
        {
            Console.WriteLine(this.Titolo);
            foreach (Evento ev in ListaEventi)
                Console.WriteLine($"Evento {ListaEventi.IndexOf(ev)} - {ev.ToString()}");
        }

        public void SvuotaLista()
        {
            this.ListaEventi.Clear();
        }

    }

}