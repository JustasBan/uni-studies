using A11.Classes;

namespace A11.Services;

public static class OperacijosMatricos
{
    public static Matrica Suliejimas(Matrica A, Matrica B)
    {
        var eilutes = A.Eilutes_k;
        var stulpeliai1 = A.Stulpeliai_n;
        var stulpeliai2 = B.Stulpeliai_n;

        var sulietaMatrica = new int[eilutes, stulpeliai1 + stulpeliai2];

        for (var i = 0; i < eilutes; i++)
        {
            for (var j = 0; j < stulpeliai1; j++)
            {
                sulietaMatrica[i, j] = A.Duomenys[i, j];
            }
        }

        for (var i = 0; i < eilutes; i++)
        {
            for (var j = 0; j < stulpeliai2; j++)
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

    public static int[] Daugyba(Matrica A, int[] B)
    {
        int k = A.Eilutes_k;
        int n = A.Stulpeliai_n;

        int[] rezultatas = new int[n];

        for (int i = 0; i < n; i++)
        {
            rezultatas[i] = 0;
            for (int j = 0; j < k; j++)
            {
                rezultatas[i] ^= B[j] * A.Duomenys[j, i];
            }
        }

        return rezultatas;
    }
}
