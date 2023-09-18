using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Controls;
using TechStore.UI.ViewModels;
using TechStore.UI.Views.EditViews;

namespace TechStore.UI.Services;

/// <summary>
/// Класс для открытия вспомогательного окна с выбором "Да/Нет"
/// </summary>
public sealed class WindowDialogService : IWindowDialogService
{
    public bool ShowDialog(Type controlType, BaseViewModel dataContext)
    {
        // Создать представление (View)
        var control = (ContentControl)Activator.CreateInstance(controlType, dataContext)!;

        // Заполнить заголовок и внутреннее содержимое окна с изменениями
        var editView = new EditView(dataContext.Title, control);
        var dialogResult = editView.ShowDialog();

        if (dialogResult.HasValue)
        {
            return editView.DialogResult!.Value;
        }

        return false;
    }

    public bool SelectImage(out byte[] imageBytes)
    {
        var fileDialog = new OpenFileDialog()
        {
            Title = "Выбор изображения",
            Filter = "Изображение|*.jpg;*.jpeg;*png"
        };

        var result = fileDialog.ShowDialog();

        if (result.HasValue)
        {
            imageBytes = File.ReadAllBytes(fileDialog.FileName);
            return true;
        }

        imageBytes = default!;
        return false;
    }
}
