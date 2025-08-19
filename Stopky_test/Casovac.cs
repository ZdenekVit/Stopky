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
        TimeSpan nula = DateTime.Now - DateTime.Now; 

        //list docasne drzíci zaznami
        List<TimeSpan> Zaznami = new List<TimeSpan>();
        //list docasne drzíci zaznami mezicasu
        List<TimeSpan> ZaznamiMeziCasu = new List<TimeSpan>();
        //Konstruktor
        public Casovac() 
        {
            //vlozí původní záznam 00:00:00:00 aby první mezicas vysel
            ZaznamiMeziCasu.Add(nula);
        }

        //Preformatuje datovy typ TimeSpan na String a zkrati presnost milisekund na 2 desetina cisla
        public string Formatovat(TimeSpan formatovat)
        {
            string vysledek = string.Format("{0:00}:{1:00}:{2:00}:{3:00}",
                (int)formatovat.TotalHours, formatovat.Minutes, formatovat.Seconds, formatovat.Milliseconds / 10);
            return vysledek;
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
                //Vypise cas
                Console.Write($"\r   {Formatovat(konec)}   ");

                //Ceka na stisknuti klavesy
                if (Console.KeyAvailable)
                {
                    //nacte stisknutou klavesu
                    ConsoleKeyInfo volba = Console.ReadKey();
                    //pokud je klavesa K
                    if (volba.Key == ConsoleKey.K)
                    {
                        Kolo(konec,konec);
                    }
                }
            }
        }

        //udelá zaznam o kole + pridá záznam do historie a vypíše max 5 zaznamu
        public void Kolo(TimeSpan aktualniCas, TimeSpan meziCas)
        {
            //prida zaznam do seznamu zaznamu
            Zaznami.Add(aktualniCas);
            //prida zaznam do seznamu mezicasu
            ZaznamiMeziCasu.Add(meziCas);
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
                    Console.WriteLine((i + 1) + " --- " + Formatovat(ZaznamiMeziCasu[i+1] - ZaznamiMeziCasu[i]) + " --- " + Formatovat(Zaznami[i]));
                    poziceKurzoruVHistorii++;

                }
                else
                {
                    //pokud pocet zaznamu presahne 5 ukonci projizdeni seznamu
                    Console.WriteLine((i + 1) + " --- " + Formatovat(ZaznamiMeziCasu[i + 1] - ZaznamiMeziCasu[i]) + " --- " + Formatovat(Zaznami[i]));
                    poziceKurzoruVHistorii = 2;
                    i = 0;
                }
            }
        }

    }
}
