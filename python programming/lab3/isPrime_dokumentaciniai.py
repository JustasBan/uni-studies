# dokumentaciniai testai
def is_prime(n):
    """
    Patikrina, ar skaičius n yra pirminis.

    Argumentai:
    n - sveikasis skaičius, kurį norime patikrinti.

    Grąžina:
    True, jei skaičius yra pirminis; False, jei skaičius nėra pirminis.

    Dokumentaciniai testai:
    >>> is_prime(2)
    True
    >>> is_prime(3)
    True
    >>> is_prime(4)
    False
    >>> is_prime(5)
    True
    >>> is_prime(6)
    False
    """
    if n <= 1:
        return False
    for i in range(2, int(n**0.5) + 1):
        if n % i == 0:
            return False
    return True

# ištestuokime dokumentacinius testus
import doctest
doctest.testmod()