using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace U4_22
{
    // Konteinerine  klase
    class Fakultetas
    {
        const int CMaxi = 800; // maks.studentu skaicius
        private Destytojas[] Studentai; // studentai kurie rinkosi destytoja
        private int n; // studentu skaicius
        
        public Fakultetas()
        {
            n = 0;
            Studentai = new Destytojas[CMaxi];
        }
        
        //Sąrašas surikiuojamas pagal kreditus maž. tvarka

        public void Rikiuoti()
        {
            for (int a = 0; a < n - 1; a++)
            {
                int min = Studentai[a].ImtiKreditai();
                Destytojas nim = Studentai[a];
                int mi = a;
                for (int b = a + 1; b < n; b++)
                    if (Studentai[b].ImtiKreditai() > min)
                    {
                        min = Studentai[b].ImtiKreditai();
                        nim = Studentai[b];
                        mi = b;
                    }
                Studentai[mi] = Studentai[a];
                Studentai[a] = nim;
            }
        }

      

        /** Grąžina nurodyto indekso destytojo objektą.
       @param i - destytojo indeksas */

        public Destytojas Imtidest(int i) { return Studentai[i]; }

        /** Grąžina dėstytojų kiekį */
        public int Imti() { return n; }

        /** Padeda į dėstytojų objektų masyvą naują dėstytoją ir
        // masyvo dydį padidina vienetu.
        @param des - dėstytojo objektas */
        public void Deti(Destytojas des) { Studentai[n++] = des; }
    }
}
