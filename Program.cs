using System.Security.Cryptography.X509Certificates;

namespace uebungDelegate
{
    // Delegatendefinition für das Guthaben-Event
    public delegate void GuthabenEventHandler(object sender, GuthabenEventArgs e);

    //EventArgs
    public class GuthabenEventArgs : EventArgs 
    {
        public int Änderung { get; } 
        public GuthabenEventArgs(int änderung) 
        { 
            Änderung = änderung; 
        } 
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Erstellen einer Instanz der Testklasse
            testKlasse tk1 = new testKlasse();

            // Abonnieren des Abheben-Events mit der Methode AbhebenMethod
            tk1.Abheben += AbhebenMethode;      

            Console.WriteLine(tk1.Wert);
            tk1.Wert = 99;
            Console.WriteLine(tk1.Wert);
            tk1.Wert = -155;
            Console.WriteLine(tk1.Wert);
            Console.ReadLine();
        }
        // Event-Handler-Methode, die aufgerufen wird, wenn das Abheben-Event ausgelöst wird
        public static void AbhebenMethode(object sender, GuthabenEventArgs e)
        {
            Console.WriteLine($"Betrag hat sich geändert. {e.Änderung}");
        }
    }
    internal class testKlasse
    {
        // Event-Definition in der Testklasse
        public event GuthabenEventHandler Abheben;
        private int _wert;
        public int Wert
        {
            get { return _wert; }
            set
            {
                if (value != 0)
                {
                    // Auslösen des Abheben-Events, wenn sich der Wert ändert
                    Abheben?.Invoke(this, new GuthabenEventArgs(value));
                  //Abheben = Event aus testKlasse, wird in Main von AbhebenMethode abonniert
                }
                _wert += value;
            }
        }
    }
}

