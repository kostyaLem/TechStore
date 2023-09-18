using DevExpress.Mvvm;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.BL.Mapping;
using TechStore.BL.Models.Employees;
using TechStore.BL.Services.Interfaces;
using TechStore.UI.Services;
using TechStore.UI.Views.EditViews;

namespace TechStore.UI.ViewModels.Employees;

public sealed class EmployeesViewModel : BaseItemsViewModel<Employee>
{
    private readonly IEmployeeService _employeeService;
    // Сервис для работы с диалоговыми окнами
    private readonly IWindowDialogService _dialogService;

    public EmployeesViewModel(IEmployeeService employeeService, IWindowDialogService dialogService)
    {
        _employeeService = employeeService;
        _dialogService = dialogService;

        LoadViewDataCommand = new AsyncCommand(LoadEmployees);
        CreateItemCommand = new AsyncCommand(CreateEmployee, () => Container.IsAdmin);
        EditItemCommand = new AsyncCommand(EditEmployee, () => Container.IsAdmin && SelectedItem != null);
        RemoveItemCommand = new AsyncCommand<object>(RemoveEmployee, _ => Container.IsAdmin && SelectedItem != null);

        ActivateItemCommand = new AsyncCommand<object>(ActivateEmployee, _ => Container.IsAdmin && SelectedItem != null);
        DisableItemCommand = new AsyncCommand<object>(DisableEmployee, _ => Container.IsAdmin && SelectedItem != null);

        ItemsView.Filter += CanFilterCustomer;
    }

    private async Task LoadEmployees()
    {
        await Execute(async () =>
        {
            _items.Clear();
            var employees = await _employeeService.GetEmployees();
            _items.AddRange(employees);
        });
    }

    private bool CanFilterCustomer(object obj)
    {
        if (SearchText is { } && obj is Employee employee)
        {
            var predicates = new List<string>
            {
                employee.Email,
                employee.Login,
                employee.FirstName,
                employee.LastName,
                employee.Birthday.ToString(),
                employee.UpdatedOn.ToString(),
                $"{employee.FirstName} {employee.LastName}",
                employee.Phone
            };

            return predicates.Any(x => x.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        }

        return true;
    }

    private async Task CreateEmployee()
    {
        await Execute(async () =>
        {
            var vm = new EditViewModel<Employee>(x => x.IsActive = true);

            if (_dialogService.ShowDialog(typeof(EditEmployeePage), vm))
            {
                var customer = vm.Item.MapToRequest((string)vm.Args);
                await _employeeService.Create(customer);
                await LoadEmployees();
            }
        });
    }

    private async Task EditEmployee()
    {
        await Execute(async () =>
        {
            var employee = await _employeeService.GetById(SelectedItem.Id);
            var vm = new EditViewModel<Employee>(employee);

            if (_dialogService.ShowDialog(typeof(EditEmployeePage), vm))
            {
                var updated = await _employeeService.Update(employee, (string)vm.Args);
                await ReplaceItem(x => x.Id == employee.Id, updated);
            }
        });
    }

    private async Task RemoveEmployee(object obj)
    {
        var items = (obj as IEnumerable)!
            .Cast<Employee>()
            .Select(x => x.Id)
            .ToList();

        if (items.Contains(Container.CurrentUser.Id))
        {
            MessageBox.Warning($"Невозможно удалить текущего пользователя {Container.CurrentUser.Login}");
            return;
        }

        await Execute(async () =>
        {
            await _employeeService.Remove(items);
            await LoadEmployees();
        });
    }

    private async Task ActivateEmployee(object obj)
    {
        var items = (obj as IEnumerable)!.Cast<Employee>();

        await Execute(async () =>
        {
            await _employeeService.UpdateActiveStatus(items.Select(x => x.Id).ToList(), true);
            items.ForEach(x => x.IsActive = true);
        });
    }

    private async Task DisableEmployee(object obj)
    {
        var items = (obj as IEnumerable)!.Cast<Employee>();

        await Execute(async () =>
        {
            await _employeeService.UpdateActiveStatus(items.Select(x => x.Id).ToList(), false);
            items.ForEach(x => x.IsActive = false);
        });
    }
}
