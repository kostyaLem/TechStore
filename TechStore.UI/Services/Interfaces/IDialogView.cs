using System.Windows;

namespace TechStore.UI.Services.Interfaces;

public interface IDialogView
{
    public MessageBoxResult ShowDialog(string title, string caption, MessageBoxButton button);
}

