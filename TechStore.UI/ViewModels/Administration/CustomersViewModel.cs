using DevExpress.Mvvm;
using HandyControl.Tools.Extension;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using TechStore.BL.Models;
using TechStore.BL.Services.Interfaces;

namespace TechStore.UI.ViewModels.Administration;

public class CustomersViewModel : BaseViewModel
{
    private readonly ICustomerService _customerService;

    private ObservableCollection<Customer> _customersSource { get; }

    public ICollectionView CustomersView { get; }

    public string SearchText
    {
        get => GetValue<string>(nameof(SearchText));
        set => SetValue(value, () => CustomersView.Refresh(), nameof(SearchText));
    }

    public bool IsUploading
    {
        get => GetValue<bool>(nameof(IsUploading));
        set => SetValue(value, nameof(IsUploading));
    }

    public ICommand LoadViewDataCommand { get; }
    public ICommand SetCustomersStatusCommand { get; }
    public ICommand CreateCustomerCommand { get; }
    public ICommand EditCustomerCommand { get; }
    public ICommand RemoveCustomerCommand { get; }

    public CustomersViewModel(ICustomerService customerService)
    {
        _customerService = customerService;

        SetCustomersStatusCommand = new AsyncCommand<bool>(SetCustomersStatus);

        RemoveCustomerCommand = new AsyncCommand<Customer>(RemoveCustomer, c => c is not null);
        LoadViewDataCommand = new AsyncCommand(RefreshCustomersSource);

        _customersSource = new ObservableCollection<Customer>();
        CustomersView = CollectionViewSource.GetDefaultView(_customersSource);
        CustomersView.Filter += CanFilterCustomer;
    }

    private bool CanFilterCustomer(object obj)
    {
        if (SearchText is { } && obj is Customer customer)
        {
            var predicates = new List<string>
            {
                customer.Email,
                customer.FirstName,
                customer.LastName,
                $"{customer.FirstName} {customer.LastName}",
                customer.Phone
            };

            return predicates.Any(x => x.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        }

        return true;
    }

    public async Task SetCustomersStatus(bool isActive)
    {
        var customersIds = _customersSource.Select(x => x.Id).ToList();
        await _customerService.UpdateActiveStatus(customersIds, isActive);
    }

    public async Task RemoveCustomer(Customer customer)
    {
        await _customerService.Remove(customer.Id);
    }

    private async Task RefreshCustomersSource()
    {
        try
        {
            IsUploading = true;

            var customers = await _customerService.GetCustomers();
            _customersSource.Clear();
            _customersSource.AddRange(customers);

            IsUploading = false;
        }
        catch (Exception e)
        {

        }
    }
}
