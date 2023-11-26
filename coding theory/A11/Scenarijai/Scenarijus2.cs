using A11.Classes;

namespace A11.Scenarijai
{
    public class Scenarijus2
    {
        // vykdo scenariju 2 su nurodytais parametrais,
        // kaip generuojanti matrica, stulpeliaiN, eilutesK, klaidosTikimybe
        public void Vykdyti(GeneruojantiMatrica generuojantiMatrica, int stulpeliaiN, int eilutesK,
            double klaidosTikimybe)
        {
            var random = new Random();

            // nuskaitomas failas ir konvertuojamas i binary string
            var failoTurinys = NuskaitytiFaila();
            var binaryString = StrToBinary(failoTurinys);

            // suskaidomas binary string i vektorius, kuriuos uzkoduojame arba tik suskaidome
            var (uzkoduotiVektoriai, uzpildymai, neKoduotiVektoriai) =
                SuskaidytiBinaryString(
                    generuojantiMatrica,
                    binaryString,
                    eilutesK);

            // siunciami vektoriai per kanala ir uzkoduotus vektorius
            var (dekoduotiVektoriai, neDekoduotiVektoriai) =
                SiustiKanaluIrDekoduoti(
                    generuojantiMatrica,
                    stulpeliaiN, eilutesK, klaidosTikimybe,
                    uzkoduotiVektoriai,
                    neKoduotiVektoriai,
                    uzpildymai,
                    random);

            // spausdinimui vektoriai paversti i string, o string i simbolius
            Console.WriteLine("\nGalutinis nekoduotas tekstas is kanalo:");
            Console.WriteLine(BinaryStrToSimboliai(VektoriaiToStr(neDekoduotiVektoriai)));
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("\nGalutinis dekoduotas tekstas is kanalo:");
            Console.WriteLine(BinaryStrToSimboliai(VektoriaiToStr(dekoduotiVektoriai)));
        }

        // papraso vartotojo failo, nuskaito ji ir grazina turini
        private static string NuskaitytiFaila()
        {
            Console.WriteLine("Tekstas bus nuskaitomas is vietos, kuri nurodysite konsoleje...");
            var failoVieta = Console.ReadLine();

            // bandoma nuskaityti faila
            try
            {
                // perimamas visas failo turinys
                var failoTurinys = File.ReadAllText(failoVieta!);
                Console.WriteLine("Gautas tekstas:");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine(failoTurinys);
                Console.WriteLine("----------------------------------------");
                return failoTurinys;
            }
            catch (Exception ex)
            {
                // jei nepavyksta nuskaityti failo, programa pranesa ir baigia darba
                Console.WriteLine("Klaida skaitant faila: " + ex.Message);
                Environment.Exit(0);
                return null;
            }
        }

        // suskaido binary string i vektorius, kuriuos uzkoduojame ir vektorius, kuriuos tik siunciam
        // grazina uzkoduotus vektorius, neuzkoduotus vektorius ir uzpildymo nuliais kiekius
        // paima generuojancia matrica, vektorius string formatu ir k reiksmes
        private static (List<int[]>, List<int>, List<int[]>) SuskaidytiBinaryString(
            Matrica generuojantiMatrica,
            string binaryString,
            int eilutesK)
        {
            var uzkoduotiVektoriai = new List<int[]>();
            var neUzkoduotiVektoriai = new List<int[]>();
            var pastumimai = new List<int>();

            // skaidome i vektorius i zinutes ilgio dalimis
            for (var i = 0; i < binaryString.Length; i += eilutesK)
            {
                var vektoriausIlgis = Math.Min(eilutesK, binaryString.Length - i);

                // skaidymas ir uzpildymas nuliais, jei reikia
                var vektoriusString = binaryString
                    .Substring(i, vektoriausIlgis)
                    .PadRight(eilutesK, '0');

                // konvertuojame string vektoriu i skaicius
                var vektorius = vektoriusString
                    .Select(c => c - '0')
                    .ToArray();

                // uzkoduojame atskirta ir konvertuota vektoriu
                var kodavimas = new Kodavimas(generuojantiMatrica, vektorius);
                uzkoduotiVektoriai.Add(kodavimas.UzkoduotasVektorius);

                // suskaiciuojame buvo uzpildyta nuliais zinute
                var uzpildymoKiekis = eilutesK - vektoriausIlgis;
                pastumimai.Add(uzpildymoKiekis);

                // jei vektorius neuzkoduotas, tai ji issaugome atskirai
                neUzkoduotiVektoriai.Add(vektorius);
            }

            return (uzkoduotiVektoriai, pastumimai, neUzkoduotiVektoriai);
        }

        private static (IEnumerable<int[]>, IEnumerable<int[]>) SiustiKanaluIrDekoduoti(
            GeneruojantiMatrica generuojantiMatrica,
            int stulpeliaiN, int eilutesK, double klaidosTikimybe,
            IReadOnlyList<int[]> uzkoduotiVektoriai,
            IReadOnlyList<int[]> neUzkoduotiVektoriai,
            IReadOnlyList<int> uzplidymai,
            Random random)
        {
            var dekoduotiVektoriai = new List<int[]>();
            var neDekoduotiVektoriai = new List<int[]>();
            var dekodavimas = new Dekodavimas(generuojantiMatrica, stulpeliaiN);

            // siunciami neuzkoduoti vektoriai per kanala
            for (var i = 0; i < neUzkoduotiVektoriai.Count; i++)
            {
                // paimamas vektorius
                var vektorius = neUzkoduotiVektoriai[i];
                var kanalas = new Kanalas(klaidosTikimybe, vektorius, random);

                // kanalas iskraipo ir issaugo rezultata,
                // atsizvelgiant i uzpildyma nuliais
                kanalas.Siusti();
                neDekoduotiVektoriai
                    .Add(kanalas.GautaZinute
                        .Take(vektorius.Length - uzplidymai[i])
                        .ToArray());
            }

            // siunciami uzkoduoti vektoriai per kanala
            for (var i = 0; i < uzkoduotiVektoriai.Count; i++)
            {
                // kanalas iskraipo vektoriu
                var kanalas = new Kanalas(klaidosTikimybe, uzkoduotiVektoriai[i], random);
                kanalas.Siusti();
                var gautasVektorius = kanalas.GautaZinute;

                // dekoduojame vektoriu,
                // rezultatas gaunamas atsizvelgiant i uzpildyma nuliais ir zinutes ilgi
                var dekoduotasVektorius = dekodavimas
                    .DekoduotiStepByStep(gautasVektorius)
                    .Take(eilutesK - uzplidymai[i])
                    .ToArray();
                dekoduotiVektoriai.Add(dekoduotasVektorius);
            }

            return (dekoduotiVektoriai, neDekoduotiVektoriai);
        }

        // paima vektoriu sarasa ir sujungia i viena string
        private static string VektoriaiToStr(IEnumerable<int[]> vektoriai)
        {
            return string.Join("", vektoriai.Select(v => string.Join("", v)));
        }

        // paima binary string ir konvertuoja i simbolius
        private static string BinaryStrToSimboliai(string vektoriusString)
        {
            var simboliai = new List<char>();
            // iteruojame po 16, nes vienas simbolis uzima 16 bitu
            for (var i = 0; i < vektoriusString.Length; i += 16)
            {
                // paimami 16 skaiciu
                var vektoriausDalis = vektoriusString.Substring(i, 16);
                // pavertus skaiciu, konvertuojama i simboli (pagal unicode lentele)
                simboliai.Add((char)Convert.ToInt32(vektoriausDalis, 2));
            }

            // grazinamas simboliu sarasas kaip vienas string
            return new string(simboliai.ToArray());
        }

        // paima string ir grazina binary string
        // kiekvienas simbolis konvertuojamas i 16 bitu skaiciu
        private static string StrToBinary(string @string)
        {
            var konvertuotasString = @string
                // konvertuojame i 16 bitu skaiciu
                .Select(c => Convert.ToString(c, 2)
                    // jei skaicius mazesnis nei 16 bitu, uzpildome nuliais
                    .PadLeft(16, '0'))
                .ToList();

            return string.Join("", konvertuotasString);
        }
    }
}
