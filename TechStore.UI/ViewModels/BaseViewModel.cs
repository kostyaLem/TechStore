using DevExpress.Mvvm;

namespace TechStore.UI.ViewModels;

public abstract class BaseViewModel : ViewModelBase
{
    public virtual string Title { get; }
}
