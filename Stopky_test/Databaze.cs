using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Stopky_test
{
    internal class Databaze
    {

        List<Zaznam> historie = new List<Zaznam>();

        List<string> nacteneZaznami = new List<string>();

        static string cesta = ("UlozenaMereni.txt");

        public Databaze() 
        {

        }

        //metoda pro vlozeni zaznamu do seznamu
        public void Vlozit(Zaznam zaznam)
        {
            historie.Add(zaznam);
        }
        //metoda pro odebrani zaznamu ze seznamu
        public void Odebrat(Zaznam zaznam)
        {
            historie.Remove(zaznam);
        }
        public void Vypis()
        {
            //vycisti konzoli a vypise legendu
            Console.Clear();
            Console.WriteLine("*-----------Historie-----------*");
            Console.WriteLine("(U)ložit záznam - (Z)pět");
            Console.WriteLine("Kolo --- Mezičas --- Čas");
            //docasne pocitadlo pro kontrolu cisla mereni
            int y = 0;
            foreach (Zaznam zaznam in historie)
            {
                //pokud se nerovnaji tak se navyši pokud se nenavyši znamena že byly zaznami porizeny ve stejnem meřeni
                if(zaznam.m_mereni != y)
                {
                    y++;
                    Console.WriteLine("*-----------měření "+ y +"--------*");
                    Console.WriteLine(zaznam.m_kolo + " --- " + Formatovat(zaznam.m_mezicas) + " --- " + Formatovat(zaznam.m_cas));
                }
                else
                {
                    Console.WriteLine(zaznam.m_kolo + " --- " + Formatovat(zaznam.m_mezicas) + " --- " + Formatovat(zaznam.m_cas));
                }
            }
        }

        public void VypisZeSouboru()
        {
            Console.WriteLine("*---------Ulozene Časy---------*");
            Console.WriteLine("Smazat záznam (d) - (Z)pět");
            Console.WriteLine("ID --- Kolo --- Mezičasy --- čas");
            foreach (String zaznam in nacteneZaznami)
            {
                Console.WriteLine(zaznam);
            }
        }

        public void NactiZeSouboru()
        {
            StreamReader cteni = new StreamReader(cesta);
            string radek;

            while ((radek = cteni.ReadLine()) != null)
            {
                if (radek.StartsWith("&"))
                {
                    nacteneZaznami.Add("---"+radek.Substring(1)+"---");
                }
                else
                {
                string[] nactenyZaznam = radek.Split(';');
                nacteneZaznami.Add(nactenyZaznam[0] + " --- " + nactenyZaznam[1] + " --- " + nactenyZaznam[2] + " --- " + nactenyZaznam[3]);
                }
                

            }
            cteni.Close();
        }

        public void UlozDoSouboru(string poznamka,int ID,int kolo,TimeSpan mezicas, TimeSpan cas)
        {
            StreamWriter psani = new StreamWriter(cesta);
            string zapisPoznamku = poznamka;
            string zapis = ID + ";" + kolo + ";" + Formatovat(mezicas) + ";" + Formatovat(cas);
            psani.WriteLine(zapisPoznamku);
            psani.WriteLine(zapis);

            psani.Close();
        }

        //metoda pro formatovani na text je to trochu redundantní
        public string Formatovat(TimeSpan formatovat)
        {
                string vysledek = string.Format("{0:00}:{1:00}:{2:00}:{3:00}",
                    (int)formatovat.TotalHours, formatovat.Minutes, formatovat.Seconds, formatovat.Milliseconds / 10);
                return vysledek;
        }

    }
}
