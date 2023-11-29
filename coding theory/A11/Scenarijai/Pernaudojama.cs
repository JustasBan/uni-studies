using System.Globalization;
using A11.Classes;

namespace A11.Scenarijai;

public static class Pernaudojama
{
    // paima vektoriu sarasa ir sujungia i viena string
    public static string VektoriaiToStr(IEnumerable<int[]> vektoriai)
    {
        return string.Join("", vektoriai.Select(v => string.Join("", v)));
    }

    // paima generuojancia matrica, n, k ir klaidos tikimybe, bei vektoriu sarasus,
    // kurie buvo uzkoduoti, neuzkoduoti ir vektoriui uzpildymo nuliais kiekius
    // siuncia vektorius ir grazina iskanalo gautus dekoduotus ir nedekoduotus vektorius
    public static (IEnumerable<int[]>, IEnumerable<int[]>) SiustiKanaluIrDekoduoti(
        GeneruojantiMatrica generuojantiMatrica, int stulpeliaiN, int eilutesK, double klaidosTikimybe,
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
            neDekoduotiVektoriai.Add(kanalas.GautaZinute.Take(vektorius.Length - uzplidymai[i]).ToArray());
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
            var dekoduotasVektorius = dekodavimas.DekoduotiStepByStep(gautasVektorius).Take(eilutesK - uzplidymai[i])
                .ToArray();
            dekoduotiVektoriai.Add(dekoduotasVektorius);
        }

        return (dekoduotiVektoriai, neDekoduotiVektoriai);
    }

    // suskaido binary string i vektorius, kuriuos uzkoduojame ir vektorius, kuriuos tik siunciam
    // grazina uzkoduotus vektorius, neuzkoduotus vektorius ir uzpildymo nuliais kiekius
    // paima generuojancia matrica, vektorius string formatu ir k reiksmes
    public static (List<int[]>, List<int>, List<int[]>) SuskaidytiIrUzkoduoti(
        Matrica generuojantiMatrica,
        string binaryString, int eilutesK)
    {
        var uzkoduotiVektoriai = new List<int[]>();
        var neUzkoduotiVektoriai = new List<int[]>();
        var pastumimai = new List<int>();

        // skaidome i vektorius i zinutes ilgio dalimis
        for (var i = 0; i < binaryString.Length; i += eilutesK)
        {
            var vektoriausIlgis = Math.Min(eilutesK, binaryString.Length - i);

            // skaidymas ir uzpildymas nuliais, jei reikia
            var vektoriusString = binaryString.Substring(i, vektoriausIlgis).PadRight(eilutesK, '0');

            // konvertuojame string vektoriu i skaicius
            var vektorius = vektoriusString.Select(c => c - '0').ToArray();

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

    public static double ParseDouble(string @string)
    {
        double skaicius;
        var taskas = CultureInfo.InvariantCulture;
        var kablelis = new CultureInfo("lt-LT");

        // Try parsing with dot first
        if (double.TryParse(@string, NumberStyles.Any, taskas, out skaicius) && skaicius is >= 0 and <= 1)
            return skaicius;

        // Then try with comma
        if (double.TryParse(@string, NumberStyles.Any, kablelis, out skaicius) && skaicius is >= 0 and <= 1)
            return skaicius;

        throw new FormatException("Negalimas skaicius");
    }

}
