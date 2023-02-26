using System.Windows;
using TechStore.UI.Services.Interfaces;
using MessageBox = HandyControl.Controls.MessageBox;

namespace TechStore.UI.Services;

public class DialogView : IDialogView
{
    public MessageBoxResult ShowDialog(string title, string caption, MessageBoxButton button)
    {
        return MessageBox.Show(caption, title, button);
    }
}

