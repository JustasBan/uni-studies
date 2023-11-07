using A11.Classes;

namespace A11.Services;

public static class OperacijosMatricos
{
    public static Matrica Suliejimas(Matrica A, Matrica B)
    {
        int eilutes = A.Eilutes_k;
        int stulpeliai1 = A.Stulpeliai_n;
        int stulpeliai2 = B.Stulpeliai_n;

        int[,] sulietaMatrica = new int[eilutes, stulpeliai1 + stulpeliai2];

        for (int i = 0; i < eilutes; i++)
        {
            for (int j = 0; j < stulpeliai1; j++)
            {
                sulietaMatrica[i, j] = A.Duomenys[i, j];
            }
        }

        for (int i = 0; i < eilutes; i++)
        {
            for (int j = 0; j < stulpeliai2; j++)
            {
                sulietaMatrica[i, j + stulpeliai1] = B.Duomenys[i, j];
            }
        }

        return new Matrica
        {
            Duomenys = sulietaMatrica,
            Eilutes_k = eilutes,
            Stulpeliai_n = stulpeliai1 + stulpeliai2
        };
    }
}
