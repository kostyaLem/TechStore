using HandyControl.Controls;
using TechStore.UI.ViewModels;

namespace TechStore.UI.Views
{
    public partial class MainView : Window
    {
        public MainView(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }
    }
}
