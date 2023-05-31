# Generated by Django 4.2.1 on 2023-05-31 11:22

from django.conf import settings
from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    dependencies = [
        ('accounts', '0013_rename_vat_code_productlist_buyer_vat_code_and_more'),
    ]

    operations = [
        migrations.CreateModel(
            name='UsageAct',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('created_at', models.DateTimeField(auto_now_add=True)),
                ('product_lists', models.ManyToManyField(blank=True, to='accounts.productlist')),
                ('user', models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, to=settings.AUTH_USER_MODEL)),
            ],
        ),
        migrations.AddField(
            model_name='productlist',
            name='usage_act',
            field=models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.SET_NULL, to='accounts.usageact'),
        ),
    ]
