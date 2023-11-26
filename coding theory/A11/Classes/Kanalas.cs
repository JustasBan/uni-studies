namespace A11.Classes;

public class Kanalas
{
    Random _random;

    // Kanalo konstruktorius, kuris priima ir inicializuoja
    // iskraipymo tikimybe, siunciamos zinutes masyva ir c# random objekta
    public Kanalas(double tikimybe, int[] siustaZinute, Random random)
    {
        Tikimybe = tikimybe;
        SiustaZinute = siustaZinute;
        _random = random;
    }

    private double Tikimybe { get; set; }
    public int[] SiustaZinute { get; set; }
    public int[] GautaZinute { get; set; }

    // Metodas, kuris atlieka siuntimo operacija, t.y iskraipo pagal tikimybe
    public void Siusti()
    {
        // gautos zinutes ilgis lygus siunciamos zinutes ilgiui
        GautaZinute = new int[SiustaZinute.Length];

        // iteruojame per visus siunciamos zinutes elementus
        for (var i = 0; i < SiustaZinute.Length; i++)
        {
            GautaZinute[i] = SiustaZinute[i];

            // jeigu atsitiktinis skaicius yra ne mazesnis uz tikimybe, tada nieko nedarome
            if (!(TraukiamasSkaicius() < Tikimybe)) continue;

            // jeigu atsitiktinis skaicius yra mazesnis uz tikimybe, tada iskraipome zinute
            GautaZinute[i] = SiustaZinute[i] == 0 ? 1 : 0;
        }
    }

    // Atspausdina vartotojui kanalo padarytas klaidas ir ju kieki
    public void SuskaiciuotiKlaidas()
    {
        var klaidos = 0;
        for (var i = 0; i < SiustaZinute.Length; i++)
        {
            // jei siunciamos ir gautos zinutes elementai sutampa, tada klaidos nera
            if (SiustaZinute[i] == GautaZinute[i]) continue;

            // esant klaidai spausdiname klaidos pozicija ir jos pokytis
            klaidos++;
            Console.WriteLine($"Klaida {i + 1} pozicijoje. {SiustaZinute[i]} -> {GautaZinute[i]}");
        }

        Console.WriteLine($"Klaidu kiekis: {klaidos}");
    }

    // kadangi c# atsitiktinio skaiciaus generavimas yra [0, 1) intervale,
    // metodas grazina skaiciu is intervalo [0, 1] su 0.000000001 tikslumu
    private double TraukiamasSkaicius()
    {
        const int tikslumas = 1000000000;
        return _random.Next(0, tikslumas + 1) / (double)tikslumas;
    }
}
