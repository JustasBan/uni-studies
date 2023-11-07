namespace A11.Classes;

public class GeneruojantiMatrica : Matrica
{
    public GeneruojantiMatrica(int stulpeliaiN, int eilutesK, bool generateRandomly)
    {
        var random = new Random();

        Stulpeliai_n = stulpeliaiN;
        Eilutes_k = eilutesK;

        Duomenys = new int[eilutesK, stulpeliaiN];

        if (generateRandomly)
        {
            for (var i = 0; i < eilutesK; i++)
            {
                for (var j = 0; j < stulpeliaiN; j++)
                {
                    Duomenys[i, j] = random.Next(2);
                }
            }
        }

        for (var i = 0; i < eilutesK; i++)
        {
            for (var j = 0; j < eilutesK; j++)
            {
                Duomenys[i, j] = 0;
                if(i == j) Duomenys[i, j] = 1;
            }
        }
    }

    public GeneruojantiMatrica()
    {
    }

    public int[,] getA()
    {
        var A = new int[Eilutes_k, Stulpeliai_n-Eilutes_k];

        for (var i = 0; i < Eilutes_k; i++)
        {
            for (var j = 0; j < Stulpeliai_n-Eilutes_k; j++)
            {
                A[i, j] = Duomenys[i, j+Eilutes_k];
            }
        }

        return A;
    }
}
