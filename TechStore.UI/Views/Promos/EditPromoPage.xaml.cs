using System.Windows.Controls;
using TechStore.BL.Models.Promos;
using TechStore.UI.ViewModels;

namespace TechStore.UI.Views.Promos;
/// <summary>
/// Логика взаимодействия для EditPromoPage.xaml
/// </summary>
public partial class EditPromoPage : UserControl
{
    public EditPromoPage(EditViewModel<Promo> context)
    {
        InitializeComponent();
        DataContext = context;
    }
}
