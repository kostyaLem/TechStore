using DevExpress.Mvvm;
using HandyControl.Tools.Extension;
using System.Threading.Tasks;
using TechStore.BL.Models.Products;
using TechStore.BL.Services.Interfaces;
using TechStore.UI.Services;

namespace TechStore.UI.ViewModels.Products;

public class ProductViewModel : BaseItemsViewModel<Product>
{
    private readonly IProductService _productService;
    // Сервис для работы с диалоговыми окнами
    private readonly IWindowDialogService _dialogService;

    public ProductViewModel(IProductService productService, IWindowDialogService dialogService)
    {
        _productService = productService;
        _dialogService = dialogService;

        LoadViewDataCommand = new AsyncCommand(LoadItems);
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
}
