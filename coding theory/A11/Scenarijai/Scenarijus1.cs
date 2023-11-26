using System.Globalization;
using A11.Classes;

namespace A11.Scenarijai;

public class Scenarijus1
{
    public void Vykdyti(GeneruojantiMatrica generuojantiMatrica, int stulpeliaiN, int eilutesK, double klaidosTikimybe)
    {
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

        // zinutes siuntimas kanalu
        Console.WriteLine("Siunciama zinute kanalu...");
        Random random = new();
        var kanalas = new Kanalas(klaidosTikimybe, kodas.UzkoduotasVektorius, random);
        kanalas.Siusti();
        kanalas.SuskaiciuotiKlaidas();

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
        var input = Console.ReadLine();
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

        Console.WriteLine($"Originalaus ir dekoduoto vektoriu atitikmuo: {suma / (double) stulpeliaiN * 100}%");
    }
}
