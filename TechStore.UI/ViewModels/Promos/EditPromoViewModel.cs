using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TechStore.BL.Models.Categories;
using TechStore.BL.Models.Promos;

namespace TechStore.UI.ViewModels.Promos;

internal class EditPromoViewModel : EditViewModel<Promo>
{
    public ObservableCollection<Category> Categories { get; private set; }
    public ObservableCollection<Category> SelectedCategories { get; private set; }

    public Category CategoryToAdd { get; set; }
    public Category CategoryToRemove { get; set; }

    public ICommand AddCategoryCommand { get; private set; }
    public ICommand RemoveCategoryCommand { get; private set; }

    public EditPromoViewModel(Promo itemViewModel, IReadOnlyList<Category> categories)
        : base(itemViewModel)
    {
        var unusedCategories = categories
            .Where(c => !itemViewModel.Categories.Select(x => x.Id).Contains(c.Id));

        Categories = new ObservableCollection<Category>(unusedCategories);
        SelectedCategories = new ObservableCollection<Category>(itemViewModel.Categories);
        InitCommands();
    }

    public EditPromoViewModel(IReadOnlyList<Category> categories)
        : base()
    {        
        Categories = new ObservableCollection<Category>(categories);
        SelectedCategories = new ObservableCollection<Category> ();
        Item.IsActive = true;
        InitCommands();
    }

    private void InitCommands()
    {
        AddCategoryCommand = new DelegateCommand(AddCategory, () => CategoryToAdd != null);
        RemoveCategoryCommand = new DelegateCommand(RemoveCategory, () => CategoryToRemove != null);
    }

    private void AddCategory()
    {
        SelectedCategories.Add(CategoryToAdd);
        Categories.Remove(CategoryToAdd);
    }

    private void RemoveCategory()
    {
        Categories.Add(CategoryToRemove);
        SelectedCategories.Remove(CategoryToRemove);
    }
}

