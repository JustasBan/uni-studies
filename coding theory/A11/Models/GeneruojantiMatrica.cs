using System.Security.Cryptography;

namespace A11.Models;

public class GeneruojantiMatrica
{
    public GeneruojantiMatrica(int n, int k, bool generateRandomly)
    {
        Random random = new Random();

        this.n = n;
        this.k = k;

        Duomenys = new int[k, n];

        if (generateRandomly)
        {
            for (var i = 0; i < k; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    Duomenys[i, j] = random.Next(2);
                }
            }
        }

        for (var i = 0; i < k; i++)
        {
            for (var j = 0; j < n-k; j++)
            {
                Duomenys[i, j] = 0;
                if(i == j) Duomenys[i, j] = 1;
            }
        }
    }

    public GeneruojantiMatrica()
    {
    }

    public int n { get; set; }
    public int k { get; set; }

    public int[,] Duomenys { get; set; }

    public void Print()
    {
        for (int i = 0; i < k; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(Duomenys[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
