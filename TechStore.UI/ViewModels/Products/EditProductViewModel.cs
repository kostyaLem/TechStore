using System.Collections.Generic;
using System.Linq;
using TechStore.BL.Models.Categories;
using TechStore.BL.Models.Products;

namespace TechStore.UI.ViewModels.Products;

internal class EditProductViewModel : EditViewModel<Product>
{
    public Category SelectedCategory
    {
        get => GetValue<Category>(nameof(SelectedCategory));
        set => SetValue(value, nameof(SelectedCategory));
    }

    public IReadOnlyList<Category> Categories { get; set; }

    public EditProductViewModel(Product itemViewModel, IReadOnlyList<Category> categories, int? selectedCategoryId)
    {
        Item = itemViewModel;
        Title = $"Редактирование {_viewModelName}";
        Categories = categories;

        if (selectedCategoryId.HasValue)
        {
            SelectedCategory = categories.FirstOrDefault(x => x.Id == selectedCategoryId)!;
        }
    }

    public EditProductViewModel(IReadOnlyList<Category> categories)
    {
        Item = new Product();
        Title = $"Создание {_viewModelName}";
        Item.IsActive = true;
        Categories = categories;
    }
}
