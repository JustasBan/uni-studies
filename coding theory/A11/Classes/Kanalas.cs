namespace A11.Classes;

public class Kanalas
{
    Random _random;

    public Kanalas(double tikimybe, int[] siustaZinute, Random random)
    {
        Tikimybe = tikimybe;
        Siusta_zinute = siustaZinute;
        _random = random;
    }

    public double Tikimybe { get; set; }

    public int[] Siusta_zinute { get; set; }

    public int[] Gauta_zinute { get; set; }

    public void Siusti()
    {
        Gauta_zinute = new int[Siusta_zinute.Length];
        for (var i = 0; i < Siusta_zinute.Length; i++)
        {
            Gauta_zinute[i] = Siusta_zinute[i];
            if (RandomInterval() < Tikimybe)
            {
                Gauta_zinute[i] = Siusta_zinute[i] == 0 ? 1 : 0;
            }
        }

        suskaiciuotiKlaidas();
    }

    private void suskaiciuotiKlaidas()
    {
        var klaidos = 0;
        for (var i = 0; i < Siusta_zinute.Length; i++)
        {
            if (Siusta_zinute[i] == Gauta_zinute[i]) continue;
            klaidos++;
            Console.WriteLine($"Klaida {i + 1} pozicijoje");
        }

        Console.WriteLine($"Klaidu skaicius: {klaidos}");
    }

    private double RandomInterval()
    {
        const int precision = 1000000000;
        return _random.Next(0, precision + 1) / (double)precision;
    }
}
