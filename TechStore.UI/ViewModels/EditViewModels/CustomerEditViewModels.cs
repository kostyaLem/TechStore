using DevExpress.Mvvm;
using System.Threading.Tasks;
using TechStore.BL.Models;
using TechStore.BL.Services.Interfaces;

namespace TechStore.UI.ViewModels.EditViewModels;

internal class CustomerEditViewModels : BaseViewModel
{
    private readonly ICustomerService _customerService;

    public Customer EditedCustomer { get; private set; }

    public AsyncCommand RejectCommand { get; }
    public AsyncCommand SubmitCommand { get; }

    public CustomerEditViewModels(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task ResetCustomer(Customer customer)
    {
        if (customer is null)
        {
            EditedCustomer = new Customer();
            return;
        }
    }
}
//   
