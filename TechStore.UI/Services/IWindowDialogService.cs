using System;
using TechStore.UI.ViewModels;

namespace TechStore.UI.Services;

public interface IWindowDialogService
{
    bool ShowDialog(Type controlType, BaseViewModel dataContext);
    bool SelectImage(out byte[] imageBytes);
}
