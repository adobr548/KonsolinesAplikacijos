// Audris Dobrikas IFZ-5/3
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; // prijungiam ivedimo ir isvedimo klasiu biblioteka

namespace U3_22
{
    class Kaliause // Klase,kuri saugo vienos kaliauses tipa,apredus,svori,kaina
    {
        private string tipas;
        private int apredai;
        private double svoris;
        private double kaina;
        // Konstruktorius
        public Kaliause(string tipas,int apredai,double svoris,double kaina)
        {
            this.tipas = tipas;
            this.apredai = apredai;
            this.svoris = svoris;
            this.kaina = kaina;
        }
        // Grazina kaliauses tipa,apredus,svori,kaina
        public string ImtiTipa() { return tipas; }
        public int ImtiApredus() { return apredai; }
        public double ImtiSvori() { return svoris; }
        public double ImtiKaina() { return kaina; }

        const int Cn = 100;                    //Masyvo dydis (konstanta) 
        const string CFd1 = "Duomenys1.txt";   // Pirmo rinkinio duomenu failas
        const string CFd2 = "Duomenys2.txt";   // Antro rinkio duomenu failas
        const string CFrez = "Rezultatai.txt"; // Rezultatu failas

        static void Main(string[] args)
        {
            Kaliause[] K1 = new Kaliause[Cn]; // K1- pirmo rinkinio objektas
            int n1;                          // pirmo rinkinio kaliausiu skaicius
            string pav1;                     // pirmo rinkinio ukininko vardas ir pavarde

            Kaliause[] K2 = new Kaliause[Cn]; // K2 - antro rinkinio objektas
            int n2;                           // antro rinkinio kaliausiu skaicius
            string pav2;                      // antro rinkinio ukininko vardas ir pavarde

            //Issikvieciam duomenu skaitymo metoda abiem rinkiniams
            Skaityti(CFd1, K1, out n1, out pav1);
            Skaityti(CFd2, K2, out n2, out pav2);

            if (File.Exists(CFrez)) // tikrinama ar rezultatu failas egzistuoja jei taip,tai istrinam ta faila
                File.Delete(CFrez);

            Kaliause[] K = new Kaliause[Cn]; // sukuriam objekta bendram rinkiniui
            int nr = 0;

            //Isikivieciam metoda ,kuris formuoja bendra rinkini is turimu dvieju
            Formuoti(K1, n1, K, ref nr);
            Formuoti(K2, n2, K, ref nr);      

            using (var fr = File.AppendText(CFrez))
            {
                // Sunkiausios kaliauses palygnimas ir rezultatu isvedimas i faila 
                if (Sunkiausia(K1, n1) > Sunkiausia(K2, n2))
                    fr.WriteLine("Sunkiausia kaliause {0},ja turi ukininkas {1} ,jos svoris {2} ,o apredai {3} ", SunkiausiasTipas(K1, n1), pav1, Sunkiausia(K1, n1), SunkiausiosApredai(K1, n1));
                else
                    fr.WriteLine("Sunkiausia kaliause {0},ja turi ukininkas {1} ,jos svoris {2} ,o apredai {3} ", SunkiausiasTipas(K2, n2), pav2, Sunkiausia(K2, n2), SunkiausiosApredai(K2, n2));
                if (Sunkiausia(K1, n1) == Sunkiausia(K2, n2))
                {
                    fr.WriteLine(" Sunkiausia kaliause {0} jos svoris {1} ", SunkiausiasTipas(K1, n1),Sunkiausia(K1, n1));
                    fr.WriteLine(" Sunkiausia kaliause {0} jos svoris {1} ", SunkiausiasTipas(K2, n2),Sunkiausia(K2, n2));
                }

               

                // Sunkiausios kaliauses apredu isvedimas i faila
                fr.WriteLine("Ukininkas {0} turi sunkiausia kaliause {1} ir jos apredai {2} ", pav1, SunkiausiasTipas(K1, n1), SunkiausiosApredai(K1, n1));
                fr.WriteLine("Ukininkas {0} turi sunkiausia kaliause {1} ir jos apredai {2} ", pav2, SunkiausiasTipas(K2, n2), SunkiausiosApredai(K2, n2));

                //Sunkiausios kaliauses apredu palyginimas ir rezultatu isvedimas i faila
                if (SunkiausiosApredai(K1, n1) > SunkiausiosApredai(K2, n2))
                    fr.WriteLine("Sunkiausia daugiausiai apredyta kaliause turi ukininkas {0}", pav1);
                else
                    fr.WriteLine("Sunkiausia daugiausiai apredyta kaliause turi ukininkas {0}", pav2);
                if (SunkiausiosApredai(K1, n1) == SunkiausiosApredai(K2, n2))
                    fr.WriteLine("Abieju ukininku sunkiausios kaliauses turi tiek pat apredu");

                //Vidutines kainos isvedimas i faila
                fr.WriteLine("Ukininkas {0} ir jo vidutine kaliauses kaina {1:f} ", pav1, VidutineKaina(K1, n1));
                fr.WriteLine("Ukininkas {0} ir jo vidutine kaliauses kaina {1:f} ", pav2, VidutineKaina(K2, n2));
                fr.WriteLine("Abieju ukininku Vidutine kaliauses kaina {0:f} ", VidutineKaina(K, nr));

                // Vidutines kainos palygininmas ir rezultatu isvedimas faile
                if (VidutineKaina(K1, n1) < VidutineKaina(K2, n2))
                    fr.WriteLine(" Maziausia Vidutine kaliauses kaina yra pas ukininka,kurio vardas {0} ", pav1);
                else
                    fr.WriteLine(" Maziausia Vidutine kaliauses kaina yra pas ukininka,kurio vardas {0} ", pav2);
                if (VidutineKaina(K1, n1) == VidutineKaina(K2, n2))
                    fr.WriteLine("Vidutines kainos lygios pas abu ukininkus");
            }

            //Issikvieciam bendra rinkini ir ji atspausdinam 
            SpausdintiDuomenis(CFrez, K, nr, " Abieju ukininku kaliauses ");
            //Issikvieciam 1 ir 2 rinkinius ir juos atspausdinam
            SpausdintiDuomenis(CFrez, K1, n1, pav1);
            SpausdintiDuomenis(CFrez, K2, n2, pav2);
                                          
        }

        // Metodas formuojantis bendra rinkini is dvieju rinkiniu
        static void Formuoti(Kaliause[] X, int n, Kaliause[] Xr, ref int nr)
        {            
            for (int i = 0; i < n; i++)
            {                                                       
                    Xr[nr] = X[i];  // papildomas rinkinys 
                    nr++;                
            }
        }
        // Metodas skaiciuojantis vidutine kaliauses kaina
        static double VidutineKaina(Kaliause[] X, int n)
        {
            double vidurkis = 0;
            for (int i = 0; i < n; i++)
            {
                vidurkis = vidurkis + X[i].ImtiKaina() / n;
            }
            return vidurkis;
        }
        // Metodas randantis sunkiausios kaliauses tipa
        static string SunkiausiasTipas(Kaliause[] X, int n)
        {
            string pav = X[0].ImtiTipa();
            double svoris = X[0].ImtiSvori();

            for (int i = 0; i < n; i++)
            {
                if (X[i].ImtiSvori() > svoris)
                {
                    svoris = X[i].ImtiSvori();
                    pav = X[i].ImtiTipa();
                }
            }
            return pav;
        }

        // Metodas randantis sunkiausios kaliauses svori
        static double Sunkiausia(Kaliause[] X, int n)
        {
            double svoris = X[0].ImtiSvori();

            for (int i = 0; i < n; i++)
            {
                if (X[i].ImtiSvori() > svoris)
                {
                    svoris = X[i].ImtiSvori();
                }
            }
            return svoris;
        }
        // Metodas randantis sunkiausios kaliauses apredus
        static int SunkiausiosApredai(Kaliause[] X, int n)
        {
            int apredai = X[0].ImtiApredus();
            double svoris = X[0].ImtiSvori();

            for (int i = 0; i < n; i++)
            {
                if (X[i].ImtiSvori() > svoris)
                {
                    svoris = X[i].ImtiSvori();
                    apredai = X[i].ImtiApredus();
                }
            }
            return apredai;
        }
        // Duomenu skaitymo metodas is failo.Nuskaito 1 rinkinio duomenis is failo 
        static void Skaityti(string Fd, Kaliause[] X, out int n, out string pav)
        {
            using (StreamReader reader = new StreamReader(Fd))
            {
                string eil; int apred; double svoris; double kaina;
                string line;
                line = reader.ReadLine();
                string[] parts;
                pav = line;
                line = reader.ReadLine();
                n = int.Parse(line);
                for (int i = 0; i < n; i++)
                {
                    line = reader.ReadLine();
                    parts = line.Split(';');
                    eil = parts[0];
                    apred = int.Parse(parts[1]);
                    svoris = double.Parse(parts[2]);
                    kaina = double.Parse(parts[3]);
                    X[i] = new Kaliause(eil, apred, svoris, kaina);
                }
            }
        }
        // Duomenu spausdinimo metodas i faila.Lentele spausdinam vieno rinkinio duomenis
        static void SpausdintiDuomenis(string fv, Kaliause[] X, int n, string pav)
        {
            const string virsus =
                "|-----------------|------------|---------------|---------|\r\n"
                + "|   Tipas         |   Apredu   |  Svoris       |  Kaina  | \r\n"
                + "|                 |   skaicius |   (kg)        | (eur)   | \r\n"
                + "|-----------------|------------|---------------|---------|";
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine("{0}", pav);
                fr.WriteLine(virsus);
                Kaliause tarp;
                for (int i = 0; i < n; i++)
                {
                    tarp = X[i];
                    fr.WriteLine("| {0,-15} | {1,8}   |    {2,5:f2}      | {3,7:f2} |",
                        tarp.ImtiTipa(), tarp.ImtiApredus(), tarp.ImtiSvori(), tarp.ImtiKaina());
                }
                fr.WriteLine("----------------------------------------------------------");
            }
        }

        
        }
    }

