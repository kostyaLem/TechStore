using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;

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

    public async Task ReplaceItem(T targetItem, T newItem)
    {
        await Execute(async () =>
        {
            var index = _items.IndexOf(targetItem);

            if (index >= 0)
            {
                _items.Remove(targetItem);
                _items.Insert(index, newItem);
            }
        });
    }
}

