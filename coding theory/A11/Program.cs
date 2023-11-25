using System.Globalization;
using A11.Classes;

int stulpeliai, eiltutes;

do {
    /* vartotojas suveda n ir k */
    Console.WriteLine("Iveskite n... ");
    stulpeliai = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Iveskite k... ");
    eiltutes = Convert.ToInt32(Console.ReadLine());

    if (stulpeliai <= eiltutes)
    {
        Console.WriteLine("n turi buti didesnis uz k");
    }
} while (stulpeliai <= eiltutes);

/* vartotojas pasirenka ar nori gauti matrica atsitiktinai ar suvesti pats */
Console.WriteLine("1 - generuojant matrica sukriama atsitiktinai\n2 - generuojancia matrica suvedade Jus...");

var input = Console.ReadLine();
GeneruojantiMatrica generuojantiMatrica = null;

switch(input)
{
    // atsitiktinai gaunama matrica
    case "1":
        generuojantiMatrica = new GeneruojantiMatrica(stulpeliai, eiltutes, true);
        Console.WriteLine("Atsitiktinai sugeneruota generuojanti matrica:");
        generuojantiMatrica.Print();
        break;

    // vartotojas suveda generuoajncia matrica
    case "2":
        generuojantiMatrica = new GeneruojantiMatrica(stulpeliai, eiltutes);
        Console.WriteLine("Suveskite standartinio pavidalo generuojancia matrica:");
        for (var i = 0; i < eiltutes; i++)
        {
            Console.WriteLine($"Iveskite {stulpeliai} {i+1}-os eilutes elementus...");

            string line;
            do
            {
                line = Console.ReadLine();
                if (line.Length != stulpeliai)
                {
                    Console.WriteLine($"Ivesta {line.Length} elementu, o turetu buti {stulpeliai}");
                }
            }
            while (line.Length != stulpeliai);

            for (var j = 0; j < stulpeliai; j++)
            {
                // vartotojo ivesta eilute paverciama i skaicius matricos eiluteje
                generuojantiMatrica.Duomenys[i, j] = Convert.ToInt32(line[j].ToString());
            }
        }
        Console.WriteLine("Jusu ivesta generuojanti matrica:");
        generuojantiMatrica.Print();
        break;

    default:
        Console.WriteLine("Blogas pasirinkimas");
        Environment.Exit(1);
        break;
}

Console.WriteLine();

/* vartotojas iveda zinute */

string zinuteInput;
do
{
    Console.WriteLine($"Iveskite ilgio {eiltutes} zinute...");
    zinuteInput = Console.ReadLine();
    if (zinuteInput.Length != eiltutes)
    {
        Console.WriteLine($"Ivesta {zinuteInput.Length} elementu, o turetu buti {eiltutes}");
    }
} while (zinuteInput.Length != eiltutes);


var zinuteVektorius = zinuteInput.Select(c => c - '0').ToArray();

/* zinutes uzkodavimas */
var kodas = new Kodavimas(generuojantiMatrica, zinuteVektorius);

Console.WriteLine("Uzkoduota zinute:");
Console.WriteLine(string.Join(',', kodas.UzkoduotasVektorius));
Console.WriteLine();

/* vartotojas iveda klaidos tikimybe ir su ja siunciama kanalu*/
Console.WriteLine("Iveskite klaidos tikimybe (su tasku, pvz.: 0.1)...");
var klaidosTikimybe = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);

Console.WriteLine("Siunciama zinute kanalu...");
Random random = new();
var kanalas = new Kanalas(klaidosTikimybe, kodas.UzkoduotasVektorius, random);
kanalas.Siusti();
Console.WriteLine();

Console.WriteLine("Is kanalo gauta zinute:");
Console.WriteLine(string.Join(", ", kanalas.Gauta_zinute));
Console.WriteLine();

/* Vartotojas pries dekodavima gali pakeisti gauta zinute */
Console.WriteLine("Ar norite pakeisti gauta zinute?");
Console.WriteLine("1 - taip, keisti 2 - ne, tesiame dekodavima...");

var dekoduojamaZinute = kanalas.Gauta_zinute;

input = Console.ReadLine();

if(input == "1")
    do
    {
        Console.WriteLine($"Iveskite nauja ilgio {stulpeliai} zinute...");
        input = Console.ReadLine();

        if (input!.Length != stulpeliai)
        {
            Console.WriteLine($"Ivesta {input.Length} elementu, o turetu buti {stulpeliai}");
        }
        else
        {
            dekoduojamaZinute = input.Select(c => c - '0').ToArray();
        }
    } while (input.Length != stulpeliai);

/* zinutes dekodavimas */
var dekodavimas = new Dekodavimas(generuojantiMatrica, k: eiltutes, n: stulpeliai);
