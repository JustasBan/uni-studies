namespace A11.Classes.Matricos;

public class GeneruojantiMatrica : Matrica
{
    // konstruktorius, skirtas atsitiktiniam generuojancios matricos sugeneravimui ir inicializavimui
    // paima n ir k parametrus ir ar reikia atsitiktinai sugeneruoti matrica
    public GeneruojantiMatrica(int stulpeliaiN, int eilutesK, bool generateRandomly)
    {
        // n ir k parametru inicializacija
        StulpeliaiN = stulpeliaiN;
        EilutesK = eilutesK;

        // matricos inicializacija
        Duomenys = new int[eilutesK, stulpeliaiN];

        // atsitiktinai sugeneruojama matrica, pasirenkant varotojui
        if (generateRandomly)
        {
            var random = new Random();

            // atsitiktinai sugeneruojamas skaicius 0 arba 1 kiekvienam matricos elementui
            for (var i = 0; i < eilutesK; i++)
            {
                for (var j = 0; j < stulpeliaiN; j++)
                {
                    Duomenys[i, j] = random.Next(2);
                }
            }
        }

        // generuojancios matricos kaire puse paverciama vienetine,
        // nes dirbama tik su standartiniu pavidalu
        for (var i = 0; i < eilutesK; i++)
        {
            for (var j = 0; j < eilutesK; j++)
            {
                Duomenys[i, j] = 0;
                if(i == j) Duomenys[i, j] = 1;
            }
        }
    }

    // konstruktorius, skirtas vartotojo suvestai matricai inicializuoti
    // paima n ir k parametrus
    public GeneruojantiMatrica(int stulpeliaiN, int eilutesK)
    {
        StulpeliaiN = stulpeliaiN;
        EilutesK = eilutesK;
        Duomenys = new int[eilutesK, stulpeliaiN];
    }

    // grazina generuojancios matricos desne (ne vienetine) puse,
    // skirta kontrolines matricos generavimui
    public int[,] IsgautiA()
    {
        // desnes puses matricos parametrai
        var desnePuse = new int[EilutesK, StulpeliaiN-EilutesK];

        // isgaunama desne (ne vienetine) puse
        for (var i = 0; i < EilutesK; i++)
        {
            // isgaunama iki ne vienetines dalies pabaigos
            for (var j = 0; j < StulpeliaiN-EilutesK; j++)
            {
                desnePuse[i, j] = Duomenys[i, j+EilutesK];
            }
        }

        return desnePuse;
    }
}
