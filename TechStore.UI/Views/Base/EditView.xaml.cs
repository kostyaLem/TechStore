using HandyControl.Controls;
using System.Windows.Controls;

namespace TechStore.UI.Views.EditViews;

/// <summary>
/// Логика взаимодействия для EditView.xaml
/// </summary>
public partial class EditView : Window
{
    private bool _isAccept;

    public ContentControl ContextItem { get; }

    public EditView(string title, ContentControl page)
    {
        InitializeComponent();
        Title = title;
        ContextItem = page;
        DataContext = this;
    }

    private void btnClose_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        DialogResult = false;
        this.Close();
    }

    private void btnOk_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        DialogResult = true;
        _isAccept = true;
        this.Close();
    }

    private void Window_Closed(object sender, System.EventArgs e)
    {
        if (!_isAccept)
        {
            //DialogResult = false;
        }
    }
}
