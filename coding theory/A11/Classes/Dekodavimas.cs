using A11.Classes.Matricos;
using A11.Classes.Sindromai;

namespace A11.Classes;

public class Dekodavimas
{
    public Dekodavimas(GeneruojantiMatrica generuojantiMatrica, int n, int k)
    {
        KontrolineMatrica = new KontrolineMatrica(generuojantiMatrica);

        var LyderiuLentele = new LyderiuLentele(generuojantiMatrica, n, k);
        LyderiuLentele.Print();
    }

    private KontrolineMatrica KontrolineMatrica { get; set; }
}
