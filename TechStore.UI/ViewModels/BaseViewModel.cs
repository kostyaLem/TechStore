using DevExpress.Mvvm;
using HandyControl.Controls;
using System;
using System.Threading.Tasks;

namespace TechStore.UI.ViewModels;

/// <summary>
/// Базоовая ViewModel для всех дочерних
/// </summary>
public abstract class BaseViewModel : ViewModelBase
{
    // Название View (окна)
    public virtual string Title { get; protected set; }

    // Флаг для отображения индикатора загрузки при выполнении асинхронных действий
    public bool IsUploading
    {
        get => GetValue<bool>(nameof(IsUploading));
        set => SetValue(value, nameof(IsUploading));
    }

    // Метод, устанавливающий флаг и выполняющий передаваемую функцию
    public async Task Execute(Func<Task> action)
    {
        IsUploading = true;
        await Task.Delay(50);

        try
        {
            await action();
        }
        catch (Exception ex)
        {
            // Вывод ошибки в унифицированном окне
            MessageBox.Error(ex.Message, "Ошибка выполнения операции");
        }
        finally
        {
            IsUploading = false;
        }
    }
}
