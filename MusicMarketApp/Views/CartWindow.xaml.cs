using MusicMarketKursach.Models.Entitys;
using MusicMarketKursach.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MusicMarketKursach.Views
{
    /// <summary>
    /// Логика взаимодействия для CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        private ViewModelCart _viewModelCart;
        public CartWindow(User user)
        {
            InitializeComponent();
            DataContext = new ViewModelCart(user);
            _viewModelCart = (ViewModelCart)DataContext;
        }
    }
}
