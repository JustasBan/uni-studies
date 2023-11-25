using System.Security.AccessControl;
using A11.Classes.Matricos;
using A11.Services;

namespace A11.Classes.Sindromai
{
    public class LyderiuLentele
    {
        public LyderiuLentele(GeneruojantiMatrica generuojantiMatrica, int n, int k)
        {
            this.n = n;
            this.k = k;

            KontrolineMatrica = new KontrolineMatrica(generuojantiMatrica)
            {
                Duomenys = new[,]
                {
                    { 1,0,0,1,0,1 },
                    { 0,1,0,1,1,0 },
                    { 0,0,1,0,1,1 }
                }
            };
            KontrolineMatrica.Transponuoti();

            SindromaiSvoriai = new Dictionary<string, int>();

            GeneruotiSvoriusSuSindromais();
        }

        private readonly int n;
        private readonly int k;

        private Dictionary<string, int> SindromaiSvoriai { get; set; }
        private KontrolineMatrica KontrolineMatrica { get; set; }

        private void GeneruotiSvoriusSuSindromais()
        {

            for (var i = 0; i < (int) Math.Pow(2, n); i++)
            {
                var generuotaZinute = DecToBin(i, n);

                var sindromas = OperacijosMatricos.Daugyba(B: generuotaZinute, A: KontrolineMatrica);
                var sindromasString = string.Join("", sindromas);

                var svoris = generuotaZinute.Sum();

                Console.WriteLine($"{string.Join("", generuotaZinute)} - {sindromasString} - {svoris})");

                try
                {
                    if (SindromaiSvoriai[sindromasString] > svoris)
                    {
                        SindromaiSvoriai[sindromasString] = svoris;
                    }
                }
                catch (KeyNotFoundException)
                {
                    SindromaiSvoriai[sindromasString] = svoris;
                }
            }
        }

        private static int[] DecToBin(int dec, int ilgis)
        {
            var binary = new int[ilgis];
            var index = 0;
            while (dec > 0)
            {
                binary[index++] = dec % 2;
                dec /= 2;
            }
            return binary;
        }

        public void Print()
        {
            // print svorius su sindromais
            foreach (var sindromas in SindromaiSvoriai)
            {
                Console.WriteLine($"{sindromas.Key} - {sindromas.Value}");
            }
        }
    }
}
