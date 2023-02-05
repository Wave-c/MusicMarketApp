using MusicMarketKursach.Models.Entitys;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicMarketKursach.ViewModels.Interfaces
{
    public interface IViewModelMarket
    {
        public UsersType Role { get; set; }
        public string[] ButtonsText { get; }
        public IEnumerable<MusicRecord> MusicRecords { get; set; }
        public DelegateCommand Button1Click { get; }
        public DelegateCommand Button2Click { get; }
        public DelegateCommand Button3Click { get; }
        public ObservableCollection<MusicRecord> SelectedMusicRecords { get; set; }
        public MusicRecord SelectedMusicRecord { get; set; }
    }
}
