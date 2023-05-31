from decimal import Decimal
from django.contrib.auth.models import AbstractUser, User
from django.db import models
from django.core.exceptions import ValidationError
import re
from num2words import num2words
from django.core.validators import RegexValidator
from datetime import datetime


# Django user modelis, praplečiamas papildomais laukais
class CustomUser(AbstractUser):
    name = models.CharField(blank=True, max_length=255)
    surname = models.CharField(blank=True, max_length=255)
    phone_regex = RegexValidator(
        regex=r'^\d{3}-\d{3}-\d{5}$',
        message="Numerio formatas: '370-638-38790'"
    )
    phone_number = models.CharField(
        validators=[phone_regex], max_length=13, blank=True)


# Produktų modelis, kurie priklausys sąskaitos faktūrai
class Product(models.Model):
    name = models.CharField(max_length=200)
    unit_name = models.CharField(max_length=200)
    usage = models.CharField(max_length=400)
    quantity = models.IntegerField()
    summed_cost_without_vat = models.DecimalField(
        max_digits=10, decimal_places=2)
    vat_percentage = models.IntegerField()

    def __str__(self):
        return self.name

    # Validacija, kad teisingi duomenys būtų įvesti
    def clean(self):
        super().clean()

        if self.quantity < 0:
            raise ValidationError('Kiekis negali būti neigiamas.')

        if self.summed_cost_without_vat < 0:
            raise ValidationError('Kaina negali būti neigiama.')

        if self.vat_percentage < 0:
            raise ValidationError('PVM procentai negali būti neigiami.')

        if self.vat_percentage > 100:
            raise ValidationError('PVM procentai negali viršyti 100.')

        if not self.name:
            raise ValidationError('Pavadinimas negali būti tuščias.')

        if not self.unit_name:
            raise ValidationError('Matavimo vienetas negali būti tuščias.')

        if not self.quantity:
            raise ValidationError('Kiekis negali būti tuščias.')

        if not self.usage:
            raise ValidationError('Panaudojimas negali būti tuščias.')

        if not self.summed_cost_without_vat and self.summed_cost_without_vat != 0:
            raise ValidationError('Suma negali būti tuščia.')

        if not self.vat_percentage and self.vat_percentage != 0:
            raise ValidationError('PVM negali būti tuščias.')

    # pagal uzduoties reikalavima, jog kaina = suma / kiekis
    def cost(self):
        return Decimal(self.summed_cost_without_vat) / self.quantity

    # pagal uzduoties reikalavima, jog kaina = suma / kiekis
    def cost_with_vat(self):
        temp = self.total_cost_with_vat()
        return temp / self.quantity

    # PVM sumos skaiciavimas
    def vat_amount(self):
        return Decimal(self.vat_percentage/100) * self.summed_cost_without_vat

    # Pilnos sumos, su PVM, skaiciavimas
    def total_cost_with_vat(self):
        return self.summed_cost_without_vat + self.vat_amount()


# Sąskaitos faktūros modelis
class ProductList(models.Model):
    # Vartotojas, kuris sukūrė sąskaitą faktūrą
    user = models.ForeignKey(CustomUser, on_delete=models.CASCADE)
    # Produktai, kurie priklauso sąskaitai faktūrai
    products = models.ManyToManyField(Product)
    # Sąskaitos faktūros sukūrimo data, automatiškai užpildoma
    created_at = models.DateTimeField(auto_now_add=True)

    # Pirkėjo duomenys
    buyer_company_name = models.CharField(max_length=200)
    buyer_address = models.CharField(max_length=200)
    buyer_code = models.CharField(max_length=200)
    buyer_VAT_code = models.CharField(max_length=200)

    # Sąskaitos faktūros panaudojimo duomenys akte (jei yra)
    is_used = models.BooleanField(default=False)
    usage_act = models.ForeignKey('UsageAct', null=True, blank=True, on_delete=models.SET_NULL, related_name='usage_acts')

    # Pardavėjo duomenys
    seller_company_name = models.CharField(max_length=200)
    seller_address = models.CharField(max_length=200)
    seller_code = models.CharField(max_length=200)
    seller_VAT_code = models.CharField(max_length=200)

    # Validacija, kad teisingi duomenys būtų įvesti
    def clean(self):
        if not self.buyer_company_name:
            raise ValidationError('Įmonės pavadinimas negali būti tuščias.')
        if not self.buyer_address:
            raise ValidationError('Adresas negali būti tuščias.')
        if not self.buyer_code:
            raise ValidationError('Pirkėjo kodas negali būti tuščias.')
        if not self.buyer_VAT_code:
            raise ValidationError('PVM kodas negali būti tuščias.')
        if not self.seller_company_name:
            raise ValidationError('Įmonės pavadinimas negali būti tuščias.')
        if not self.seller_address:
            raise ValidationError('Adresas negali būti tuščias.')
        if not self.seller_code:
            raise ValidationError('Pirkėjo kodas negali būti tuščias.')
        if not self.seller_VAT_code:
            raise ValidationError('PVM kodas negali būti tuščias.')
        if self.usage_act and self.is_used:
            raise ValidationError('Sąskaita faktūra jau panaudota.')

    def __str__(self):
        created_at_formatted = self.created_at.strftime('%Y-%m-%d %H:%M')
        return f'Sąskaita nr. {self.id} sukurta {created_at_formatted}'

    # Visų produktų suma saskaitoje faktūroje
    def total_cost_of_products(self):
        total_cost = 0
        for product in self.products.all():
            total_cost += product.total_cost_with_vat()
        return total_cost

    # Visų produktų suma saskaitoje faktūroje žodžiais PDF failui
    def total_cost_of_products_string(self):
        total_cost = self.total_cost_of_products()
        total_cost_words = num2words(
            total_cost, lang='lt', to='currency', currency='EUR')

        return total_cost_words


# Nurašymo akto modelis
class UsageAct(models.Model):
    # Vartotojas, kuris sukūrė nurašymo aktą
    user = models.ForeignKey(CustomUser, on_delete=models.CASCADE)
    # Sąskaitos faktūros, kurios buvo panaudotos nurašymo akte
    product_lists = models.ManyToManyField(ProductList, related_name='usage_acts')
    # Nurašymo akto sukūrimo data, automatiškai užpildoma
    created_at = models.DateTimeField(auto_now_add=True)

    def __str__(self):
        return f'Nurašymo aktas nr. {self.id}'

    # Visi produktai nurašymo akte
    def all_products(self):
        products = []
        for product_list in self.product_lists.all():
            for product in product_list.products.all():
                products.append(product)
        return products

    # Visų produktų nurašymo akte panaudojimo pazymejimas
    def save(self, *args, **kwargs):
        super().save(*args, **kwargs)
        for product_list in self.product_lists.all():
            product_list.is_used = True
            product_list.save()

    # Visų produktų suma nurašymo akte
    def total_sum(self):
        total = 0
        for product_list in self.product_lists.all():
            for product in product_list.products.all():
                temp = product.total_cost_with_vat()
                total += temp  # assuming this is the cost field
        return total

