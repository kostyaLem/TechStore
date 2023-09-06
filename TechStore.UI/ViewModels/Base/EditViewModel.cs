using System;
using TechStore.UI.Services;

namespace TechStore.UI.ViewModels;

public class EditViewModel<T> : BaseViewModel where T : class, new()
{
    protected string _viewModelName = ViewPrefixService.Get<T>();

    public dynamic? Args { get; set; }

    public T Item { get; set; }

    public EditViewModel(T itemViewModel, dynamic? args = null)
    {
        Item = itemViewModel;
        Title = $"Редактирование {_viewModelName}";
        Args = args;
    }

    public EditViewModel(Action<T> preUpdate = null, dynamic? args = null)
    {
        Item = new T();
        Title = $"Создание {_viewModelName}";
        Args = args;

        preUpdate?.Invoke(Item);
    }
}
