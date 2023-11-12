using A11.Services;

namespace A11.Classes;

public class KontrolineMatrica : Matrica
{
    public KontrolineMatrica(GeneruojantiMatrica generuojantiMatrica)
    {
        Matrica dalisA = new()
        {
            Duomenys = generuojantiMatrica.getA(),
            Stulpeliai_n = generuojantiMatrica.Stulpeliai_n - generuojantiMatrica.Eilutes_k,
            Eilutes_k = generuojantiMatrica.Eilutes_k
        };

        dalisA.Transponuoti();

        Stulpeliai_n = generuojantiMatrica.Stulpeliai_n;
        Eilutes_k = dalisA.Eilutes_k;
        Duomenys = new int[Eilutes_k,Stulpeliai_n];

        Matrica dalisVienetine = new()
        {
            Eilutes_k = generuojantiMatrica.Stulpeliai_n-generuojantiMatrica.Eilutes_k
        };
        dalisVienetine.Vienetine();

        Duomenys = OperacijosMatricos.Suliejimas(dalisA, dalisVienetine).Duomenys;
    }
}
