using A11.Classes.Matricos;

namespace A11.Classes.Sindromai
{
    public class LyderiuLentele
    {
        // lyderiu lenteles konstruktorius, priima kontroline matrica ir n (ilgi)
        // inicializuoja sindromu ir ju svoriu lentele, reikalinga dekodavimui
        public LyderiuLentele(KontrolineMatrica kontrolineMatrica, int n)
        {
            this.n = n;
            KontrolineMatrica = kontrolineMatrica;
            SindromaiSvoriai = new Dictionary<string, int>();
            GeneruotiSvoriusSuSindromais();
        }

        private readonly int n;
        public Dictionary<string, int> SindromaiSvoriai { get; set; }
        private KontrolineMatrica KontrolineMatrica { get; set; }

        // generuoja svorius su sindromais,
        // ideda i lentele galimu zinuciu sindromus, kurios turi maziausia svori
        private void GeneruotiSvoriusSuSindromais()
        {
            // iteruojama per visas imanomas zinutes
            for (var i = 0; i < (int) Math.Pow(2, n); i++)
            {
                // zinutes pavertimas i binarini vektoriu
                var generuotaZinute = DecToBin(i, n);

                // zinutes daugyba su kontroline matrica, gaunamas sindromas
                var sindromas = OperacijosMatricos.Daugyba(b: generuotaZinute, a: KontrolineMatrica);

                // sindromas paverciamas i string, kad galetume ji naudoti kaip raktu
                var sindromasString = string.Join("", sindromas);

                // zinutes svoris yra visu vienetu suma, nes tik vienetai turi reiksme
                var svoris = generuotaZinute.Sum();

                // tarpiniams rezultatams tikrinti:
                //Console.WriteLine($"{string.Join("", generuotaZinute)} - {sindromasString} - {svoris})");
                try
                {
                    // jei sindromas jau yra lenteleje, tikrinama ar naujas svoris yra mazesnis
                    if (SindromaiSvoriai[sindromasString] > svoris)
                    {
                        // jeigu mazesnis, svoris pakeiciamas
                        SindromaiSvoriai[sindromasString] = svoris;
                    }
                }
                catch (KeyNotFoundException)
                {
                    // jei sindromas dar nera lenteleje, ji idedame su svoriu
                    SindromaiSvoriai[sindromasString] = svoris;
                }
            }
        }

        // paima skaiciaus ilgi, kad zinotu kiek reikia iteraciju, ir desimtaini skaiciu
        // kovertuoja ir grazina dvejetaini skaiciu, isreiksta vektoriaus pavidalu
        private static int[] DecToBin(int dec, int ilgis)
        {
            var binary = new int[ilgis];
            var index = 0;
            while (dec > 0)
            {
                // vektorius formuojama su "mod" operacija
                binary[index++] = dec % 2;
                // desimtainis skaicius dalinamas is 2, kad galetume gauti sekancia reiksme
                dec /= 2;
            }
            return binary;
        }

        // atspausdina visus svorius su sindromais, tarpiniams rezultatams tikrinti
        public void Print()
        {
            foreach (var sindromas in SindromaiSvoriai)
            {
                Console.WriteLine($"{sindromas.Key} - {sindromas.Value}");
            }
        }
    }
}
