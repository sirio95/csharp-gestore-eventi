using System;
using System.Globalization;
using System.Linq;

namespace csharp_gestore_eventi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //creazione programma
            ProgrammaEventi nuovoEvento = CreaProgrammaEventi();

            //numero eventi in programma
            Console.WriteLine($"In totale, il programma prevede {nuovoEvento.ListaEventi.Count()} eventi");

            // tutti gli eventi in programma
            ProgrammaEventi.StampaTuttiEventiStatic(nuovoEvento.ListaEventi);

            //tutti gli eventi in programma in una data chiesta all'utente
            ProgrammaEventi.StampaTuttiEventiStatic(nuovoEvento.ListaEventoPerData());

            //svuotamento lista eventi
            nuovoEvento.SvuotaLista();



            //dati di debug
            //Evento evento1 = new Evento("Coffee Vincit Omnia", "29/05/2023");
            //Evento evento2 = new Evento("Coffee Caput Mundi", "29/05/2023");
            //Evento evento3 = new Evento("Coffee Stupor Mundi", "30/05/2023");
            //Evento evento4 = new Evento("Coffee In Anno Licere", "30/05/2023");

            //ProgrammaEventi CoffeeWorldCup = new ProgrammaEventi("Coffee World Cup");
            //CoffeeWorldCup.AddEvento(evento1);
            //CoffeeWorldCup.AddEvento(evento2);
            //CoffeeWorldCup.AddEvento(evento3);
            //CoffeeWorldCup.AddEvento(evento4);

            //CoffeeWorldCup.EventoPerData();

        }
        public static ProgrammaEventi CreaProgrammaEventi()
        {
            //titolo programma
            Console.WriteLine("Come vuoi che si chiami il tuo programma?");
            string title = Console.ReadLine();
            while (title == "" || title == null)
            {
                Console.WriteLine("Inserisci un titolo valido");
                title = Console.ReadLine();
            }

            //creazione programma
            ProgrammaEventi nuovoProgramma = new ProgrammaEventi(title);
            Console.WriteLine($"Di quanti eventi si comporrà {title}?");

            //inserimento eventi per programma
            int numEventi;
            while (!int.TryParse(Console.ReadLine(), out numEventi) || numEventi <= 0)
                Console.WriteLine("inserisci un numero valido e ricorda: un programma non può avere meno di zero eventi.");

            for (int i = 0; i < numEventi; i++)
            {
                Evento nuovoEvento = CreaEvento();
                nuovoProgramma.AddEvento(nuovoEvento);
            }


            // aggiunta conferenze

            Console.WriteLine("Vuoi inserire delle conferenze? (si/no)");

            string conferenza = Console.ReadLine();
            while ((conferenza == "" || conferenza == null) || (conferenza != "si" && conferenza != "no"))
            {
                Console.WriteLine("Rispsta non valida");
                conferenza = Console.ReadLine();
            }
            if (conferenza == "no") 
            {
                return nuovoProgramma;
            }
            else
            {
                Console.WriteLine("Indica il numero di conferenze da inserire in programma");
                int conferenze;
                while(!int.TryParse(Console.ReadLine(), out conferenze) || conferenze <0)
                    Console.WriteLine("Inserisci un numero valido. Ricorda, non puoi creare un numero negativo di conferenze");

                for (int i=0; i< conferenze; i++)
                {
                    Conferenza nuovaConferenza = CreaConferenza();
                    nuovoProgramma.AddEvento(nuovaConferenza);
                }
                return nuovoProgramma;
            }
        }
        public static Evento CreaEvento()
        {
            //titolo evento
            Console.WriteLine("Inserisci il nome dell'evento: ");
            string title = Console.ReadLine();
            while(title == "" || title == null)
            {
                Console.WriteLine("Inserisci un titolo valido");
                title = Console.ReadLine();
            }
                

            //data evento
            Console.WriteLine("Indica la data dell'evento (gg/MM/yyyy): ");
            DateTime date;
            while (!DateTime.TryParse(Console.ReadLine(), out date) || date<DateTime.Now)
                Console.WriteLine("Inserisci una data valida nel formato indicato (date passate non sono valide)");
            string eventDate = date.ToString();

            //posti evento
            Console.WriteLine("Indica la capienza di posti dell'evento: ");
            int capacity;
            while (!int.TryParse(Console.ReadLine(), out capacity))
                Console.WriteLine("Inserisci un numero valido");

            //prenotazioni evento
            Console.WriteLine("Indica quanti posti desideri prenotare: ");
            int reserved;
            while (!int.TryParse(Console.ReadLine(), out reserved) || reserved>capacity)
                Console.WriteLine("Inserisci un numero valido (ricorda: non puoi prenotare più posti di quanti non ne siano disponibili)");


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

        public static Conferenza CreaConferenza()
        {
            Console.WriteLine("Inserisci il nome della conferenza: ");
            string title = Console.ReadLine();
            while (title == "" || title == null)
            {
                Console.WriteLine("Inserisci un titolo valido");
                title = Console.ReadLine();
            }


            //data conferenza
            Console.WriteLine("Indica la data della conferenza (gg/MM/yyyy): ");
            DateTime date;
            while (!DateTime.TryParse(Console.ReadLine(), out date) || date < DateTime.Now)
                Console.WriteLine("Inserisci una data valida nel formato indicato (date passate non sono valide)");
            string conferenceDate = date.ToString();

            //posti conferenza
            Console.WriteLine("Indica la capienza di posti della conferenza: ");
            int capacity;
            while (!int.TryParse(Console.ReadLine(), out capacity))
                Console.WriteLine("Inserisci un numero valido");

            //prenotazioni conferenza
            Console.WriteLine("Indica quanti posti desideri prenotare: ");
            int reserved;
            while (!int.TryParse(Console.ReadLine(), out reserved) || reserved > capacity)
                Console.WriteLine("Inserisci un numero valido (ricorda: non puoi prenotare più posti di quanti non ne siano disponibili)");

            // dati conferenza

            Console.WriteLine("Indica il relatore della conferenza");
            string relatore = Console.ReadLine();
            while (relatore == "" || relatore == null || !relatore.Contains(" "))
            {
                Console.WriteLine("Inserisci un relatore valido. Ricorda di indicare sia nome che cognome");
                relatore = Console.ReadLine();
            }
            double price;
            while (!double.TryParse(Console.ReadLine(), out price))
                Console.WriteLine("Inserisci un prezzo valido");

            //creazione conferenza

            Conferenza nuovaConferenza = new Conferenza(relatore, price, title, conferenceDate);
            nuovaConferenza.MaxCapacity = capacity;
            nuovaConferenza.Reserved = reserved;

            nuovaConferenza.StampaPosti();
            string postiLoop = "si";

            // modifica prenotazioni conferenza

            while (postiLoop == "si")
            {
                Console.WriteLine("Vuoi disdire dei posti(si/no)?");

                postiLoop = Console.ReadLine();
                if (postiLoop == "no")
                    break;
                while (postiLoop != "si" && postiLoop != "no")
                    Console.WriteLine("Comando non valido");
                nuovaConferenza.DisdiciPosti();

            }
            return nuovaConferenza;
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
            while (!int.TryParse(Console.ReadLine(), out posti) || posti > this.Reserved)
                Console.WriteLine("Inserisci un numero valido (e ricorda: non puoi cancellare più prenotazioni di quelle attive)");

            Console.WriteLine($"Posti disponibili: {this.MaxCapacity - (this.Reserved - posti)}");
            Console.WriteLine($"Posti prenotati: {this.Reserved - posti}");
            
            
            return this.Reserved -= posti;
        }

        public override string ToString()
        {
            string date = this.Date.ToString("dd/MM/yyyy");
            return date + " - " + this.Titolo;
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
        public List<Evento> ListaEventoPerData()
        {
            Console.WriteLine("Digita una data (gg/MM/yyyy) per visualizzare tutti gli eventi previsti per quella data: ");
            
            List<Evento> ListaPerData = new List<Evento>();

            //parametri ricerca
            DateTime dateFilter;
            
            while (!DateTime.TryParse(Console.ReadLine(), out dateFilter))
                Console.WriteLine("Digita una data nel formato valido");

            
            //filtraggio eventi per data
            var eventoPerData = from evento in ListaEventi
                                where evento.Date == dateFilter
                                select evento;

            if(eventoPerData.Count() == 0)
            {
                Console.WriteLine($"Ci dispiace ma non ci sono eventi il {dateFilter.ToString()}");
            }
            else
            {
                foreach (var evat in eventoPerData)
                {
                    ListaPerData.Add(evat);
                }
            }
            return ListaPerData;

        }
        public static void StampaTuttiEventiStatic(List<Evento> ListaEventi)
        {
            foreach(Evento ev in ListaEventi)
                Console.WriteLine($"Evento {ListaEventi.IndexOf(ev) + 1} - {ev.ToString()}");
        }
        public void EventiAttuali()
        {
            DateTime today = DateTime.Now;

            var eventoAttuale = from evento in ListaEventi 
                                         where evento.Date == today
                                         select evento;

            if(eventoAttuale== null)
            {
                Console.WriteLine($"Ci dispiace ma nessuno evento è previsto per {today.ToString()}");
            }
            else
            {
                foreach (var evat in eventoAttuale)
                {
                    Console.WriteLine(evat.ToString());
                }
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

    public class Conferenza : Evento
    {
        public string Relatore { get; set; }


        //manipolazione prezzo con due tipi (string uscita, double ingresso)
        private double _prezzo;
        public string GetPrezzo()
        {
            string prezzoEuro = this._prezzo.ToString("0.00") + " " + "euro";
            return prezzoEuro;
        }
        public void SetPrezzo(double price)
        {
            this._prezzo = price;
        }

        public Conferenza(string realtore, double price, string titolo, string date ) : base(titolo, date)
        {
            this.Relatore = realtore;
            SetPrezzo(price);
        }

        public override string ToString()
        {
            string date = this.Date.ToString("dd/MM/yyyy");
            return date + " - " + this.Titolo + " - " + this.Relatore + " - " + this.GetPrezzo();
        }
    }
}