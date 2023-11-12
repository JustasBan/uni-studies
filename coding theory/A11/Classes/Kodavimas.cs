using A11.Services;

namespace A11.Classes;

public class Kodavimas
{
    public Kodavimas(Matrica generuojantiMatrica, int[] siunciamasVektorius)
    {
        Uzkoduotas_vektorius = OperacijosMatricos.Daugyba(generuojantiMatrica, siunciamasVektorius);
    }

    public int[] Uzkoduotas_vektorius { get; set; }
}
