using A11.Classes;
using A11.Classes.Matricos;

namespace A11.Scenarijai
{
    public class Scenarijus2
    {
        // vykdo scenariju 2 su nurodytais parametrais,
        // kaip generuojanti matrica, stulpeliaiN, eilutesK, klaidosTikimybe
        public void Vykdyti(GeneruojantiMatrica? generuojantiMatrica, int stulpeliaiN, int eilutesK,
            double klaidosTikimybe)
        {
            var random = new Random();

            // nuskaitomas failas ir konvertuojamas i binary string
            var failoTurinys = NuskaitytiFaila();
            var binaryString = StrToBinary(failoTurinys);

            // suskaidomas binary string i vektorius, kuriuos uzkoduojame arba tik suskaidome
            var (uzkoduotiVektoriai, uzpildymai, neKoduotiVektoriai) =
                Pernaudojama.SuskaidytiIrUzkoduoti(
                    generuojantiMatrica,
                    binaryString,
                    eilutesK);

            // siunciami vektoriai per kanala ir uzkoduotus vektorius
            var (dekoduotiVektoriai, neDekoduotiVektoriai) =
                Pernaudojama.SiustiKanaluIrDekoduoti(
                    generuojantiMatrica,
                    stulpeliaiN, eilutesK, klaidosTikimybe,
                    uzkoduotiVektoriai,
                    neKoduotiVektoriai,
                    uzpildymai,
                    random);

            // spausdinimui vektoriai paversti i string, o string i simbolius
            Console.WriteLine("\nGalutinis nekoduotas tekstas is kanalo:");
            Console.WriteLine(BinaryStrToSimboliai(Pernaudojama.VektoriaiToStr(neDekoduotiVektoriai)));
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Galutinis dekoduotas tekstas is kanalo:");
            Console.WriteLine(BinaryStrToSimboliai(Pernaudojama.VektoriaiToStr(dekoduotiVektoriai)));

            Console.WriteLine("Scenarijus 2 baigtas");
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
