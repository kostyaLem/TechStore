using DevExpress.Mvvm;
using HandyControl.Controls;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TechStore.BL.Auth;
using TechStore.BL.Exceptions;
using MessageBox = HandyControl.Controls.MessageBox;

namespace TechStore.UI.ViewModels;

public class AuthViewModel : BaseViewModel
{
    private readonly AuthorizationService _authService;

    public override string Title => "Авторизация";

    public string Login
    {
        get => GetValue<string>(nameof(Login));
        set => SetValue(value, nameof(Login));
    }

    public bool IsUploading
    {
        get => GetValue<bool>(nameof(IsUploading));
        set => SetValue(value, nameof(IsUploading));
    }

    public ICommand LoginCommand { get; }

    public AuthViewModel(AuthorizationService authService)
    {
        _authService = authService;

        LoginCommand = new AsyncCommand<object>(TryToLogin, x => !string.IsNullOrWhiteSpace(Login));
    }

    public async Task TryToLogin(object passwordControl)
    {
        IsUploading = true;
        await Task.Delay(500);

        var pswrdBox = (PasswordBox)passwordControl;

        try
        {
            var user = await _authService.Login(Login, pswrdBox.Password);
        }
        catch (AuthorizeException)
        {
            MessageBox.Error("Неверный логин или пароль", "Ошибка авторизации");
        }
        catch (Exception e)
        {
            MessageBox.Error(e.Message, "Внутренняя ошибка");
        }

        IsUploading = false;
    }
}
