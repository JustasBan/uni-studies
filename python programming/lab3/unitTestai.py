import unittest
from isPrime_dokumentaciniai import is_prime

# Unit testu klase
class TestIsPrime(unittest.TestCase):
    def test_is_prime(self):
        self.assertTrue(is_prime(2))
        self.assertTrue(is_prime(3))
        self.assertFalse(is_prime(4))
        self.assertTrue(is_prime(5))
        self.assertFalse(is_prime(6))

# testuojam
if __name__ == "__main__":
    unittest.main()
