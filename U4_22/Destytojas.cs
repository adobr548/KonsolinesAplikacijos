using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace U4_22
{
    // Destytojo klase
    class Destytojas
    {
        private string modulioPav;      //Modulio pavadinimas
        private string VardasDest;      //Atsakingo dėstytojo pavardė
        private string PavardeDest;     //Atsakingo dėstytojo vardas
        private int kreditai;           //Kreditų kiekis
        private string VardasStud;      //Studento vardas
        private string PavardeStud;     //Studento pavardė
        private string grupe;           //Grupė

        // Konstruktorius
        public Destytojas(string modulioPav, string VardasDest, string PavardeDest, int kreditai, string VardasStud, string PavardeStud, string grupe)
        {
            this.modulioPav = modulioPav;
            this.VardasDest = VardasDest;
            this.PavardeDest = PavardeDest;
            this.kreditai = kreditai;
            this.VardasStud = VardasStud;
            this.PavardeStud = PavardeStud;
            this.grupe = grupe;

        }
        //Konstruktorius be parametru
        public Destytojas() {}
       
        //grazina modulio pavadiima ,dest.varda ir pavarde,kreditu kieki,studento varda,pavarde ir grupe
        public string ImtimodulioPav() { return modulioPav; }
        public string ImtiVardasDest() { return VardasDest; }
        public string ImtiPavardeDest() { return PavardeDest; }
        public int ImtiKreditai() { return kreditai; }
        public string ImtiVardasStud() { return VardasStud; }
        public string ImtiPavardeStud() { return PavardeStud; }
        public string ImtiGrupe() { return grupe; }

        // Spausdinimo metodas
        public override string ToString()
        {
            string eilute;
            eilute = string.Format("|{0,-27}|{1,-11}|{2,-11}| {3,-8}|{4,-12}|{5,-12}|{6,-8}|",
                        modulioPav, VardasDest, PavardeDest, kreditai, VardasStud, PavardeStud, grupe);
            return eilute;
        }
    }
 }

