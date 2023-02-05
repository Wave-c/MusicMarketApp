using MusicMarketKursach.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MusicMarketKursach.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private ViewModelLogin _loginViewModel;
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new ViewModelLogin(this);
            _loginViewModel = (ViewModelLogin)DataContext;
        }
        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            _loginViewModel.Password = passwordBox.Password;
        }
    }
}
