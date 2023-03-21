using DevExpress.Mvvm;
using TechStore.UI.Services;

namespace TechStore.UI.ViewModels;

internal class EditViewModel<T> : BaseViewModel where T : class, new()
{
    private string _viewModelName = ViewPrefixService.Get<T>();

    public T Item { get; set; }

    public EditViewModel(T itemViewModel)
    {
        Item = itemViewModel;
        Title = $"Редактирование {_viewModelName}";
    }

    public EditViewModel()
    {
        Item = new T();
        Title = $"Создание {_viewModelName}";
    }
}
