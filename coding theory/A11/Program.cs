using System.Globalization;
using A11.Classes;

int stulpeliaiN, eilutesK;

do {
    // vartotojas suveda n ir k
    Console.WriteLine("Iveskite n... ");
    stulpeliaiN = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Iveskite k... ");
    eilutesK = Convert.ToInt32(Console.ReadLine());

    // tikranama ar validus ivestis i console
    if (stulpeliaiN <= eilutesK)
    {
        Console.WriteLine("n turi buti didesnis uz k. Pakartokite.");
    }
} while (stulpeliaiN <= eilutesK);

// vartotojas pasirenka ar nori gauti matrica atsitiktinai ar suvesti pats
Console.WriteLine("1 - generuojant matrica sukriama atsitiktinai\n2 - generuojancia matrica suvedade Jus...");
var input = Console.ReadLine();

GeneruojantiMatrica generuojantiMatrica = null;

switch(input)
{
    // atsitiktinai gaunama matrica
    case "1":
        generuojantiMatrica = new GeneruojantiMatrica(stulpeliaiN, eilutesK, true);

        Console.WriteLine("Atsitiktinai sugeneruota generuojanti matrica:");
        generuojantiMatrica.Print();

        break;

    // vartotojas suveda generuoajncia matrica, po viena eilute
    case "2":
        generuojantiMatrica = new GeneruojantiMatrica(stulpeliaiN, eilutesK);

        Console.WriteLine("Suveskite standartinio pavidalo generuojancia matrica:");

        for (var i = 0; i < eilutesK; i++)
        {
            Console.WriteLine($"Iveskite {stulpeliaiN} {i+1}-os eilutes elementus...");

            string eilute;
            do
            {
                eilute = Console.ReadLine();

                // tikrinama ar validus ivestis i console
                if (eilute.Length != stulpeliaiN)
                {
                    Console.WriteLine($"Ivesta {eilute.Length} elementu, o turetu buti {stulpeliaiN}. Pakartokite.");
                }
            }
            while (eilute.Length != stulpeliaiN);

            for (var j = 0; j < stulpeliaiN; j++)
            {
                // eilutes paverciamos i skaicius matricoje
                generuojantiMatrica.Duomenys[i, j] = Convert.ToInt32(eilute[j].ToString());
            }
        }
        Console.WriteLine("Jusu ivesta generuojanti matrica:");
        generuojantiMatrica.Print();
        break;

    default:
        // blogam pramatetru pasirinkime, programa baigia darba
        Console.WriteLine("Blogas pasirinkimas");
        Environment.Exit(1);
        break;
}

Console.WriteLine();

// vartotojas iveda zinute
string zinuteInput;
do
{
    Console.WriteLine($"Iveskite ilgio {eilutesK} zinute...");
    zinuteInput = Console.ReadLine();

    // tikrinama ar validus ivestis i console
    if (zinuteInput.Length != eilutesK)
    {
        Console.WriteLine($"Ivesta {zinuteInput.Length} elementu, o turetu buti {eilutesK}. Pakartokite.");
    }
} while (zinuteInput.Length != eilutesK);

// zinute paverciama i skaicius vektoriuje
var zinuteVektorius = zinuteInput.Select(c => c - '0').ToArray();

// zinutes uzkodavimas
var kodas = new Kodavimas(generuojantiMatrica, zinuteVektorius);
Console.WriteLine("Uzkoduota zinute:");
Console.WriteLine(string.Join(", ", kodas.UzkoduotasVektorius));
Console.WriteLine();

// vartotojas iveda klaidos tikimybe
Console.WriteLine("Iveskite klaidos tikimybe (su tasku, pvz.: 0.1)...");
var klaidosTikimybe = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);

// zinutes siuntimas kanalu
Console.WriteLine("Siunciama zinute kanalu...");
Random random = new();
var kanalas = new Kanalas(klaidosTikimybe, kodas.UzkoduotasVektorius, random);
kanalas.Siusti();

Console.WriteLine();

Console.WriteLine("Is kanalo gauta zinute:");
Console.WriteLine(string.Join(", ", kanalas.GautaZinute));
Console.WriteLine();

// Vartotojas pries dekodavima gali pakeisti gauta zinute
Console.WriteLine("Ar norite pakeisti gauta zinute?");
Console.WriteLine("1 - taip, keisti 2 - ne, tesiame dekodavima...");

// zinute is kanalo, kuri galimai turi klaidu
var dekoduojamaZinute = kanalas.GautaZinute;

// vartotojui leidziama pakeisti zinute is kanalo
input = Console.ReadLine();
if(input == "1")
    do
    {
        Console.WriteLine($"Iveskite nauja ilgio {stulpeliaiN} zinute...");
        input = Console.ReadLine();

        // tikrinama ar validus ivestis i console
        if (input!.Length != stulpeliaiN)
        {
            Console.WriteLine($"Ivesta {input.Length} elementu, o turetu buti {stulpeliaiN}. Pakartokite.");
        }
        else
        {
            // zinute paverciama i skaicius vektoriuje
            dekoduojamaZinute = input.Select(c => c - '0').ToArray();
        }

        // tikrinama ar validus ivestis i console
    } while (input.Length != stulpeliaiN);

// zinutes dekodavimas, gaunam pataisyta vektoriu
var dekodavimas = new Dekodavimas(generuojantiMatrica, n: stulpeliaiN);
var dekoduotaZinute = dekodavimas.DekoduotiStepByStep(dekoduojamaZinute);

Console.WriteLine();
Console.WriteLine("Dekoduota zinute:");
Console.WriteLine(string.Join(", ", dekoduotaZinute));

// pataisytu klaidu procento skaiciavimas
var suma = 0;
for (var i = 0; i < stulpeliaiN; i++)
{
    if (dekoduotaZinute[i] == kodas.UzkoduotasVektorius[i])
    {
        suma++;
    }
}

Console.WriteLine($"Pataisytu klaidu procentas: {suma / (double) stulpeliaiN * 100}%");
