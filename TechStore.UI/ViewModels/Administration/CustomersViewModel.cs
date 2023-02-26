using DevExpress.Mvvm;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TechStore.BL.Mapping;
using TechStore.BL.Models;
using TechStore.BL.Services.Interfaces;
using TechStore.UI.Services;
using TechStore.UI.Views.EditViews;

namespace TechStore.UI.ViewModels.Administration;

public class CustomersViewModel : BaseItemsViewModel<Customer>
{
    private readonly ICustomerService _customerService;
    // Серивс для работы с диалоговыси окнами
    private readonly IWindowDialogService _dialogService;

    public ICommand LoadViewDataCommand { get; }
    public ICommand CreateCustomerCommand { get; }
    public ICommand EditCustomerCommand { get; }
    public ICommand RemoveCustomerCommand { get; }
    public ICommand<object> ActivateCustomersCommand { get; }
    public ICommand<object> DisableCustomersCommand { get; }

    public CustomersViewModel(ICustomerService customerService, IWindowDialogService dialogService)
    {
        _customerService = customerService;
        _dialogService = dialogService;

        LoadViewDataCommand = new AsyncCommand(LoadCustomers);
        CreateCustomerCommand = new AsyncCommand(CreateCustomer, () => App.IsAdmin);
        EditCustomerCommand = new AsyncCommand(EditCustomer, () => App.IsAdmin);
        RemoveCustomerCommand = new AsyncCommand(RemoveCustomer, () => App.IsAdmin);

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
            var vm = new EditViewModel<Customer>();

            var result = _dialogService.ShowDialog(typeof(EditCustomerPage), vm);

            if (result == DialogResult.OK)
            {
                var customer = vm.Item.MapToRequest();
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
                // Вызвать обновление пользователя, если есть подтверждение
                await _customerService.Update(vm.Item);

                // Обновить коллекцию на интерфейсе
                await LoadCustomers();
            }
        });
    }

    private async Task RemoveCustomer()
    {
        await Execute(async () =>
        {
            await _customerService.Remove(SelectedItem.Id);
            await LoadCustomers();
        });
    }
}
