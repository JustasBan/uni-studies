using System.Diagnostics;
using System.Text;
using A11.Classes;

namespace A11.Scenarijai
{
    public class Scenarijus3
    {
        // vykdo scenariju 3 su nurodytais parametrais,
        // kaip generuojanti matrica, stulpeliaiN, eilutesK, klaidosTikimybe
        public void Vykdyti(
            GeneruojantiMatrica generuojantiMatrica,
            int stulpeliaiN, int eilutesK,
            double klaidosTikimybe)
        {
            var random = new Random();

            // nuskaitomas failas ir konvertuojamas i binary string
            var (failoTurinys, failoAntraste) = NuskaitytiFaila();

            // suskaidomas binary string i vektorius, kuriuos uzkoduojame arba tik suskaidome
            var (uzkoduotiVektoriai, uzpildymai, neKoduotiVektoriai) =
                Pernaudojama.SuskaidytiBinaryString(generuojantiMatrica, failoTurinys, eilutesK);

            // siunciami vektoriai per kanala ir uzkoduotus/neuzkoduotus vektorius
            var (dekoduotiVektoriai, neDekoduotiVektoriai) =
                Pernaudojama.SiustiKanaluIrDekoduoti(
                    generuojantiMatrica,
                    stulpeliaiN, eilutesK, klaidosTikimybe,
                    uzkoduotiVektoriai,
                    neKoduotiVektoriai,
                    uzpildymai,
                    random);

            // dekoduoti vektoriai konvertuojami i binary string
            var dekoduotasBinaryString = Pernaudojama.VektoriaiToStr(dekoduotiVektoriai);
            var neDekoduotasBinaryString = Pernaudojama.VektoriaiToStr(neDekoduotiVektoriai);

            // sukuriami laikini .bmp failai
            // ir atidaromi numatyta paveikslelio perziuros programa

            Console.WriteLine("Kuriami laikini .bmp failai...");

            SukurtiBmpFaila(
                dekoduotasBinaryString,
                @"C:\temp_JustoBaniulio\test_dekoduotas.bmp",
                failoAntraste);
            Console.WriteLine("Atidaromas dekoduotas .bmp failas...");
            AtidarytiNuotrauka(@"C:\temp_JustoBaniulio\test_dekoduotas.bmp");

            SukurtiBmpFaila(
                neDekoduotasBinaryString,
                @"C:\temp_JustoBaniulio\test_nedekoduotas.bmp",
                failoAntraste);
            Console.WriteLine("Atidaromas nedekoduotas .bmp failas...");
            AtidarytiNuotrauka(@"C:\temp_JustoBaniulio\test_nedekoduotas.bmp");

            Console.WriteLine("Scenarijus 3 baigtas");
        }

        // papraso vartotojo failo, nuskaito ji ir grazina turini su antraste
        private static (string, byte[]) NuskaitytiFaila()
        {
            Console.WriteLine("Paveikslelis bus nuskaitomas is vietos, kuri nurodysite konsoleje...");
            var failoVieta = Console.ReadLine();
            AtidarytiNuotrauka(failoVieta!);

            // atidaromas failas ir nuskaitomas turinys
            using var fileStream = new FileStream(failoVieta!, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(fileStream);

            var failoTurinys = reader.ReadBytes((int)fileStream.Length);

            // antraste yra laikoma kaip 54 baitai
            var antrastesDuomenys = new byte[54];
            Array.Copy(failoTurinys, antrastesDuomenys, 54);

            // viskas, kas yra po antrastes, yra nuotraukos turinys
            var nuotraukosDuomenys = new byte[failoTurinys.Length - 54];
            Array.Copy(failoTurinys, 54, nuotraukosDuomenys, 0, failoTurinys.Length - 54);

            // konvertuojamas nuotraukos bitinis turinys i binary string
            var binaryStringBuilder = new StringBuilder();
            foreach (var b in nuotraukosDuomenys)
            {
                binaryStringBuilder.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }

            return (binaryStringBuilder.ToString(), antrastesDuomenys);
        }

        // sukuria .bmp faila is binary string, antrastes ir lokacija, kurioje issaugoti
        private static void SukurtiBmpFaila(string nuotraukosTurinys, string rezultatoFailoLokacija, byte[] antrastesTurinys)
        {
            // konvertuojamas binary string i baitus
            var baituDuomenys = new byte[nuotraukosTurinys.Length / 8];
            for (var i = 0; i < baituDuomenys.Length; i++)
            {
                baituDuomenys[i] = Convert.ToByte(nuotraukosTurinys.Substring(i * 8, 8), 2);
            }

            // irasoma antraste ir nuotraukos turinys
            using var fileStream = new FileStream(rezultatoFailoLokacija, FileMode.Create, FileAccess.Write);
            using var writer = new BinaryWriter(fileStream);

            writer.Write(antrastesTurinys);
            writer.Write(baituDuomenys);
        }

        // atidaro nuotrauka per numatyta perziuros programa, paimta is failo lokacijos
        private static void AtidarytiNuotrauka(string lokacija)
        {
            Process.Start(new ProcessStartInfo(lokacija) { UseShellExecute = true });
        }
    }
}
