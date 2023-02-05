using MusicMarketKursach.Models.Entitys;
using MusicMarketKursach.ViewModels;
using System.Windows;

namespace MusicMarketKursach.Views
{
    /// <summary>
    /// Логика взаимодействия для UserAccauntWindow.xaml
    /// </summary>
    public partial class UserAccauntWindow : Window
    {
        ViewModelUserAccaunt _viewModel;
        public UserAccauntWindow(User user)
        {
            InitializeComponent();
            DataContext = new ViewModelUserAccaunt(user, this);
            _viewModel = (ViewModelUserAccaunt) DataContext;
        }
    }
}
