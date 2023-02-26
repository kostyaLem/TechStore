using DevExpress.Mvvm;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using TechStore.BL.Models;
using TechStore.BL.Services.Interfaces;
using TechStore.UI.Services.Interfaces;

namespace TechStore.UI.ViewModels.Administration;

public class CustomersViewModel : BaseViewModel
{
    private readonly ICustomerService _customerService;
    private readonly ICustomerDialogView _customerDialog;
    private ObservableCollection<Customer> _customersSource;

    public ICollectionView CustomersView { get; }

    public Customer SelectedCustomer
    {
        get => GetValue<Customer>(nameof(SelectedCustomer));
        set => SetValue(value, nameof(SelectedCustomer));
    }

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

    public AsyncCommand LoadViewDataCommand { get; }
    public AsyncCommand CreateCustomerCommand { get; }
    public AsyncCommand EditCustomerCommand { get; }
    public AsyncCommand<object> RemoveCustomerCommand { get; }
    public AsyncCommand<object> ActivateCustomersCommand { get; }
    public AsyncCommand<object> DisableCustomersCommand { get; }


    public CustomersViewModel(ICustomerService customerService, ICustomerDialogView customerDialog)
    {
        _customerService = customerService;
        _customerDialog = customerDialog;

        ActivateCustomersCommand = new(customers => SetCustomersStatus(customers, isActive: true));
        DisableCustomersCommand = new(customers => SetCustomersStatus(customers, isActive: false));

        EditCustomerCommand = new(UpdateCustomer, () => SelectedCustomer is not null);
        RemoveCustomerCommand = new(customers => RemoveCustomer(customers));
        LoadViewDataCommand = new(RefreshCustomersSource);

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
                customer.Birthday.ToString(),
                customer.UpdatedOn.ToString(),
                $"{customer.FirstName} {customer.LastName}",
                customer.Phone
            };

            return predicates.Any(x => x.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        }

        return true;
    }

    private async Task SetCustomersStatus(object selectedItemCollection, bool isActive)
    {
        var customers = ((IList)selectedItemCollection).Cast<Customer>();

        if (customers.Any())
        {
            var customerIds = customers.Select(x => x.Id).ToList();
            await _customerService.UpdateActiveStatus(customerIds, isActive);
            await RefreshCustomersSource();
        }
    }

    private async Task UpdateCustomer()
    {
        await _customerService.Update(SelectedCustomer);
    }

    private async Task RemoveCustomer(object selectedItemCollection)
    {
        var customers = ((IList)selectedItemCollection).Cast<Customer>();
        var customerIds = customers.Select(x => x.Id).ToList();

        if (_customerDialog.ShowRemoveCustomerView(customerIds))
        {
            await _customerService.Remove(customerIds);

            await RefreshCustomersSource();
        }
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
        catch (Exception)
        {
            throw;
        }
    }
}
