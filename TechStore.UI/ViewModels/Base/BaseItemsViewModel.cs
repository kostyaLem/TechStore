using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace TechStore.UI.ViewModels;

/// <summary>
/// Базовый класс для всех ViewModel'ей.
/// Имеется флаг IsUploading для отображения индикатора загрузки, используя метод Execute
/// </summary>
public abstract class BaseItemsViewModel<T> : BaseViewModel
{
    protected readonly ObservableCollection<T> _items;

    // Элементы коллекции, отображаемые во View
    public ICollectionView ItemsView { get; }

    public ICommand LoadViewDataCommand { get; protected set; }
    public ICommand CreateItemCommand { get; protected set; }
    public ICommand EditItemCommand { get; protected set; }
    public ICommand<object> RemoveItemCommand { get; protected set; }
    public ICommand<object> ActivateItemCommand { get; protected set; }
    public ICommand<object> DisableItemCommand { get; protected set; }

    public T SelectedItem
    {
        get => GetValue<T>(nameof(SelectedItem));
        set => SetValue(value, nameof(SelectedItem));
    }

    // Поле для текстового поиска с автоматическим обновлением коллекции
    public virtual string SearchText
    {
        get => GetValue<string>(nameof(SearchText));
        set => SetValue(value, () => ItemsView.Refresh(), nameof(SearchText));
    }

    public BaseItemsViewModel()
    {
        _items = new ObservableCollection<T>();
        ItemsView = CollectionViewSource.GetDefaultView(_items);
    }

    public async Task ReplaceItem(Predicate<T> predicate, T newItem)
    {
        await Execute(async () =>
        {
            var index = _items.IndexOf(predicate);

            if (index >= 0)
            {
                _items.RemoveAt(index);
                _items.Insert(index, newItem);
            }
        });
    }

    public async Task RemoveItem(Predicate<T> predicate)
    {
        await Execute(async () =>
        {
            var index = _items.IndexOf(predicate);

            if (index >= 0)
            {
                _items.RemoveAt(index);
            }
        });
    }
}

