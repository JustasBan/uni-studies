using A11.Services;

namespace A11.Classes;

public class Kodavimas
{
    public Kodavimas(Matrica generuojantiMatrica, int[] siunciamasVektorius)
    {
        UzkoduotasVektorius = OperacijosMatricos.Daugyba(generuojantiMatrica, siunciamasVektorius);
    }

    public int[] UzkoduotasVektorius { get; set; }
}
