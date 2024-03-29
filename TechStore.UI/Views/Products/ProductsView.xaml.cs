﻿using HandyControl.Controls;
using TechStore.UI.ViewModels.Products;

namespace TechStore.UI.Views.Products;

/// <summary>
/// Логика взаимодействия для Products.xaml
/// </summary>
public partial class ProductsView : Window
{
    public ProductsView(ProductViewModel dataContext)
    {
        InitializeComponent();
        DataContext = dataContext;
    }
}
