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

        List<string> Zaznami = new List<string>();

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

                //Ceka na stisknuti klavesy
                if (Console.KeyAvailable)
                {
                    //nacte stisknutou klavesu
                    ConsoleKeyInfo volba = Console.ReadKey();
                    //pokud je klavesa K
                    if (volba.Key == ConsoleKey.K)
                    {
                        Kolo(aktualniCas);
                    }
                }
            }
        }

        //udelá zaznam o kole + pridá záznam do historie a vypíše max 5 zaznamu
        public void Kolo(string aktualniCas)
        {
            //prida zaznam do seznamu nedavnych zaznamu
            Zaznami.Add(aktualniCas);
            //nastavi startovni hodnotu kurzoru pro nedavnou historii
            int poziceKurzoruVHistorii = 2;
            //dynamicky projede seznam od zadu
            for (int i = Zaznami.Count - 1; i >= 0; i--)
            {
                //nastavi startovni pozici kurzoru
                Console.SetCursorPosition(0, poziceKurzoruVHistorii);
                //pridava zaznami odshora 
                if (poziceKurzoruVHistorii <= 5)
                {
                    Console.WriteLine(Zaznami[i]);
                    poziceKurzoruVHistorii++;
                }
                else
                {
                    //pokud pocet zaznamu presahne 5 ukonci projizdeni seznamu
                    Console.WriteLine(Zaznami[i]);
                    poziceKurzoruVHistorii = 2;
                    i = 0;
                }
            }
        }

    }
}
