from django.urls import path
from . import views

# URL nukreipimai
urlpatterns = [
    # vartojui
    path('register/', views.register, name='register'),
    path('login/', views.login_view, name='login'),
    path('logout/', views.logout_view, name='logout'),

    # pagrindinis puslapis
    path('', views.index, name='index'),

    # sąskaitos faktūros
    path('add_product/', views.add_product, name='add_product'),
    path('add_product/<int:list_id>/', views.add_product, name='add_product_to_list'),
    path('view_lists/', views.view_lists, name='view_lists'),
    path('delete_list/<int:list_id>/', views.delete_list, name='delete_list'),
    path('add_product_list/', views.add_product_list, name='add_product_list'),
    path('download_invoice/<int:list_id>/', views.download_invoice, name='download_invoice'),

    # nurašymo aktai
    path('add_usage_act/', views.add_usage_act, name='add_usage_act'),
    path('view_usage_acts/', views.view_usage_acts, name='view_usage_acts'),
    path('delete_usage_act/<int:usage_act_id>/', views.delete_usage_act, name='delete_usage_act'),
    path('download_usage_act/<int:usage_act_id>/', views.download_usage_act, name='download_usage_act'),
]