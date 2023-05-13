using DevExpress.Mvvm;
using HandyControl.Controls;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TechStore.BL.Auth;
using TechStore.BL.Exceptions;
using MessageBox = HandyControl.Controls.MessageBox;

namespace TechStore.UI.ViewModels.Administration;

public class AuthViewModel : BaseViewModel
{
    private readonly IAuthorizationService _authService;

    public override string Title => "Терминал | Авторизация";

    public string Login
    {
        get => GetValue<string>(nameof(Login));
        set => SetValue(value, nameof(Login));
    }

    public ICommand LoginCommand { get; }

    public AuthViewModel(IAuthorizationService authService)
    {
        _authService = authService;

        LoginCommand = new AsyncCommand<object>(TryToLogin, x => !string.IsNullOrWhiteSpace(Login));
    }

    public async Task TryToLogin(object passwordControl)
    {
        await Execute(async () =>
        {
            await Task.Delay(200);

            try
            {
                var pswrdBox = (PasswordBox)passwordControl;
                var user = await _authService.Login(Login, pswrdBox.Password);
            }
            catch (AuthorizeException)
            {
                MessageBox.Error("Неверный логин или пароль.", "Ошибка авторизации");
            }
            catch (UserNotFoundAuthorizeException)
            {
                MessageBox.Error("Пользователь с таким логином и паролем не существует.", "Ошибка авторизации");
            }
            catch (Exception e)
            {
                MessageBox.Error(e.Message, "Внутренняя ошибка");
            }
        });
    }
}
