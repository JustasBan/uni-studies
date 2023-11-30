namespace A11.Classes.Matricos;

public class Matrica
{
    // matricos parametrai
    public int StulpeliaiN { get; set; }
    public int EilutesK { get; set; }

    // pati matrica
    public int[,] Duomenys { get; set; }

    // matricos isvedimas i ekrana
    public void Print()
    {
        for (var i = 0; i < EilutesK; i++)
        {
            for (var j = 0; j < StulpeliaiN; j++)
            {
                Console.Write(Duomenys[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    // matricos transponavimas
    public void Transponuoti()
    {
        // transponuotos matricos parametru pakeitimas
        var transposed = new int[StulpeliaiN, EilutesK];

        // transponavimas,
        // iteruojama per kiekviena matricos eilute
        for (var i = 0; i < EilutesK; i++)
        {
            // iteruojama per kiekviena matricos stulpeli
            for (var j = 0; j < StulpeliaiN; j++)
            {
                // stulpeliai tampa eilutemis
                transposed[j, i] = Duomenys[i, j];
            }
        }

        // transponuotos matricos parametru pakeitimas
        Duomenys = transposed;
        (StulpeliaiN, EilutesK) = (EilutesK, StulpeliaiN);
    }

    // matricos pavertimas vienetine
    public void Vienetine()
    {
        // vienetine matrica yra k*k
        Duomenys = new int[EilutesK, EilutesK];
        StulpeliaiN = EilutesK;

        for (var i = 0; i < EilutesK; i++)
        {
            for (var j = 0; j < EilutesK; j++)
            {
                // vienetine matrica turi 1 tik sutapus eilutei ir stulpeliui, kitur 0
                Duomenys[i, j] = 0;
                if(i == j) Duomenys[i, j] = 1;
            }
        }
    }
}
