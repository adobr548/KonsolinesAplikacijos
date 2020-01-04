// Audris Dobrikas IFZ-5/3
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace U4_22
{
    class Program
    {
        const string CFd = "Duomenys.txt";
        const string CFr = "Rezultatai.txt";

        static void Main(string[] args)
        {
            Fakultetas Studentai = new Fakultetas();
            Destytojas MaxDest = new Destytojas();
            if (File.Exists(CFr))
                File.Delete(CFr);
            Skaityti(ref Studentai, CFd);
            Spausdinti(Studentai, CFr, "Studentų pasirenkami moduliai");           
            Studentai.Rikiuoti();
            Skaiciuoti(Studentai, out MaxDest, CFr);
            

        }


        /** Failo duomenis surašo į konteinerį
       @param dėstytojai - dėstytojų konteineris
       @param fv - duomenų failo vardas */

        static void Skaityti(ref Fakultetas destytojai, string fv)
        {
            int i = 0;
            string modulioPav, VardasDest, PavardeDest, VardasStud, PavardeStud, grupe;
            int kreditai;
            using (StreamReader reader = new StreamReader(fv, Encoding.GetEncoding(1257)))
            {

                string line;
                string[] parts;
                while ((line = reader.ReadLine()) != null)
                {

                    parts = line.Split(';');
                    modulioPav = parts[0];
                    PavardeDest = parts[1];
                    VardasDest = parts[2];
                    kreditai = int.Parse(parts[3]);
                    PavardeStud = parts[4];
                    VardasStud = parts[5];
                    grupe = parts[6];
                    Destytojas des = new Destytojas(modulioPav, VardasDest, PavardeDest, kreditai, VardasStud, PavardeStud, grupe);
                    destytojai.Deti(des);
                    i = i + 1;
                }
            }

        }

        /** Spausdina konteinerio duomenis faile lentele
        @param Destytojai - dėstytojų konteineris
        @param fv - rezultatų failo vardas
        @param antraste - užrašas virš lentelės */

        static void Spausdinti(Fakultetas Destytojai, string fv, string antraštė)
        {
            string virsus =
              "------------------------------------------------------------------------------------------------\r\n"
            + "| Modulio                   | Destytojo |  Destytojo| Kreditai| Studento   | Studento   | Grupe  |\r\n"
            + "| pavadinimas               |   vardas  |   pavarde |         |   vardas   |  pavarde   |        |\r\n    "
            + "----------------------------------------------------------------------------------------------";
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine(antraštė);
                fr.WriteLine(virsus);
                for (int i = 0; i < Destytojai.Imti(); i++)
                    fr.WriteLine("{0}", Destytojai.Imtidest(i).ToString());
                fr.WriteLine("------------------------------------------------------------------------------------------------\r\n");

            }
        }
        // Atrenka megstamiausia destytoja, atspausdina jo modulius su kreditu kiekiu,atrenka ir atspausdina studentus ir grupes ,kurie nepasirinko sio destytojo
        public static void Skaiciuoti(Fakultetas Destytojai, out Destytojas maxDest, string fv)
        {
            int k = 0,
                maxk = 0;
            string vard, pavard;
            maxDest = new Destytojas();

            for (int i = 0; i < Destytojai.Imti(); i++)
            {
                vard = Destytojai.Imtidest(i).ImtiVardasDest();
                pavard = Destytojai.Imtidest(i).ImtiPavardeDest();

                for (int j = 0; j < Destytojai.Imti(); j++)
                {
                    if ((vard == Destytojai.Imtidest(j).ImtiVardasDest()) && (pavard == Destytojai.Imtidest(j).ImtiPavardeDest()))
                        k++;
                }

                if (k > maxk) // Surandam daugiausia modulių
                {
                    maxk = k;
                    maxDest = Destytojai.Imtidest(i);

                }

                if (k == 1) //Pašalinama
                {                   
                }
                k = 0;
            }

            using (var fr = File.AppendText(fv))
            {

                fr.WriteLine("  Daugumos studentu pasirinktas destytojas  {0}  {1},jo moduliai ir kreditai ", maxDest.ImtiPavardeDest(), maxDest.ImtiVardasDest());
            }



            for (int i = 0; i < Destytojai.Imti(); i++)
            {
                if ((maxDest.ImtiVardasDest() == Destytojai.Imtidest(i).ImtiVardasDest()) && (maxDest.ImtiPavardeDest() == Destytojai.Imtidest(i).ImtiPavardeDest()))
                {


                    using (var fr = File.AppendText(fv))
                    {

                        fr.WriteLine("{0}  {1}   ", Destytojai.Imtidest(i).ImtimodulioPav(), Destytojai.Imtidest(i).ImtiKreditai());
                    }
                }
            }

            using (var fr = File.AppendText(fv))
            {

                fr.WriteLine();
                fr.WriteLine("Studentai esantys siose grupese nepasirinko megstamiausio destytojo");
            }



            for (int i = 0; i < Destytojai.Imti(); i++)
            {
                if ((maxDest.ImtiVardasDest() != Destytojai.Imtidest(i).ImtiVardasDest()) && (maxDest.ImtiPavardeDest() != Destytojai.Imtidest(i).ImtiPavardeDest()))
                {


                    using (var fr = File.AppendText(fv))
                    {

                        fr.WriteLine("{0}   {1} {2}", Destytojai.Imtidest(i).ImtiGrupe(), Destytojai.Imtidest(i).ImtiVardasStud(), Destytojai.Imtidest(i).ImtiPavardeStud());
                    }
                }
                else
                {
                }

            }



        }
    }
}
