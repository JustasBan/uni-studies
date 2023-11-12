namespace A11.Classes;

public class Kanalas
{
    Random random = new();

    public Kanalas(double tikimybe, int[] siustaZinute)
    {
        Tikimybe = tikimybe;
        Siusta_zinute = siustaZinute;
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
            if (random.NextDouble() < Tikimybe)
            {
                Gauta_zinute[i] = Siusta_zinute[i] == 0 ? 1 : 0;
            }
        }
    }
}
