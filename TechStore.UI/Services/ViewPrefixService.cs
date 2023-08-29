using System;
using System.Collections.Generic;
using TechStore.BL.Models.Customers;
using TechStore.BL.Models.Employees;
using TechStore.BL.Models.Promos;

namespace TechStore.UI.Services;

internal static class ViewPrefixService
{
    private static Dictionary<Type, string> _prefixes;

    static ViewPrefixService()
    {
        _prefixes = new Dictionary<Type, string>()
        {
            { typeof(Customer), "клиента" },
            { typeof(Employee), "работника" },
            { typeof(Promo), "промокода" }
        };
    }

    public static string Get<T>()
    {
        if (_prefixes.TryGetValue(typeof(T), out var prefix))
        {
            return prefix;
        }

        return string.Empty;
    }
}
