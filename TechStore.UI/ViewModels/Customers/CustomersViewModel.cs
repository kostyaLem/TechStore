using DevExpress.Mvvm;
using HandyControl.Tools.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.BL.Mapping;
using TechStore.BL.Models.Customers;
using TechStore.BL.Services.Interfaces;
using TechStore.UI.Services;
using TechStore.UI.Views.EditViews;

namespace TechStore.UI.ViewModels.Customers;

public sealed class CustomersViewModel : BaseItemsViewModel<Customer>
{
    private readonly ICustomerService _customerService;
    // Сервис для работы с диалоговыми окнами
    private readonly IWindowDialogService _dialogService;

    public CustomersViewModel(ICustomerService customerService, IWindowDialogService dialogService)
    {
        _customerService = customerService;
        _dialogService = dialogService;

        LoadViewDataCommand = new AsyncCommand(LoadCustomers);
        CreateItemCommand = new AsyncCommand(CreateCustomer, () => Container.IsAdmin);
        EditItemCommand = new AsyncCommand(EditCustomer, () => Container.IsAdmin && SelectedItem != null);
        RemoveItemCommand = new AsyncCommand<object>(RemoveCustomer, _ => Container.IsAdmin && SelectedItem != null);

        ActivateItemCommand = new AsyncCommand<object>(ActivateCustomers, _ => Container.IsAdmin && SelectedItem != null);
        DisableItemCommand = new AsyncCommand<object>(DisableCustomers, _ => Container.IsAdmin && SelectedItem != null);

        ItemsView.Filter += CanFilterCustomer;
    }

    private async Task LoadCustomers()
    {
        await Execute(async () =>
        {
            _items.Clear();
            var customers = await _customerService.GetCustomers();
            _items.AddRange(customers);
        });
    }

    private bool CanFilterCustomer(object obj)
    {
        if (SearchText is { } && obj is Customer customer)
        {
            var predicates = new List<string>
            {
                customer.Email,
                customer.Login,
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

    private async Task CreateCustomer()
    {
        await Execute(async () =>
        {
            var vm = new EditViewModel<Customer>(x => x.IsActive = true);

            var result = _dialogService.ShowDialog(typeof(EditCustomerPage), vm);

            if (result == DialogResult.OK)
            {
                var customer = vm.Item.MapToRequest((string)vm.Args);
                await _customerService.Create(customer);
                await LoadCustomers();
            }
        });
    }

    private async Task EditCustomer()
    {
        await Execute(async () =>
        {
            var customer = await _customerService.GetById(SelectedItem.Id);
            var vm = new EditViewModel<Customer>(customer);

            var result = _dialogService.ShowDialog(typeof(EditCustomerPage), vm);

            if (result == DialogResult.OK)
            {
                var updated = await _customerService.Update(customer, (string)vm.Args);
                await ReplaceItem(x => x.Id == customer.Id, updated);
            }
        });
    }

    private async Task RemoveCustomer(object obj)
    {
        var items = (obj as IEnumerable)!
            .Cast<Customer>()
            .Select(x => x.Id)
            .ToList();

        await Execute(async () =>
        {
            await _customerService.Remove(items);
            await LoadCustomers();
        });
    }

    private async Task ActivateCustomers(object obj)
    {
        var items = (obj as IEnumerable)!.Cast<Customer>();

        await Execute(async () =>
        {
            await _customerService.UpdateActiveStatus(items.Select(x => x.Id).ToList(), true);
            items.ForEach(x => x.IsActive = true);
        });
    }

    private async Task DisableCustomers(object obj)
    {
        var items = (obj as IEnumerable)!.Cast<Customer>();

        await Execute(async () =>
        {
            await _customerService.UpdateActiveStatus(items.Select(x => x.Id).ToList(), false);
            items.ForEach(x => x.IsActive = false);
        });
    }
}
