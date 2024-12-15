using A11.Classes.Matricos;

namespace A11.Classes;

public class Kodavimas
{
    // Kodavimo konstruktorius, kuris uzkoduoja vektoriu,
    // t.y sudauginus generuojancia matrica su siunciamu vektoriumi
    // priima generuojancia matrica ir siunciamaji vektoriu
    public Kodavimas(Matrica? generuojantiMatrica, int[] siunciamasVektorius)
    {
        UzkoduotasVektorius = OperacijosMatricos.Daugyba(generuojantiMatrica, siunciamasVektorius);
    }

    public int[] UzkoduotasVektorius { get; set; }
}
