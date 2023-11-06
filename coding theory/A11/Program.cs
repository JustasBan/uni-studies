﻿using A11.Models;

int n, k;

Console.WriteLine("Iveskite n... ");
n = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("Iveskite k... ");
k = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("1 - generuojant matrica sugeneruojama atsitiktinai\n2 - generuojancia matrica suvedade Jus...");

var input = Console.ReadLine();
var generuojantiMatrica = new GeneruojantiMatrica();

switch(input)
{
    case "1":
        generuojantiMatrica = new GeneruojantiMatrica(n, k, true);
        Console.WriteLine("Atsitiktinai sugeneruota generuojanti matrica:");
        generuojantiMatrica.Print();
        break;
    case "2":
        generuojantiMatrica = new GeneruojantiMatrica(n, k, false);
        Console.WriteLine("Suveskite matrica A, kuri bus prijungta prie vienetines matricos...");
        for (var i = 0; i < k; i++)
        {
            Console.WriteLine($"Iveskite {n-k} {i}-os eilutes elementus...");
            var line = Console.ReadLine();
            for (var j = 0; j < n-k; j++)
            {
                generuojantiMatrica.Duomenys[i, j+k] = Convert.ToInt32(line[j].ToString());
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
generuojantiMatrica.Print();
