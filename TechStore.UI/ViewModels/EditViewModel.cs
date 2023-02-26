using DevExpress.Mvvm;
using TechStore.UI.Services;

namespace TechStore.UI.ViewModels;

internal class EditViewModel<T> : BaseViewModel where T : class, new()
{
    public T Item { get; set; }

    public EditViewModel(T itemViewModel)
    {
        Item = itemViewModel;
        Title = $"Редактирование {ViewPrefixService.Get<T>()}";
    }

    public EditViewModel()
    {
        Item = new T();
        Title = $"Создание {ViewPrefixService.Get<T>()}";
    }
}
