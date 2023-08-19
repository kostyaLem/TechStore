using HandyControl.Controls;
using TechStore.UI.ViewModels;

namespace TechStore.UI.Views.Promos;

public partial class PromosView : GlowWindow
{
    public PromosView(PromosViewModel dataContext)
    {
        InitializeComponent();
        DataContext = dataContext;
    }
}
