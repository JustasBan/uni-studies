using A11.Classes;

namespace A11.Scenarijai
{
    public class Scenarijus2
    {
        public void Vykdyti(GeneruojantiMatrica generuojantiMatrica, int stulpeliaiN, int eilutesK,
            double klaidosTikimybe)
        {
            var random = new Random();
            var fileContents = ReadTextFromFile();
            var binaryString = StrToBinary(fileContents);
            var gautiVektoriaiTikKanalu = OnlyTransmit(klaidosTikimybe, binaryString.Select(c => c - '0').ToArray(), random);
            var finalTextTikKanalu = BinaryToStr(string.Join("",gautiVektoriaiTikKanalu));
            Console.WriteLine("Galutinis nekoduotas:");
            Console.WriteLine(finalTextTikKanalu);

            var (encodedVectors, paddingCounts) = EncodeBinaryString(generuojantiMatrica, binaryString, eilutesK);
            var decodedVectors = TransmitAndDecodeVectors(generuojantiMatrica, stulpeliaiN, eilutesK, klaidosTikimybe,
                encodedVectors, paddingCounts, random);
            var finalBinaryString = ReassembleDecodedVectors(decodedVectors);
            var finalText = BinaryToStr(finalBinaryString);
            Console.WriteLine("Galutinis dekoduotas tekstas is kanalo:");
            Console.WriteLine(finalText);
        }

        private static string ReadTextFromFile()
        {
            Console.WriteLine("Tekstas bus nuskaitomas is vietos, kuris bus nurodomas konsoleje...");
            var failoVieta = Console.ReadLine();
            try
            {
                var fileContents = File.ReadAllText(failoVieta!);
                Console.WriteLine("Gautas tekstas:");
                Console.WriteLine(fileContents);
                return fileContents;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Klaida skaitant faila: " + ex.Message);
                Environment.Exit(0);
                return null; // Required by compiler, never reached
            }
        }

        private static (List<int[]>, List<int>) EncodeBinaryString(Matrica generuojantiMatrica, string binaryString,
            int eilutesK)
        {
            var encodedVectors = new List<int[]>();
            var paddingCounts = new List<int>();
            for (var i = 0; i < binaryString.Length; i += eilutesK)
            {
                var segmentLength = Math.Min(eilutesK, binaryString.Length - i);
                var binarySegment = binaryString.Substring(i, segmentLength).PadRight(eilutesK, '0');
                var paddingCount = eilutesK - segmentLength;
                var vector = binarySegment.Select(c => c - '0').ToArray();
                var kodavimas = new Kodavimas(generuojantiMatrica, vector);
                encodedVectors.Add(kodavimas.UzkoduotasVektorius);
                paddingCounts.Add(paddingCount);
            }

            return (encodedVectors, paddingCounts);
        }

        private static IEnumerable<int[]> TransmitAndDecodeVectors(GeneruojantiMatrica generuojantiMatrica,
            int stulpeliaiN, int eilutesK, double klaidosTikimybe, IReadOnlyList<int[]> encodedVectors,
            IReadOnlyList<int> paddingCounts, Random random)
        {
            var decodedVectors = new List<int[]>();
            var dekodavimas = new Dekodavimas(generuojantiMatrica, stulpeliaiN);
            for (var i = 0; i < encodedVectors.Count; i++)
            {
                var kanalas = new Kanalas(klaidosTikimybe, encodedVectors[i], random);
                kanalas.Siusti();
                var receivedVector = kanalas.GautaZinute;
                var decodedVector = dekodavimas.DekoduotiStepByStep(receivedVector).Take(eilutesK - paddingCounts[i])
                    .ToArray();
                decodedVectors.Add(decodedVector);
            }

            return decodedVectors;
        }

        private static IEnumerable<int[]> OnlyTransmit(double klaidosTikimybe, int[] vektorius, Random random)
        {
            var decodedVectors = new List<int[]>();

            var kanalas = new Kanalas(klaidosTikimybe, vektorius, random);
            kanalas.Siusti();

            var receivedVector = kanalas.GautaZinute;
            decodedVectors.Add(receivedVector);

            return decodedVectors;
        }

        private static string ReassembleDecodedVectors(IEnumerable<int[]> decodedVectors)
        {
            return string.Join("", decodedVectors.Select(v => string.Join("", v)));
        }

        private static string BinaryToStr(string binaryString)
        {
            var charList = new List<char>();
            for (var i = 0; i < binaryString.Length; i += 16)
            {
                var binaryChunk = binaryString.Substring(i, 16);
                charList.Add((char)Convert.ToInt32(binaryChunk, 2));
            }

            return new string(charList.ToArray());
        }

        private static string StrToBinary(string fileContents)
        {
            var binaryList = fileContents.Select(c => Convert.ToString(c, 2).PadLeft(16, '0')).ToList();
            return string.Join("", binaryList);
        }
    }
}
