from setuptools import setup, Extension

module = Extension('taskas_ext',
                    sources = ['taskas_ext.c'])

setup(name = 'taskas_ext',
      version = '1.0',
      description = 'Tasko judinimo modulis',
      ext_modules = [module])
