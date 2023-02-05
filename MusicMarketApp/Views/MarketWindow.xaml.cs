using MusicMarketKursach.ViewModels;
using MusicMarketKursach.Models.Entitys;
using MusicMarketKursach.ViewModels.Interfaces;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Linq;

namespace MusicMarketKursach.Views
{
    /// <summary>
    /// Логика взаимодействия для MarketWindow.xaml
    /// </summary>
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

    public partial class MarketWindow : Window
    {
        private IViewModelMarket _marketViewModel;
        public MarketWindow(User user)
        {
            InitializeComponent();
            switch (user.Role)
            {
                case UsersType.ADMIN:
                    DataContext = new ViewModelMusicMarketWindowAdmin();
                    _marketViewModel = (ViewModelMusicMarketWindowAdmin)DataContext;
                    _marketViewModel.Role = user.Role;
                    break;
                case UsersType.USER:
                    DataContext = new ViewModelMusicMarketWindowUser(user);
                    _marketViewModel = (ViewModelMusicMarketWindowUser)DataContext;
                    _marketViewModel.Role = user.Role;
                    break;
            }
        }
        private void CarsDataGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedMusicRecords = ((DataGrid)sender).SelectedItems.Cast<MusicRecord>();
            _marketViewModel.SelectedMusicRecords = new ObservableCollection<MusicRecord>(selectedMusicRecords);
        }
    }
}
