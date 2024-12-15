namespace A11.Classes.Matricos;

public static class OperacijosMatricos
{

    // priima dvi matricas ir grazina nauja matrica, kuri yra sulieta is tu dvieju matricu
    public static Matrica Suliejimas(Matrica a, Matrica b)
    {
        // suliejame tik vienodo eiluciu kiekio matricas
        var eilutes = a.EilutesK;

        // stulpeliu skaicius naujoje matricoje bus lygus dvieju matricu stulpeliu sumai
        var stulpeliai1 = a.StulpeliaiN;
        var stulpeliai2 = b.StulpeliaiN;
        var sulietaMatrica = new int[eilutes, stulpeliai1 + stulpeliai2];

        // kopijuojame pirma matrica i naujos matricos kaire puse
        for (var i = 0; i < eilutes; i++)
        {
            for (var j = 0; j < stulpeliai1; j++)
            {
                sulietaMatrica[i, j] = a.Duomenys[i, j];
            }
        }

        // kopijuojame antra matrica i naujos matricos antra puse
        for (var i = 0; i < eilutes; i++)
        {
            for (var j = 0; j < stulpeliai2; j++)
            {
                // "pastumiame" stulpeliu numerius, kadangi antra matrica bus desineje
                sulietaMatrica[i, j + stulpeliai1] = b.Duomenys[i, j];
            }
        }

        return new Matrica
        {
            Duomenys = sulietaMatrica,
            EilutesK = eilutes,
            StulpeliaiN = stulpeliai1 + stulpeliai2
        };
    }

    // priima matrica ir vektoriu.
    // grazina vektoriu, kuris yra matricos ir vektoriaus daugybos rezultatas
    public static int[] Daugyba(Matrica? a, int[] b)
    {
        var k = a.EilutesK;
        var n = a.StulpeliaiN;

        var rezultatas = new int[n];

        // iteruojame per kiekviena matricos stulpeli
        for (var i = 0; i < n; i++)
        {
            // iteruojame per kiekviena matricos eilute
            rezultatas[i] = 0;
            for (var j = 0; j < k; j++)
            {
                // dauginame matricos stulpeli su vektoriaus elementu
                // ir pridedame prie rezultato (XOR, nes binarinis)
                rezultatas[i] ^= b[j] * a.Duomenys[j, i];
            }
        }

        return rezultatas;
    }
}
