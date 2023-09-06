using System;
using System.Windows.Controls;
using TechStore.UI.ViewModels;
using TechStore.UI.Views.EditViews;

namespace TechStore.UI.Services;

public interface IWindowDialogService
{
    DialogResult ShowDialog(Type controlType, BaseViewModel dataContext);
}

/// <summary>
/// Класс для открытия вспомогательного окна с выбором "Да/Нет"
/// </summary>
public sealed class WindowDialogService : IWindowDialogService
{
    public DialogResult ShowDialog(Type controlType, BaseViewModel dataContext)
    {
        // Создать представление (View)
        var control = (ContentControl)Activator.CreateInstance(controlType, dataContext)!;

        // Заполнить заголовок и внутреннее содержимое окна с имзенениями
        var editView = new EditView(dataContext.Title, control);
        var dialogResult = editView.ShowDialog();

        if (dialogResult.HasValue)
        {
            return editView.DialogResult;
        }

        return DialogResult.Cancel;
    }
}
