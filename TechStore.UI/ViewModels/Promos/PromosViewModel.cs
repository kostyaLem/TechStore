using DevExpress.Mvvm;
using HandyControl.Tools.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.BL.Mapping;
using TechStore.BL.Models.Promos;
using TechStore.BL.Services.Interfaces;
using TechStore.UI.Services;
using TechStore.UI.Views.Promos;

namespace TechStore.UI.ViewModels.Promos;

public sealed class PromosViewModel : BaseItemsViewModel<Promo>
{
    private readonly IPromoService _promoService;
    private readonly ICategoryService _categoryService;

    // Сервис для работы с диалоговыми окнами
    private readonly IWindowDialogService _dialogService;

    public PromosViewModel(IPromoService promoService, ICategoryService categoryService, IWindowDialogService dialogService)
    {
        _promoService = promoService;
        _categoryService = categoryService;
        _dialogService = dialogService;

        LoadViewDataCommand = new AsyncCommand(LoadItems);
        CreateItemCommand = new AsyncCommand(CreateItem, () => Container.IsAdmin);
        EditItemCommand = new AsyncCommand(EditItem, () => Container.IsAdmin && SelectedItem != null);
        RemoveItemCommand = new AsyncCommand<object>(RemoveItem, _ => Container.IsAdmin && SelectedItem != null);

        ActivateItemCommand = new AsyncCommand<object>(ActivateItems, _ => Container.IsAdmin && SelectedItem != null);
        DisableItemCommand = new AsyncCommand<object>(DisableItems, _ => Container.IsAdmin && SelectedItem != null);

        ItemsView.Filter += CanFilterItem;
    }

    private async Task LoadItems()
    {
        await Execute(async () =>
        {
            _items.Clear();
            var promos = await _promoService.GetPromos();
            _items.AddRange(promos);
        });
    }

    private bool CanFilterItem(object obj)
    {
        if (SearchText is { } && obj is Promo promo)
        {
            var predicates = new List<string>()
            {
                promo.Name,
                promo.Discount.ToString(),
                promo.CreatedBy,
                promo.CreatedOn.ToString()
            };

            return predicates.Any(x => x.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        }

        return true;
    }

    private async Task CreateItem()
    {
        await Execute(async () =>
        {
            var vm = new EditPromoViewModel(await _categoryService.GetCategories());

            if (_dialogService.ShowDialog(typeof(EditPromoPage), vm))
            {
                var promo = vm.Item.MapToRequest(Container.CurrentUser.Id);
                await _promoService.Create(promo);
                await LoadItems();
            }
        });
    }

    private async Task EditItem()
    {
        await Execute(async () =>
        {
            var promo = await _promoService.GetById(SelectedItem.Id);
            var vm = new EditPromoViewModel(promo, await _categoryService.GetCategories());

            if (_dialogService.ShowDialog(typeof(EditPromoPage), vm))
            {
                var updated = await _promoService.Update(promo);
                await ReplaceItem(x => x.Id == promo.Id, updated);
            }
        });
    }

    private async Task RemoveItem(object obj)
    {
        var items = (obj as IEnumerable)!
            .Cast<Promo>()
            .Select(x => x.Id)
            .ToList();

        await Execute(async () =>
        {
            await _promoService.Remove(items);
            await LoadItems();
        });
    }

    private async Task ActivateItems(object obj)
    {
        var items = (obj as IEnumerable)!.Cast<Promo>();

        await Execute(async () =>
        {
            await _promoService.UpdateActiveStatus(items.Select(x => x.Id).ToList(), true);
            items.ForEach(x => x.IsActive = true);
        });
    }

    private async Task DisableItems(object obj)
    {
        var items = (obj as IEnumerable)!.Cast<Promo>();

        await Execute(async () =>
        {
            await _promoService.UpdateActiveStatus(items.Select(x => x.Id).ToList(), false);
            items.ForEach(x => x.IsActive = false);
        });
    }
}
