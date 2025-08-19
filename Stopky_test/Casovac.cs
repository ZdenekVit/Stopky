using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Stopky_test
{
    internal class Casovac
    {
        DateTime zacatek;
        TimeSpan konec;
        //Konstruktor
        public Casovac() 
        {
        }

        //Spustí se casomira
        public void CasStart() 
        {
            //Ziska cas pri zmacknuti Start
            zacatek = DateTime.Now;

            while(true)
            {
                //Nastaví pozici casomiri na prvni radek
                Console.SetCursorPosition(0, 0);
                //Získá aktualni cas
                konec = (DateTime.Now - zacatek);
                //Preformatuje datovy typ TimeSpan na String a zkrati presnost milisekund na 2 desetina cisla
                string aktualniCas = string.Format("{0:00}:{1:00}:{2:00}:{3:00}",
                (int)konec.TotalHours, konec.Minutes, konec.Seconds, konec.Milliseconds / 10);
                //Vypise cas
                Console.Write($"\r   {aktualniCas}   ");
            }
        }

    }
}
