﻿using System.Globalization;
using A11.Classes;
using A11.Scenarijai;

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

// vartotojas iveda klaidos tikimybe
Console.WriteLine("Iveskite klaidos tikimybe (su tasku, pvz.: 0.1)...");
var klaidosTikimybe = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);

Console.WriteLine();

// vartotojas pasirenka scenariju
Console.WriteLine("Kuri scenariju norite paleisti? Galimi: 1, 2");
input = Console.ReadLine();
switch (input)
{
    case "1":
        Scenarijus1 scenarijus1 = new();
        scenarijus1.Vykdyti(generuojantiMatrica, stulpeliaiN, eilutesK, klaidosTikimybe);
        break;
    case "2":
        Scenarijus2 scenarijus2 = new();
        scenarijus2.Vykdyti(generuojantiMatrica, stulpeliaiN, eilutesK, klaidosTikimybe);
        break;

    default:
    // blogam pramatetru pasirinkime, programa baigia darba
    Console.WriteLine("Blogas pasirinkimas");
    Environment.Exit(1);
    break;
}
