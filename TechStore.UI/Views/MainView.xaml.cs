using HandyControl.Controls;
using TechStore.UI.ViewModels;

namespace TechStore.UI.Views
{
    public partial class MainView : GlowWindow
    {
        public MainView(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }
    }
}
