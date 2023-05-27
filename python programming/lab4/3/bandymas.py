import taskas_ext

# Tasko objektas
t = taskas_ext.Taskas(2, 3)

# Pradine busena
print(f"Initial position: ({t.x}, {t.y})")

# move
t.move(-5, -7)

# nauja busena
print(f"New position: ({t.x}, {t.y})")