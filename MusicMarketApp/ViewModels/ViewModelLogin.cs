using MusicMarketKursach.Helpers;
using MusicMarketKursach.Models;
using MusicMarketKursach.Models.Entitys;
using MusicMarketKursach.Views;
using Prism.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MusicMarketKursach.ViewModels
{
#pragma warning disable CS8632 // Аннотацию для ссылочных типов, допускающих значения NULL, следует использовать в коде только в контексте аннотаций "#nullable".
    public class ViewModelLogin
    {
        private readonly LoginWindow _loginWindow;

        public ViewModelLogin(LoginWindow loginWindow)
        {
            _loginWindow = loginWindow;
        }
        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                LoginCommand.RaiseCanExecuteChanged();
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                LoginCommand.RaiseCanExecuteChanged();
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        private bool CanLoginOrRegister =>
            !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);

        private DelegateCommand _loginCommand;
        public DelegateCommand LoginCommand =>
            _loginCommand ??= new DelegateCommand(LoginCommand_Execute, LoginCommand_CanExecute);

        private async void LoginCommand_Execute()
        {
            using (var dbContext = new Models.AppDBContext())
            {
                User? user = null;
                bool loginRes = await Task<bool>.Run(() =>
                {
                    try
                    {
                        user = dbContext.Users.FirstOrDefault(u => u.Login == Login);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    var encryptor = new Encryptor();
                    if (user == null || user.Password != encryptor.GenerateHash(Password))
                    {
                        MessageBox.Show("Неверно введён логин или пароль.");
                        return false;
                    }
                    return true;
                });
                
                if(loginRes)
                    EnterToSystem(user);
            }
        }

        private bool LoginCommand_CanExecute()
        {
            return CanLoginOrRegister;
        }

        private DelegateCommand _registerCommand;


        public DelegateCommand RegisterCommand =>
                    _registerCommand ??= new DelegateCommand(RegisterCommand_Execute, RegisterCommand_CanExecute);

        private async void RegisterCommand_Execute()
        {
            using (var dbContext = new AppDBContext())
            {
                User? user = null;
                bool loginRes = await Task<bool>.Run(() =>
                {
                    try
                    {
                        user = dbContext.Users.FirstOrDefault(u => u.Login == Login);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    var encryptor = new Encryptor();
                    if (user != null)
                    {
                        MessageBox.Show("Такой пользователь существует.");
                        return false;
                    }

                    user = new User
                    {
                        Id = Guid.NewGuid(),
                        Login = Login,
                        Password = encryptor.GenerateHash(Password),
                        Role = UsersType.USER
                    };
                    try
                    {
                        dbContext.Users.Add(user);
                        dbContext.SaveChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return false;
                    }
                });
                if (loginRes)
                    EnterToSystem(user);
            }
        }

        private bool RegisterCommand_CanExecute()
        {
            return CanLoginOrRegister;
        }

        private void EnterToSystem(User user)
        {
            var marketWindow = new MarketWindow(user);
            marketWindow.Show();
            _loginWindow.Close();
        }
    }
}
