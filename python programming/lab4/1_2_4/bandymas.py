import prime_extent

try:
    p = prime_extent.Prime(-20) 
except prime_extent.negative_value_error as e:
    print('issauktas NegativeValueError')

try:
    p = prime_extent.Prime('bbbbb') 
except prime_extent.not_integer_error as e:
    print('issauktas NotIntegerError')

p = prime_extent.Prime(10)
print(p.limit)
print(p.get_primes())

p.limit = 20
print(p.get_primes())

print(p.is_prime())
