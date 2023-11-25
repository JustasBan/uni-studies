namespace A11.Classes.Matricos;

public class KontrolineMatrica : Matrica
{
    // konsruktorius, kuris priima generuojancia matrica
    // ir pagal ja inicializuoja kontroline matrica
    public KontrolineMatrica(GeneruojantiMatrica generuojantiMatrica)
    {
        // generuojancios matricos desne (ne vienetine) puse.
        // paverciama atskira matrica
        Matrica dalisA = new()
        {
            Duomenys = generuojantiMatrica.IsgautiA(),

            // kontrolines matricos parametru skaiciavimas
            StulpeliaiN = generuojantiMatrica.StulpeliaiN - generuojantiMatrica.EilutesK,
            EilutesK = generuojantiMatrica.EilutesK
        };

        // transponuojama desne (ne vienetine) puse,
        // nes dirbama su standartiniu pavidalu
        dalisA.Transponuoti();

        // kontrolines matricos parametru skaiciavimas
        StulpeliaiN = generuojantiMatrica.StulpeliaiN;
        EilutesK = dalisA.EilutesK;
        Duomenys = new int[EilutesK,StulpeliaiN];

        // generuojancios matricos desne (vienetine) puse.
        // paverciama atskira matrica
        Matrica dalisVienetine = new()
        {
            // vienetinei matricai reikia tik k parametro, nes ji k*k
            EilutesK = generuojantiMatrica.StulpeliaiN-generuojantiMatrica.EilutesK
        };
        dalisVienetine.Vienetine();

        // suliejama vienetine puse ir transponuota kaire (ne vienetine) puse
        // i viena matrica
        Duomenys = OperacijosMatricos.Suliejimas(dalisA, dalisVienetine).Duomenys;
    }
}
