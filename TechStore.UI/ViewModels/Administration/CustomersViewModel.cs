using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TechStore.BL.Models;
using TechStore.BL.Services.Interfaces;

namespace TechStore.UI.ViewModels.Administration;

internal class CustomersViewModel : BaseViewModel
{
    private readonly ICustomerService _customerService;

    public ObservableCollection<Customer> CustomersSource { get; init; }

    public ICommand SetCustomersStatusCommand { get; }
    public ICommand CreateCustomerCommand { get; }
    public ICommand EditCustomerCommand { get; }
    public ICommand RemoveCustomerCommand { get; }

    public CustomersViewModel(ICustomerService customerService)
    {
        _customerService = customerService;

        SetCustomersStatusCommand = new AsyncCommand<bool>(SetCustomersStatus);

        RemoveCustomerCommand = new AsyncCommand<Customer>(RemoveCustomer, c => c is not null);
    }

    public async Task SetCustomersStatus(bool isActive)
    {
        var customersIds = CustomersSource.Select(x => x.Id).ToList();
        await _customerService.UpdateActiveStatus(customersIds, isActive);
    }

    public async Task RemoveCustomer(Customer customer)
    {
        await _customerService.Remove(customer.Id);
    }
}
