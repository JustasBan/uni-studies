from django.shortcuts import render, redirect
from .forms import CustomUserCreationForm, CustomAuthenticationForm
from django.contrib.auth import login, logout
from django.views.generic import ListView, CreateView
from django.contrib.auth.decorators import login_required
from django.utils.decorators import method_decorator
from .models import ProductList, Product, UsageAct
from .forms import ProductForm, ProductListForm, UsageActForm
from django.shortcuts import render, redirect, get_object_or_404

from django.template.loader import get_template, render_to_string
from io import BytesIO
from xhtml2pdf import pisa

from django.http import HttpResponse


# Registracijos formos implementacija
def register(request):
    if request.method == 'POST':
        form = CustomUserCreationForm(request.POST)
        if form.is_valid():
            user = form.save()
            login(request, user)
            return redirect('index')
    else:
        form = CustomUserCreationForm()
    return render(request, 'register.html', {'form': form})


# Prisijungimo formos implementacija
def login_view(request):
    if request.method == 'POST':
        form = CustomAuthenticationForm(data=request.POST)
        if form.is_valid():
            login(request, form.get_user())
            return redirect('index')
    else:
        form = CustomAuthenticationForm()
    return render(request, 'login.html', {'form': form})


# Pagrindinio puslapio implementacija
def index(request):
    return render(request, 'index.html')


# Atsijungimo implementacija
def logout_view(request):
    logout(request)
    return redirect('index')


# Sąskaitos sukūrimo implementacija
@login_required
def add_product_list(request):
    if request.method == 'POST':
        form = ProductListForm(request.POST)
        if form.is_valid():
            product_list = form.save(commit=False)
            product_list.user = request.user
            product_list.save()
            return redirect('view_lists')
    else:
        form = ProductListForm()
    return render(request, 'add_product_list.html', {'form': form})


# Produktų pridėjimo į sąskaitą implementacija
@login_required
def add_product(request, list_id=None):
    if list_id:
        product_list = get_object_or_404(
            ProductList, id=list_id, user=request.user)
    else:
        product_list = ProductList(user=request.user)
        product_list.save()

    if request.method == 'POST':
        form = ProductForm(request.POST)
        if form.is_valid():
            product = form.save()
            product_list.products.add(product)
            product_list.save()
            return redirect('view_lists')
    else:
        form = ProductForm()

    return render(request, 'add_product.html', {'form': form})


# Sąskaitų faktūrų peržiūros implementacija
@login_required
def view_lists(request):
    product_lists = ProductList.objects.filter(user=request.user)
    return render(request, 'view_lists.html', {'product_lists': product_lists})


# Sąskaitos faktūros ištrynimo implementacija
@login_required
def delete_list(request, list_id):
    ProductList.objects.filter(id=list_id, user=request.user).delete()
    return redirect('view_lists')


# Sąskaitos faktūros PDF atsisiuntimo implementacija
@login_required
def download_invoice(request, list_id):
    product_list = get_object_or_404(
        ProductList, id=list_id, user=request.user)
    products = product_list.products.all()
    rendered = render_to_string('invoice.html', {'product_list': product_list, 'products': products,
                                'name': request.user.name, 'surname': request.user.surname, 'phone_number': request.user.phone_number})

    result = BytesIO()

    pdf = pisa.pisaDocument(BytesIO(rendered.encode("UTF-8")), result)
    if not pdf.err:
        return HttpResponse(result.getvalue(), content_type='application/pdf')
    else:
        return HttpResponse('Errors')

    response = HttpResponse(pdf, content_type='application/pdf')
    response['Content-Disposition'] = 'attachment; filename="invoice.pdf"'
    return response


# Nurašymo akto sukūrimo ir peržiūros implementacija (sukuriama iškart su sąskaitos faktūromis) 
@login_required
def add_usage_act(request):
    if request.method == 'POST':
        form = UsageActForm(request.user, request.POST)
        if form.is_valid():
            usage_act = form.save(commit=False)
            usage_act.user = request.user
            usage_act.save()
            form.save_m2m() 
            for product_list in usage_act.product_lists.all():
                product_list.is_used = True
                product_list.save()

            return redirect('view_usage_acts')
    else:
        form = UsageActForm(request.user)
    return render(request, 'add_usage_act.html', {'form': form})


# Nurašymo akto peržiūros implementacija
@login_required
def view_usage_acts(request):
    usage_acts = UsageAct.objects.filter(user=request.user)
    act_details = [
        {
            'act': act,
            'products': act.all_products(),
            'product_lists': act.product_lists.all(),
        }
        for act in usage_acts
    ]
    return render(request, 'view_usage_acts.html', {'act_details': act_details})


# Nurašymo akto ištrynimo implementacija (atlaisvinamos sąskaitos faktūros)
@login_required
def delete_usage_act(request, usage_act_id):
    usage_act = UsageAct.objects.get(id=usage_act_id, user=request.user)
    for product_list in usage_act.product_lists.all():
        product_list.is_used = False
        product_list.save()
    usage_act.delete()
    return redirect('view_usage_acts')


# Nurašymo akto PDF atsisiuntimo implementacija
@login_required
def download_usage_act(request, usage_act_id):
    usage_act = get_object_or_404(
        UsageAct, id=usage_act_id, user=request.user)

    product_lists = usage_act.product_lists.all()
    products = [product for product_list in product_lists for product in product_list.products.all()]
    
    rendered = render_to_string('usage_act.html', {
        'usage_act': usage_act,
        'products': products,
        'product_lists': product_lists,  
        'name': request.user.name,
        'surname': request.user.surname,
        'phone_number': request.user.phone_number
    })

    result = BytesIO()

    pdf = pisa.pisaDocument(BytesIO(rendered.encode("UTF-8")), result)
    if not pdf.err:
        return HttpResponse(result.getvalue(), content_type='application/pdf')
    else:
        return HttpResponse('Errors')

    response = HttpResponse(pdf, content_type='application/pdf')
    response['Content-Disposition'] = 'attachment; filename="usage_act.pdf"'
    return response

