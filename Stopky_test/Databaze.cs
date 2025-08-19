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

        public void Vlozit(Zaznam zaznam)
        {
            historie.Add(zaznam);
        }
        public void Odebrat(Zaznam zaznam)
        {
            historie.Remove(zaznam);
        }
        public void Vypis()
        {
            int y = 0;
            foreach (Zaznam zaznam in historie)
            {
                if(zaznam.m_mereni != y)
                {
                    y++;
                    Console.WriteLine("*-----------měření "+ y +"--------*");
                    Console.WriteLine(zaznam.m_kolo + " --- " + zaznam.m_mezicas + " --- " + zaznam.m_cas);
                }
                else
                {
                    Console.WriteLine(zaznam.m_kolo + " --- " + zaznam.m_mezicas + " --- " + zaznam.m_cas);
                }
            }
        }

    }
}
