using A11.Classes.Matricos;
using A11.Classes.Sindromai;

namespace A11.Classes;

public class Dekodavimas
{
    // Dekodavimo konsruktorius, kuris priima generuojancia matrica ir n.
    // Paruosia dekodavimui: pagal argumentus gauna kontroline matrica, ja transponuoja, gauna sindromus su svoriais
    // ir inicializuoja dekodavima.
    public Dekodavimas(GeneruojantiMatrica generuojantiMatrica, int n)
    {
        this.n = n;
        KontrolineMatrica = new KontrolineMatrica(generuojantiMatrica)
        {
            // Literaturos pavyzdys:
            // Duomenys = new[,]
            // {
            //     { 1,0,0,1,0,1 },
            //     { 0,1,0,1,1,0 },
            //     { 0,0,1,0,1,1 }
            // }
        };
        KontrolineMatrica.Transponuoti();

        SindromaiSvoriai = new LyderiuLentele(KontrolineMatrica, n).SindromaiSvoriai;
    }

    private readonly int n;
    private KontrolineMatrica KontrolineMatrica { get; set; }
    private Dictionary<string, int> SindromaiSvoriai { get; set; }

    // Pritaiko "Grandininio" dekodavimo algoritma
    // priima uzkoduota zinute, grazina dekoduota zinute
    public int[] DekoduotiStepByStep(int[] uzkoduotaZinute)
    {
        var dekoduotaZinute = uzkoduotaZinute;

        // iteruojame per visus vektoriaus elementus
        for (var i = 0; i < n; i++)
        {
            // apskaiciuojame sena sindroma
            var senasSindromas = OperacijosMatricos.Daugyba(b: dekoduotaZinute, a: KontrolineMatrica);
            var senoSindromoString = string.Join("", senasSindromas);

            // tarpiniams rezultatams tikrinti
            //Console.WriteLine($"{senoSindromoString} - {string.Join("", dekoduotaZinute)}");

            // jei sindromas lygus nuliui, tai dekodavimas baigtas, klaidos istaisytos
            if (senasSindromas.Sum() == 0)
            {
                return dekoduotaZinute;
            }

            // jei sindromas nelygus nuliui, tai ieskome tokio sindromo lenteleje ir pasiimame svori
            var senasSvoris = SindromaiSvoriai[senoSindromoString];

            // pakeiciame i-aji vektoriaus elementa
            dekoduotaZinute[i] = dekoduotaZinute[i] == 0 ? 1 : 0;

            // apskaiciuojame nauja sindroma
            var naujasSindromas = OperacijosMatricos.Daugyba(b: dekoduotaZinute, a: KontrolineMatrica);
            var naujasSindromoString = string.Join("", naujasSindromas);

            // ieskome naujo sindromo lenteleje ir pasiimame svori
            var naujasSvoris = SindromaiSvoriai[naujasSindromoString];

            // jei naujas svoris mazesnis uz sena, tai paliekame pakeista elementa,
            // nes "pakilome" lenteleje
            if(senasSvoris > naujasSvoris)
            {
                continue;
            }

            // jei naujas svoris didesnis arba lygus senam,
            // tai graziname elementa i pradine reiksme
            dekoduotaZinute[i] = dekoduotaZinute[i] == 0 ? 1 : 0;
        }

        return dekoduotaZinute;
    }
}
