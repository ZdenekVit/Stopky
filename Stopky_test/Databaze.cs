using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stopky_test
{
    internal class Databaze
    {

        List<Zaznam> historie = new List<Zaznam>();
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

        //metoda pro formatovani na text je to trochu redundantní
        public string Formatovat(TimeSpan formatovat)
        {
                string vysledek = string.Format("{0:00}:{1:00}:{2:00}:{3:00}",
                    (int)formatovat.TotalHours, formatovat.Minutes, formatovat.Seconds, formatovat.Milliseconds / 10);
                return vysledek;
        }

    }
}
