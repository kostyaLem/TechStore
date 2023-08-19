using System;
using System.Collections.Generic;
using TechStore.BL.Models.Customers;
using TechStore.BL.Models.Employees;

namespace TechStore.UI.Services;

internal static class ViewPrefixService
{
    private static Dictionary<Type, string> _prefixes;

    static ViewPrefixService()
    {
        _prefixes = new Dictionary<Type, string>()
        {
            { typeof(Customer), "клиента" },
            { typeof(Employee), "работника" }
        };
    }

    public static string Get<T>()
    {
        return _prefixes[typeof(T)];
    }
}
