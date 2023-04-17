namespace csharp_gestore_eventi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Evento evento1 = new Evento("Calcestruzzo", "18/04/2023");
            Console.WriteLine(evento1.ToString());
            evento1.PrenotaPosti();
            evento1.DisdiciPosti();            
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

            public Evento(string titolo, string date){
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
                while(posti > this.MaxCapacity)
                    Console.WriteLine("Non puoi prenotare più posti di quelli disponibili");

                return this.Reserved += posti;
            }
            public int DisdiciPosti()
            {
                Console.WriteLine($"Posti disponibili: {this.MaxCapacity - this.Reserved} - Posti prenotati: {this.Reserved}");
                Console.Write("Indica il numero di prenotazioni da cancellare: ");
                int posti;
                while (!int.TryParse(Console.ReadLine(), out posti))
                    Console.WriteLine("Inserisci un numero valido");
                while (posti > this.Reserved)
                    Console.WriteLine("Non puoi cancellare più prenotazioni di quelle attualmente attive");
                return this.Reserved -= posti;
            }

            public override string ToString()
            {
                string date = this.Date.ToString("dd/MM/yyyy");
                return date + " " + this.Titolo;
            }
        }
    }
}