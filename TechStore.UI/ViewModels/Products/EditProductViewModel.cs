using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TechStore.BL.Models.Categories;
using TechStore.BL.Models.Products;
using TechStore.UI.Services;

namespace TechStore.UI.ViewModels.Products;

internal class EditProductViewModel : EditViewModel<Product>
{
    private readonly IWindowDialogService _dialogService;

    public Category SelectedCategory
    {
        get => GetValue<Category>(nameof(SelectedCategory));
        set => SetValue(value, () => Item.Category = value, nameof(SelectedCategory));
    }

    public IReadOnlyList<Category> Categories { get; set; }

    public ICommand SelectLargeImageCommand { get; }
    public ICommand SelectSmallImageCommand { get; }
    public ICommand RemoveLargeImageCommand { get; }
    public ICommand RemoveSmallImageCommand { get; }

    public EditProductViewModel(Product itemViewModel, IReadOnlyList<Category> categories, IWindowDialogService dialogService)
        : this(dialogService)
    {
        Item = itemViewModel;
        Title = $"Редактирование {_viewModelName}";
        Categories = categories;

        if (Item.Category != null)
        {
            SelectedCategory = categories.FirstOrDefault(x => x.Id == Item.Category.Id)!;
        }
    }

    public EditProductViewModel(IReadOnlyList<Category> categories, IWindowDialogService dialogService)
        : this(dialogService)
    {
        Item = new Product();
        Title = $"Создание {_viewModelName}";
        Item.IsActive = true;
        Categories = categories;
    }

    private EditProductViewModel(IWindowDialogService dialogService)
    {
        _dialogService = dialogService;
        SelectLargeImageCommand = new DelegateCommand(SelectLargeImage);
        SelectSmallImageCommand = new DelegateCommand(SelectSmallImage);
        RemoveLargeImageCommand = new DelegateCommand(RemoveLargeImage);
        RemoveSmallImageCommand = new DelegateCommand(RemoveSmallImage);
    }

    private void SelectLargeImage()
    {
        if (_dialogService.SelectImage(out var imageBytes))
        {
            Item.Image = imageBytes;
        }
    }

    private void SelectSmallImage()
    {
        if (_dialogService.SelectImage(out var imageBytes))
        {
            Item.SmallImage = imageBytes;
        }
    }

    private void RemoveLargeImage()
    {
        Item.Image = null;
    }

    private void RemoveSmallImage()
    {
        Item.SmallImage = null;
    }
}
