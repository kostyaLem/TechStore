using DevExpress.Mvvm;
using HandyControl.Tools.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.BL.Mapping;
using TechStore.BL.Models.Products;
using TechStore.BL.Services.Interfaces;
using TechStore.UI.Services;
using TechStore.UI.Views.Products;

namespace TechStore.UI.ViewModels.Products;

public class ProductViewModel : BaseItemsViewModel<Product>
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    // Сервис для работы с диалоговыми окнами
    private readonly IWindowDialogService _dialogService;

    public ProductViewModel(IProductService productService, ICategoryService categoryService, IWindowDialogService dialogService)
    {
        _productService = productService;
        _categoryService = categoryService;
        _dialogService = dialogService;

        LoadViewDataCommand = new AsyncCommand(LoadItems);
        CreateItemCommand = new AsyncCommand(CreateItem, () => Container.IsAdmin);
        EditItemCommand = new AsyncCommand(EditItem, () => Container.IsAdmin && SelectedItem != null);
        //RemoveItemCommand = new AsyncCommand<object>(RemoveItem, _ => Container.IsAdmin && SelectedItem != null);

        ActivateItemCommand = new AsyncCommand<object>(ActivateItems, _ => Container.IsAdmin && SelectedItem != null);
        DisableItemCommand = new AsyncCommand<object>(DisableItems, _ => Container.IsAdmin && SelectedItem != null);

        ItemsView.Filter += CanFilterItem;
    }

    private async Task LoadItems()
    {
        await Execute(async () =>
        {
            _items.Clear();
            var promos = await _productService.GetProducts();
            _items.AddRange(promos);
        });
    }

    private bool CanFilterItem(object obj)
    {
        if (SearchText is { } && obj is Product products)
        {
            var predicates = new List<string>()
            {
                products.Title,
                products.CreatedOn.ToShortDateString(),
                products.Price.ToString(),
                products.CreatedOn.ToString()
            };

            if (products.Category != null)
                predicates.Add(products.Category.Name);

            return predicates.Any(x => x.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        }

        return true;
    }

    private async Task CreateItem()
    {
        await Execute(async () =>
        {
            var vm = new EditProductViewModel(await _categoryService.GetCategories(), _dialogService);

            if (_dialogService.ShowDialog(typeof(EditProductPage), vm))
            {
                var product = vm.Item.MapToRequest();
                await _productService.Create(product);
                await LoadItems();
            }
        });
    }

    private async Task EditItem()
    {
        await Execute(async () =>
        {
            var product = await _productService.GetById(SelectedItem.Id);
            var vm = new EditProductViewModel(product, await _categoryService.GetCategories(), _dialogService);

            if (_dialogService.ShowDialog(typeof(EditProductPage), vm))
            {
                var updated = await _productService.Update(product);
                await ReplaceItem(x => x.Id == product.Id, updated);
            }
        });
    }

    private async Task ActivateItems(object obj)
    {
        var items = (obj as IEnumerable)!.Cast<Product>();

        await Execute(async () =>
        {
            await _productService.UpdateActiveStatus(items.Select(x => x.Id).ToList(), true);
            items.ForEach(x => x.IsActive = true);
        });
    }

    private async Task DisableItems(object obj)
    {
        var items = (obj as IEnumerable)!.Cast<Product>();

        await Execute(async () =>
        {
            await _productService.UpdateActiveStatus(items.Select(x => x.Id).ToList(), false);
            items.ForEach(x => x.IsActive = false);
        });
    }
}
