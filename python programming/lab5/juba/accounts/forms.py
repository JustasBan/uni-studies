from django import forms
from django.contrib.auth.forms import UserCreationForm, AuthenticationForm
from .models import CustomUser, Product, ProductList, UsageAct

# Naudojamos formos

# Vartotojo registracijos forma su papildomais laukais
class CustomUserCreationForm(UserCreationForm):
    class Meta(UserCreationForm.Meta):
        model = CustomUser
        fields = UserCreationForm.Meta.fields + \
            ('name', 'surname', 'phone_number')
        labels = {
            'name': 'Vardas',
            'surname': 'Pavarde',
            'phone_number': 'Telefono numeris'
        }


# Vartotojo prisijungimo forma
class CustomAuthenticationForm(AuthenticationForm):
    class Meta:
        model = CustomUser
        fields = ('username', 'password',)


# Forma produktui pridėti į sąskaitos faktūrą
class ProductForm(forms.ModelForm):
    class Meta:
        model = Product
        fields = ['name', 'unit_name', 'quantity',
                  'summed_cost_without_vat', 'vat_percentage', 'usage']
        labels = {
            'name': 'Vardas',
            'unit_name': 'Vnt. pavadinimas',
            'quantity': 'kiekis',
            'summed_cost_without_vat': 'suma (be pvm)',
            'vat_percentage': 'PVM procentai',
            'usage': 'Panaudojimas'
        }


# Forma sąskaitos faktūrai pridėti su pirkėjo ir pardavėjo duomenimis
class ProductListForm(forms.ModelForm):
    class Meta:
        model = ProductList
        fields = ['buyer_company_name', 'buyer_address', 'buyer_code', 'buyer_VAT_code',
                  'seller_company_name', 'seller_address', 'seller_code', 'seller_VAT_code']
        labels = {
            'buyer_company_name': 'Perkanti įmonė',
            'buyer_address': 'Pirkėjo adresas',
            'buyer_code': 'Pirkėjo kodas',
            'buyer_VAT_code': 'Pirkėjo PVM kodas',

            'seller_company_name': 'Parduodanti įmonė',
            'seller_address': 'Pardavėjo adresas',
            'seller_code': 'Pardavėjo kodas',
            'seller_VAT_code': 'Pardavėjo PVM kodas'
        }


# Nurašymo akte pridėti sąskaitas faktūras
class UsageActForm(forms.ModelForm):
    def __init__(self, user, *args, **kwargs):
        super().__init__(*args, **kwargs)
        
        # pasirenkam sąskaitas faktūras, kurios dar nėra naudotos ir priklauso dabartiniam vartotojui
        self.fields['product_lists'].queryset = ProductList.objects.filter(user=user, is_used=False)

    product_lists = forms.ModelMultipleChoiceField(
        queryset=ProductList.objects.none(),
        widget=forms.SelectMultiple,
        required=True,
        label='Sąskaitos faktūros'
    )

    class Meta:
        model = UsageAct
        fields = ['product_lists']
        labels = {
            'product_lists': 'Sąskaitos faktūros',
        }


