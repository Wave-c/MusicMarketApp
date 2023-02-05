using MusicMarketKursach.Models.Entitys;
using MusicMarketKursach.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MusicMarketKursach.ViewModels
{
    public class ViewModelUserAccaunt : BindableBase
    {
        public User User { get; }
        private UserAccauntWindow _window;
        public ViewModelUserAccaunt(User user, UserAccauntWindow window) 
        {
            _window = window;
            User = user;
            Login = User.Login;
            TotalAllPurchases = User.TotalAllPurchases;
        }

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                RaisePropertyChanged();
                SaveCommandButton.RaiseCanExecuteChanged();
            }
        }

        private float _totalAllPurchases;
        public float TotalAllPurchases 
        { 
            get => _totalAllPurchases;
            set
            {
                _totalAllPurchases = value;
                RaisePropertyChanged();
            }
        }

        private DelegateCommand _saveCommandButton;
        public DelegateCommand SaveCommandButton => _saveCommandButton ??= new DelegateCommand(SaveLoginCommand_Execute, SaveLoginCommand_CanExecute);

        private DelegateCommand _openCartCommandButton;
        public DelegateCommand OpenCartCommandButton => _openCartCommandButton ??= new DelegateCommand(OpenCartWindowCommand_Execute);

        private void SaveLoginCommand_Execute()
        {
            User.Login = Login;
            _window.DialogResult = true;
            _window.Close();
        }
        private bool SaveLoginCommand_CanExecute()
        {
            return Login != User.Login
                && !string.IsNullOrWhiteSpace(Login);
        }

        private void OpenCartWindowCommand_Execute()
        {
            var window = new CartWindow(User);
            window.Show();
        }
    }
}
