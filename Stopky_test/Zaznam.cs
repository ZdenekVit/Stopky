using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stopky_test
{
    internal class Zaznam
    {
        public int m_mereni { get; private set; }
        public int m_kolo { get;private set; }
        public TimeSpan m_mezicas { get; private set; }
        public TimeSpan m_cas { get; private set; }

        //konstruktor
        public Zaznam(int mereni, int kolo, TimeSpan mezicas, TimeSpan cas)
        {
            m_mereni = mereni;
            m_kolo = kolo;
            m_mezicas = mezicas;
            m_cas = cas;
        }


        //tyhle funkce jsou v tomto případě zbytečné chci jen aby jste viděli že vím že ve větších projektech by se to správně mělo dělat přes ne
        public int vratKolo(Zaznam zaznam)
        {
            return zaznam.m_kolo;
        }
        public int vratMereni(Zaznam zaznam)
        {
            return zaznam.m_mereni;
        }
        public TimeSpan vratMeziCas(Zaznam zaznam)
        {
            return zaznam.m_mezicas;
        }
        public TimeSpan vratCas(Zaznam zaznam)
        {
            return zaznam.m_cas;
        }
    }
}
