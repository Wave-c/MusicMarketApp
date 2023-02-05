using Microsoft.EntityFrameworkCore;
using MusicMarketKursach.Models;
using MusicMarketKursach.Models.Entitys;
using MusicMarketKursach.ViewModels.BaseClasses;
using MusicMarketKursach.ViewModels.Interfaces;
using MusicMarketKursach.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using System.Windows;

namespace MusicMarketKursach.ViewModels
{
    public class ViewModelMusicMarketWindowAdmin : EntityViewModelBase<MusicRecord>, IViewModelMarket
    {        
        public ViewModelMusicMarketWindowAdmin()
        {
            ButtonsText = new string[] { "Добавить", "Удалить", "Редактировать" };
            using(var dbContext = new AppDBContext())
            {
                MusicRecords = new ObservableCollection<MusicRecord>(dbContext.MusicRecords);
            }
        }

        public string[] ButtonsText { get; }
        private bool HasCanEditOrRemoveMusicRecord => SelectedMusicRecord != null;
        private IEnumerable<MusicRecord> _musicRecords;
        public IEnumerable<MusicRecord> MusicRecords 
        {
            get => _musicRecords;
            set
            {
                _musicRecords = value;
                RaisePropertyChanged();
            } 
        }
        private UsersType _role;
        public UsersType Role
        {
            get => _role;
            set
            {
                _role = value;
                RaisePropertyChanged(nameof(HasUserAdminOptions));
            }
        }
        public bool HasUserAdminOptions => Role == UsersType.ADMIN;
        private DelegateCommand _addMusicRecordCommand;
        private DelegateCommand _deleteMusicRecordCommand;
        private DelegateCommand _editMusicRecordCommand;
        public DelegateCommand Button1Click => _addMusicRecordCommand ??= new DelegateCommand(AddCommand_Execute);//кнопка добавления
        public DelegateCommand Button2Click => _deleteMusicRecordCommand ??= new DelegateCommand(DeleteCommand_Execute, DeleteCommand_CanExecute);//кнопка удаления
        public DelegateCommand Button3Click => _editMusicRecordCommand ??= new DelegateCommand(EditCommand_Execute, EditCommand_CanExecute);//кнопка редактирования
        private ObservableCollection<MusicRecord> _selectedMusicRecords;
        public ObservableCollection<MusicRecord> SelectedMusicRecords 
        { 
            get => _selectedMusicRecords; 
            set => _selectedMusicRecords = value;
        }
        private MusicRecord _selectedMusicRecord;
        public MusicRecord SelectedMusicRecord 
        { 
            get => _selectedMusicRecord; 
            set
            {
                _selectedMusicRecord = value;
                Button2Click.RaiseCanExecuteChanged();
                Button3Click.RaiseCanExecuteChanged();
            }
        }

        protected override MusicRecord SelectedItemExtractor()
        {
            return SelectedMusicRecord;
        }

        protected override IEnumerable<MusicRecord> EntitiesCollectionExtractor()
        {
            return MusicRecords;
        }

        protected override bool EditCommand_CanExecute()
        {
            return HasCanEditOrRemoveMusicRecord;
        }

        protected override bool DeleteCommand_CanExecute()
        {
            return HasCanEditOrRemoveMusicRecord;
        }
        protected override void EditCommand_Execute()
        {
            base.EditCommand_Execute();
            MusicRecords = new ObservableCollection<MusicRecord>(MusicRecords);
        }
    }
}
