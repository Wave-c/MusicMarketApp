using MusicMarketKursach.Models.Entitys;
using MusicMarketKursach.ViewModels;
using MusicMarketKursach.Views.Interfaces;
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
    /// Логика взаимодействия для AddEditMusicRecordWindow.xaml
    /// </summary>
    public partial class AddEditMusicRecordWindow : Window, IAddEditWindow
    {
        public AddEditMusicRecordViewModel AddEditMusicRecordViewModel { get; }
        public AddEditMusicRecordWindow(MusicRecord selectedMusicRecord)
        {
            InitializeComponent();
            DataContext = new AddEditMusicRecordViewModel(this, selectedMusicRecord);
            AddEditMusicRecordViewModel = (AddEditMusicRecordViewModel)DataContext;
        }
    }
}
