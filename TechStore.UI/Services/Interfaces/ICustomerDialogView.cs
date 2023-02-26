using System.Collections.Generic;

namespace TechStore.UI.Services.Interfaces;

public interface ICustomerDialogView
{
    public bool ShowRemoveCustomerView(IReadOnlyList<int> customerIds);
}

