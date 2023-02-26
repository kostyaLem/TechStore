using System.Collections.Generic;
using System.Windows;
using TechStore.UI.Services.Interfaces;

namespace TechStore.UI.Services;

public class CustomerDialogView : ICustomerDialogView
{
    private readonly IDialogView _dialogView;

    public CustomerDialogView(IDialogView dialogView)
    {
        _dialogView = dialogView;
    }

    public bool ShowRemoveCustomerView(IReadOnlyList<int> customerIds)
    {
        var message = customerIds.Count > 1
            ? $"Будет удалено [{customerIds.Count}] записей. Подтвердить ?"
            : "Хоитет удалить клиента ?";

        var result = _dialogView.ShowDialog("Подтверждение", message, MessageBoxButton.YesNoCancel);

        return result == MessageBoxResult.Yes;
    }
}

