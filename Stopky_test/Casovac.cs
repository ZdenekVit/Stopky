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
        Databaze historie = new Databaze();

        DateTime zacatek;
        TimeSpan konec;
        TimeSpan nula = DateTime.Now - DateTime.Now;
        bool casovacJede = false;
        TimeSpan casPauzi = DateTime.MinValue.TimeOfDay;
        int mereni = 1;

        //list docasne drzíci zaznami
        List<TimeSpan> Zaznami = new List<TimeSpan>();

        //list docasne drzíci zaznami mezicasu (elemnt s indexem 0 je 00:00:00:00 takže je o +1 posunuty)
        List<TimeSpan> ZaznamiMeziCasu = new List<TimeSpan>();


        //Konstruktor
        public Casovac() 
        {
            menu();
            //vlozí původní záznam 00:00:00:00 aby první mezicas vysel
            ZaznamiMeziCasu.Add(nula);
            Restart();

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
                }//pokud je klavesa R pro Restart
                else if (volba.Key == ConsoleKey.R)
                { 
                        Restart();
                }//pokud je klavesa H pro Historii
                else if (volba.Key == ConsoleKey.H)
                { 
                        HistorieZaznamu();
                }
                //pokud je klavesa Z pro Zpět
                else if (volba.Key == ConsoleKey.Z)
                {
                        Console.Clear();
                        CasStart(DateTime.Now);
                }
            
        }

        //Spustí se casomira
        public void CasStart(DateTime start) 
        {
            menu();
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
                    ConsoleKeyInfo volba = Console.ReadKey(true);
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
            int poziceKurzoruVHistorii = 5;
            //dynamicky projede seznam od zadu
            for (int i = Zaznami.Count - 1; i >= 0; i--)
            {
                //nastavi startovni pozici kurzoru
                Console.SetCursorPosition(0, poziceKurzoruVHistorii);
                //vypisuje zaznami odshora 
                if (poziceKurzoruVHistorii <= 10)
                {
                    Console.WriteLine((i + 1) + " --- " + Formatovat(ZaznamiMeziCasu[i+1] - ZaznamiMeziCasu[i]) + " --- " + Formatovat(Zaznami[i]));
                    poziceKurzoruVHistorii++;

                }
                else
                {
                    //pokud pocet zaznamu presahne 10 ukonci projizdeni seznamu
                    Console.WriteLine((i + 1) + " --- " + Formatovat(ZaznamiMeziCasu[i + 1] - ZaznamiMeziCasu[i]) + " --- " + Formatovat(Zaznami[i]));
                    poziceKurzoruVHistorii = 5;
                    i = 0;
                }
            }
            //zabrání pádu pokud casovac nejede
            if (casovacJede == false)
            {
                menuVoleb(Console.ReadKey(true));
            }
        }

        //ulozi uběhnuty cas a vypne casovac + znovu zavolá menu volby
        public void Pauza(TimeSpan ubehnutyCas)
        {
            casPauzi = ubehnutyCas;
            casovacJede = false;
            menuVoleb(Console.ReadKey(true));
        }

        //Resetuje hodnotu casovace a vycisti nedavnou historii a předa ji Databazi
        public void Restart()
        {
            //vynuluje cas pauzi
            casPauzi = DateTime.MinValue.TimeOfDay;
            //vypne casovac
            casovacJede=false;
            //zkontroluje zda jsou dočasné seznami prázdné
            if(Zaznami.Count == 0 && ZaznamiMeziCasu.Count == 1)
            {

            }else // pokud ne udělá z existujících záznamu instance a uložíje do databáze
            {
                //dočasná proměná pro očíslování záznamu
                int j = 1;
                foreach (TimeSpan zaznam in Zaznami)
                {
                    historie.Vlozit(new Zaznam(mereni, j, ZaznamiMeziCasu[j], zaznam));
                    j++;
                }
                //vypráznění seznamu
                ZaznamiMeziCasu.Clear();
                Zaznami.Clear();
                mereni++;
                //opětovné přidání nuly do mezicasu
                ZaznamiMeziCasu.Add(nula);
            }

            //vycisti nedavnou historii v konzoli
            for(int x = 5; x <= 11; x++)
            {
                Console.SetCursorPosition(0, x);
                Console.Write("                                        ");
            }
            //nastavi a prepise casovac na 00
            Console.SetCursorPosition(0, 0);

            konec = TimeSpan.Zero;

            Console.Write($"\r   {Formatovat(konec)}  ");

            menuVoleb(Console.ReadKey(true));
        }

        //vypise historii
        public void HistorieZaznamu()
        {
            historie.Vypis();
            menuVoleb(Console.ReadKey(true));
        }

        //legenda
        static void menu()
        {
            Console.SetCursorPosition(0, 1);
            Console.WriteLine("*-------------------------------------------*");
            Console.WriteLine("(S)tart - (R)eset - (P)auza - (K)olo");
            Console.WriteLine("(H)istorie - (U)lozit zaznam - Ulozene (C)asy");
            Console.WriteLine("*-------------Nedávna Historie--------------*");
        }

    }
}
