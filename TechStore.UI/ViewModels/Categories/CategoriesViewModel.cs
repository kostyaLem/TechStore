using DevExpress.Mvvm;
using HandyControl.Tools.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.BL.Mapping;
using TechStore.BL.Models.Categories;
using TechStore.BL.Services.Interfaces;
using TechStore.UI.Services;
using TechStore.UI.Views.Categories;

namespace TechStore.UI.ViewModels.Categories;

public class CategoriesViewModel : BaseItemsViewModel<Category>
{
    private readonly ICategoryService _categoryService;
    // Сервис для работы с диалоговыми окнами
    private readonly IWindowDialogService _dialogService;

    public CategoriesViewModel(ICategoryService categoryService, IWindowDialogService dialogService)
    {
        _categoryService = categoryService;
        _dialogService = dialogService;

        LoadViewDataCommand = new AsyncCommand(LoadCategories);
        CreateItemCommand = new AsyncCommand(CreateCategory, () => Container.IsAdmin);
        EditItemCommand = new AsyncCommand(EditCategory, () => Container.IsAdmin && SelectedItem != null);
        RemoveItemCommand = new AsyncCommand<object>(RemoveCategory, _ => Container.IsAdmin && SelectedItem != null);

        ItemsView.Filter += CanFilterCategory;
    }

    private async Task LoadCategories()
    {
        await Execute(async () =>
        {
            _items.Clear();
            var customers = await _categoryService.GetCategories();
            _items.AddRange(customers);
        });
    }

    private bool CanFilterCategory(object obj)
    {
        if (SearchText is { } && obj is Category customer)
        {
            var predicates = new List<string>
            {
                customer.Name
            };

            return predicates.Any(x => x.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        }

        return true;
    }

    private async Task CreateCategory()
    {
        await Execute(async () =>
        {
            var vm = new EditViewModel<Category>();

            if (_dialogService.ShowDialog(typeof(EditCategoryPage), vm))
            {
                var customer = vm.Item.MapToRequest();
                await _categoryService.Create(customer);
                await LoadCategories();
            }
        });
    }

    private async Task EditCategory()
    {
        await Execute(async () =>
        {
            var customer = await _categoryService.GetById(SelectedItem.Id);
            var vm = new EditViewModel<Category>(customer);

            if (_dialogService.ShowDialog(typeof(EditCategoryPage), vm))
            {
                var updated = await _categoryService.Update(customer);
                await ReplaceItem(x => x.Id == customer.Id, updated);
            }
        });
    }

    private async Task RemoveCategory(object obj)
    {
        var items = (obj as IEnumerable)!
            .Cast<Category>()
            .Select(x => x.Id)
            .ToList();

        await Execute(async () =>
        {
            await _categoryService.Remove(items);
            await LoadCategories();
        });
    }
}
