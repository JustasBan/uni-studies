namespace A11.Classes;

public class Matrica
{
    public int Stulpeliai_n { get; set; }
    public int Eilutes_k { get; set; }
    public int[,] Duomenys { get; set; }

    public void Print()
    {
        for (var i = 0; i < Eilutes_k; i++)
        {
            for (var j = 0; j < Stulpeliai_n; j++)
            {
                Console.Write(Duomenys[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    public void Transponuoti()
    {
        var transposed = new int[Stulpeliai_n, Eilutes_k];

        for (var i = 0; i < Eilutes_k; i++)
        {
            for (var j = 0; j < Stulpeliai_n; j++)
            {
                transposed[j, i] = Duomenys[i, j];
            }
        }

        Duomenys = transposed;
        (Stulpeliai_n, Eilutes_k) = (Eilutes_k, Stulpeliai_n);
    }

    public void Vienetine()
    {
        Duomenys = new int[Eilutes_k, Eilutes_k];
        Stulpeliai_n = Eilutes_k;

        for (var i = 0; i < Eilutes_k; i++)
        {
            for (var j = 0; j < Eilutes_k; j++)
            {
                Duomenys[i, j] = 0;
                if(i == j) Duomenys[i, j] = 1;
            }
        }
    }
}
