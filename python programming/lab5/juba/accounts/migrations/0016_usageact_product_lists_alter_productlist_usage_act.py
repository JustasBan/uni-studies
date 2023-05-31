# Generated by Django 4.2.1 on 2023-05-31 14:45

from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    dependencies = [
        ('accounts', '0015_remove_usageact_product_lists_and_more'),
    ]

    operations = [
        migrations.AddField(
            model_name='usageact',
            name='product_lists',
            field=models.ManyToManyField(related_name='usage_acts', to='accounts.productlist'),
        ),
        migrations.AlterField(
            model_name='productlist',
            name='usage_act',
            field=models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.SET_NULL, related_name='usage_acts', to='accounts.usageact'),
        ),
    ]