using MusicMarketKursach.Models.Entitys;
using MusicMarketKursach.ViewModels.BaseClasses;
using MusicMarketKursach.Views;
using Prism.Commands;
using System;

namespace MusicMarketKursach.ViewModels
{
    public class AddEditMusicRecordViewModel : AddEditViewModelBase
    {
        private MusicRecord _musicRecord;
        private string? _title;
        private string? _groupName;
        private string? _publisherName;
        private int _numberTracks;
        private string? _genre;
        private int _yearPublication;
        private float _costPrice;
        private float _priceForSale;

        public AddEditMusicRecordViewModel(AddEditMusicRecordWindow window, MusicRecord musicRecord)
            : base(window)
        {
            _musicRecord = musicRecord;
            Title = musicRecord.Title;
            GroupName = musicRecord.GroupName;
            PublisherName = musicRecord.PublisherName;
            NumberTracks = musicRecord.NumberTracks;
            Genre = musicRecord.Genre;
            YearPublication = musicRecord.YearPublication;
            CostPrice = musicRecord.CostPrice;
            PriceForSale = musicRecord.PriceForSale;
        }
        public string? Title 
        { 
            get => _title;
            set
            {
                _title = value;
                RaisePropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        public string? GroupName 
        { 
            get => _groupName;
            set
            {
                _groupName = value;
                RaisePropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        public string? PublisherName 
        { 
            get => _publisherName;
            set
            {
                _publisherName = value;
                RaisePropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        public int NumberTracks 
        { 
            get => _numberTracks;
            set
            {
                _numberTracks = value;
                RaisePropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        public string? Genre 
        { 
            get => _genre;
            set
            {
                _genre = value;
                RaisePropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        public int YearPublication 
        { 
            get => _yearPublication;
            set
            {
                _yearPublication = value;
                RaisePropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        public float CostPrice 
        { 
            get => _costPrice;
            set
            {
                _costPrice = value;
                RaisePropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        public float PriceForSale 
        { 
            get => _priceForSale;
            set
            {
                _priceForSale = value;
                RaisePropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        protected override void SaveEntityOperation()
        {
            _musicRecord.Title = Title;
            _musicRecord.GroupName = GroupName;
            _musicRecord.PublisherName = PublisherName;
            _musicRecord.NumberTracks = NumberTracks;
            _musicRecord.Genre = Genre;
            _musicRecord.YearPublication = YearPublication;
            _musicRecord.CostPrice = CostPrice;
            _musicRecord.PriceForSale = PriceForSale;
            _musicRecord.Id = Guid.Empty.Equals(_musicRecord.Id) ? Guid.NewGuid() : _musicRecord.Id;
        }

        protected override bool SaveCommand_CanExecute()
        {
            return !string.IsNullOrWhiteSpace(Title)
                && !string.IsNullOrWhiteSpace(GroupName)
                && !string.IsNullOrWhiteSpace(PublisherName)
                && !string.IsNullOrWhiteSpace(Genre)
                && NumberTracks != default
                && YearPublication != default
                && CostPrice != default
                && PriceForSale != default;
        }
    }
}
