using System;
using System.Collections.Generic;
using TechStore.UI.ViewModels;
using TechStore.UI.ViewModels.Categories;
using TechStore.UI.ViewModels.Customers;
using TechStore.UI.ViewModels.Employees;
using TechStore.UI.ViewModels.Products;
using TechStore.UI.ViewModels.Promos;

namespace TechStore.UI.Services;

internal static class ViewTitleService
{
    private static Dictionary<Type, string> _titles;

    static ViewTitleService()
    {
        _titles = new Dictionary<Type, string>()
        {
            { typeof(CategoriesViewModel), "Категории" },
            //{ typeof(CategoriesView), "Заказы" },
            { typeof(CustomersViewModel), "Клиенты" },
            { typeof(EmployeesViewModel), "Сотрудники" },
            { typeof(ProductViewModel), "Товары" },
            { typeof(PromosViewModel), "Промокоды" },
            { typeof(MainViewModel), "Терминал сотрудника" }
        };
    }

    public static string Get<T>()
    {
        if (_titles.TryGetValue(typeof(T), out var prefix))
        {
            return prefix;
        }

        return string.Empty;
    }
}
