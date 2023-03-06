#Justas Baniulis 2015956

import numpy as np
import sympy as sym
import matplotlib.pyplot as plt

#1.	Padalinkite intervalą nuo -1.3 iki 2.5 tolygiai į 64 dalis.
def padalinimas(start=-1.3, end=2.5, partition=64):
    return np.linspace(start, end, partition)
#print(padalinimas())


#2. Sukonstruokite pasikartojantį masyvą pagal duotą N.
"""
Duotas masyvas [1, 2, 3, 4] ir N = 3
Rezultatas [1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4]
Masyvas gali būti bet kokio dydžio ir atsitiktinai sugeneruojamas.
"""
def pasikartojimas1(arr, N):
    return np.tile(arr, (1, N))
#print(pasikartojimas1([1,2,3,4], 3))


#3.	Sukurkite masyvą iš pasikartojančių elementų.
"""
Duotas skaičius 3 ir pasikartojimų skaičius 4.
Rezultatas [3, 3, 3, 3]
"""
def pasikartojimas2(number, repeat):
    return np.full((repeat,), number)
#print(pasikartojimas2(3, 4))


#4.	Sukurkite masyvą dydžio 10 x 10 iš nulių "įrėmintų" vienetais.
"""
Užuomina - pad.
"""
def ireminimas():
    arr = np.zeros((10, 10), dtype=int)
    arr = np.pad(arr, pad_width=1, mode='constant', constant_values=1)
    return arr
#print(ireminimas())


#5.	Sukurkite masyvą dydžio 8 x 8, kur 1 ir 0 išdėlioti šachmatine tvarka.
def sachmatai(n=8):
    indices = np.arange(n * n).reshape(n, n)
    arr = np.bitwise_xor(indices // n, indices % n) % 2
    return arr
#print(sachmatai())


#6.	Sukurkite masyvą dydžio n×n , kurio (i,j)-oji pozicija lygi i+j.
def pozicijuSuma(n):
    arr = np.fromfunction(lambda i, j: i+j, (n,n), dtype=int)
    return arr
#print(pozicijuSuma(5))


#7.	Sukurkite atsitiktinį masyvą dydžio 5×5 naudodami np.random.rand(5, 5). Surūšiuokite eilutes pagal antrąjį stulpelį. 
def surusiuotasPagalAntraStulpeliAtsitiktinisMasyvas():
    arr = np.random.rand(5,5)
    cols = arr[:, 1]
    indices = np.argsort(cols)
    return arr[indices]
#print(surusiuotasPagalAntraStulpeliAtsitiktinisMasyvas())


#8.	Apskaičiuokite matricos tikrines reikšmes ir tikrinį vektorių.
def matricosTikrinesReiksmesSuVektoriais(matrix):
    val, vec = np.linalg.eig(matrix)
    return val, vec
#print(matricosTikrinesReiksmesSuVektoriais(np.array([[1, 2], [3, 4]])))


#9.	Apskaičiuokite funkcijos 0.5*x**2 + 5 * x + 4 išvestines su numpy ir sympy paketais.
def funkcijosIsvestines():
    #numpy:
    f1 = np.poly1d([0.5, 5, 4])
    f1_prime = np.poly1d.deriv(f1)
    d1 = f1_prime(1)

    #sympy:
    x = sym.Symbol('x')
    f2 = 0.5*x**2 + 5*x + 4
    f2_prime = sym.diff(f2, x)
    d2 = f2_prime.evalf(subs={x: 1})

    return d1, d2
#print(funkcijosIsvestines())


#10. Apskaičiuokite funkcijos e-x apibrėžtinį, intervale [0,1], ir neapibrėžtinį integralus.
def exIntegralai():
    x = sp.Symbol('x')
    f = sp.exp(-x)
    neInteg = sp.integrate(f, x)
    apInteg = sp.integrate(f, (x, 0, 1))
    
    return apInteg, neInteg
#print(exIntegralai())


#11. Pasinaudodami polinėmis koordinatėmis nupieškite kardioidę.
def kardioide():
    t = np.linspace(0, 2*np.pi, 1000)
    r = 1 + np.cos(t)

    x = r * np.cos(t)
    y = r * np.sin(t)

    fig, ax = plt.subplots()
    ax.plot(x, y, color='red')
    ax.set_aspect('equal')
    ax.set_title('kardioide')
    plt.show()
#kardioide()


#12. Sugeneruokite masyvą iš 1000 atsitiktinių skaičių, pasiskirsčiusių pagal normalųjį dėsnį su duotais vidurkiu V ir dispersija D. Nupieškite jų histogramą.
def masyvas2(v=5, d=3):
    arr = np.random.normal(v, np.sqrt(d), 1000)

    fig, ax = plt.subplots()
    ax.hist(arr, bins=30, density=True, alpha=0.7, color='blue')

    ax.set_xlabel('Reiksme')
    ax.set_ylabel('daznis')
    ax.set_title('Histograma')
    plt.show()
#masyvas2()