using HandyControl.Controls;
using TechStore.UI.ViewModels;

namespace TechStore.UI.Views.Administration
{
    /// <summary>
    /// Interaction logic for AuthView.xaml
    /// </summary>
    public partial class AuthView : GlowWindow
    {
        public AuthView(AuthViewModel authViewModel)
        {
            InitializeComponent();
            DataContext = authViewModel;
        }
    }
}
