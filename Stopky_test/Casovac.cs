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
        bool casovacJede = false;
        TimeSpan casPauzi = DateTime.MinValue.TimeOfDay;

        //list docasne drzíci zaznami
        List<TimeSpan> Zaznami = new List<TimeSpan>();
        //list docasne drzíci zaznami mezicasu (elemnt s indexem 0 je 00:00:00:00 takže je o +1 posunuty)
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

        public void menuVoleb(ConsoleKeyInfo volba)
        {           
                //pokud je klavesa K pro Kolo
                if (volba.Key == ConsoleKey.K)
                {

                    Kolo(konec, konec);

                } //pokud je klavesa P pro Pauzu
                else if (volba.Key == ConsoleKey.P)
                {
                    Pauza(konec);

                }//pokud je klavesa S pro Start
                else if (volba.Key == ConsoleKey.S)
                { 
                        CasStart(DateTime.Now);
                }
            
        }

        //Spustí se casomira
        public void CasStart(DateTime start) 
        {
            //Ziska cas pri zmacknuti Start
            zacatek = start;
            //zapne casovac
            casovacJede=true;
            while(casovacJede)
            {
                //Získá aktualni cas bud před pauzou nebo po pauze
                if (casPauzi == DateTime.MinValue.TimeOfDay)
                {
                    konec = (DateTime.Now - zacatek);
                }
                else
                {
                    konec = (DateTime.Now - zacatek) + casPauzi;
                }
                //Nastaví pozici casomiri na prvni radek
                Console.SetCursorPosition(0, 0);
                //Vypise cas
                Console.Write($"\r   {Formatovat(konec)}   ");

                //Zavolá první menu
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo volba = Console.ReadKey();
                    menuVoleb(volba);
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
                //vypisuje zaznami odshora 
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
            //zabrání pádu pokud casovac nejede
            if (casovacJede == false)
            {
                menuVoleb(Console.ReadKey());
            }
        }

        //ulozi uběhnuty cas a vypne casovac + znovu zavolá menu volby
        public void Pauza(TimeSpan ubehnutyCas)
        {
            casPauzi = ubehnutyCas;
            casovacJede = false;
            menuVoleb(Console.ReadKey());
        }

    }
}
