using DevExpress.Mvvm;
using HandyControl.Tools;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TechStore.BL.Models;
using TechStore.BL.Models.Products;
using TechStore.BL.Services.Interfaces;
using TechStore.UI.Enums;

namespace TechStore.UI.ViewModels
{
    public sealed class MainViewModel : BaseItemsViewModel<Product>
    {
        private readonly IStatisticService _statisticService;

        public Statistic Statistic
        {
            get => GetValue<Statistic>(nameof(Statistic));
            set => SetValue(value, nameof(Statistic));
        }

        public ICommand ChangeThemeCommand { get; private set; }

        public MainViewModel(IStatisticService statisticService)
        {
            Statistic = new Statistic();

            _statisticService = statisticService;

            LoadViewDataCommand = new AsyncCommand(UpdateCounters);
            ChangeThemeCommand = new DelegateCommand<object>(ChangeTheme);
        }

        private async Task UpdateCounters()
        {
            _ = RepeatExecute(async () =>
            {
                var statistic = await _statisticService.CountStatistic();
                Statistic = statistic;
            }, TimeSpan.FromSeconds(5));
        }

        private void ChangeTheme(object obj)
        {
            var selectedTheme = (ThemeStyleMode)obj == ThemeStyleMode.Light
                ? HandyControl.Themes.ApplicationTheme.Light
                : HandyControl.Themes.ApplicationTheme.Dark;

            if (selectedTheme == HandyControl.Themes.ThemeManager.Current.ApplicationTheme)
                return;

            ThemeAnimationHelper.AnimateTheme(Application.Current.MainWindow, ThemeAnimationHelper.SlideDirection.Top, 0.3, 1, 0.5);

            HandyControl.Themes.ThemeManager.Current.ApplicationTheme = selectedTheme;

            ThemeAnimationHelper.AnimateTheme(Application.Current.MainWindow, ThemeAnimationHelper.SlideDirection.Bottom, 0.3, 0.5, 1);
        }
    }
}
