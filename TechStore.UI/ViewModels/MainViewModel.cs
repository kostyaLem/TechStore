using DevExpress.Mvvm;
using HandyControl.Themes;
using HandyControl.Tools;
using HandyControl.Tools.Extension;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using TechStore.BL.Models;
using TechStore.BL.Services.Interfaces;
using TechStore.UI.Models;
using TechStore.UI.Services;

namespace TechStore.UI.ViewModels
{
    public sealed class MainViewModel : BaseItemsViewModel<ViewItem>
    {
        private readonly TimeSpan RepeatInterval = TimeSpan.FromSeconds(5);

        private readonly IStatisticService _statisticService;

        public Statistic Statistic
        {
            get => GetValue<Statistic>(nameof(Statistic));
            set => SetValue(value, nameof(Statistic));
        }

        public ICommand ChangeThemeCommand { get; }
        public ICommand OpenViewCommand { get; }

        public MainViewModel(IEnumerable<ViewItem> viewItems, IStatisticService statisticService)
        {
            Statistic = new Statistic();

            _items.AddRange(viewItems);
            _statisticService = statisticService;

            Title = ViewTitleService.Get(this);
            LoadViewDataCommand = new DelegateCommand(UpdateCounters);
            OpenViewCommand = new DelegateCommand<ViewItem>(OpenView);
            ChangeThemeCommand = new DelegateCommand<ApplicationTheme>(ChangeTheme);
        }

        private void UpdateCounters()
        {
            _ = RepeatExecute(async () =>
            {
                var statistic = await _statisticService.CountStatistic();
                Statistic = statistic;
            }, RepeatInterval);
        }

        private void OpenView(ViewItem viewItem)
        {
            IsUploading = true;

            var view = Container.ServiceProvider.GetRequiredService(viewItem.ViewType) as Window;
            view!.ShowDialog();

            IsUploading = false;
        }

        private void ChangeTheme(ApplicationTheme selectedTheme)
        {
            if (selectedTheme == ThemeManager.Current.ApplicationTheme)
                return;

            ThemeAnimationHelper.AnimateTheme(Application.Current.MainWindow, ThemeAnimationHelper.SlideDirection.Top, 0.3, 1, 0.5);
            ThemeManager.Current.ApplicationTheme = selectedTheme;
            ThemeAnimationHelper.AnimateTheme(Application.Current.MainWindow, ThemeAnimationHelper.SlideDirection.Bottom, 0.3, 0.5, 1);
        }
    }
}
