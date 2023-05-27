from setuptools import setup, Extension

module = Extension('prime_extent',
                    sources = ['prime_extent.c'])

setup(name = 'prime_extent',
      version = '1.0',
      description = 'Pirminiu skaiciu modulis',
      ext_modules = [module])
