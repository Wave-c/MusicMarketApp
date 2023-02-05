using Microsoft.EntityFrameworkCore;
using MusicMarketKursach.Models;
using MusicMarketKursach.Models.Entitys;
using MusicMarketKursach.ViewModels.Interfaces;
using MusicMarketKursach.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace MusicMarketKursach.ViewModels
{
    public class ViewModelMusicMarketWindowUser : BindableBase, IViewModelMarket
    {
        public ViewModelMusicMarketWindowUser(User user)
        {
            ButtonsText = new string[] { "Купить", "Добавить в корзину", "Аккаунт" };
            using (var dbContext = new AppDBContext())
            {
                MusicRecords = new ObservableCollection<MusicRecord>(dbContext.MusicRecords);
            }
            User = user;
        }
        public User User { get; }
        public string[] ButtonsText { get; }
        public IEnumerable<MusicRecord> MusicRecords { get; set; }
        private bool HasCanBuyOrAddToCartMusicRecord => SelectedMusicRecord != null;

        private MusicRecord _selectedMusicRecord;
        public MusicRecord SelectedMusicRecord
        {
            get => _selectedMusicRecord;
            set
            {
                _selectedMusicRecord = value;
                Button1Click.RaiseCanExecuteChanged();
                Button2Click.RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<MusicRecord> SelectedMusicRecords { set; get; }

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

        private DelegateCommand _openUserAccauntWindowCommand;
        private DelegateCommand _addToCartMusicRecordCommand;
        private DelegateCommand _buyMusicRecordCommand;

        public DelegateCommand Button1Click => _buyMusicRecordCommand ??= new DelegateCommand(BuyMusicRecordCommand_Execute, BuyMusicRecordCommand_CanExecute);
        public DelegateCommand Button2Click => _addToCartMusicRecordCommand ??= new DelegateCommand(AddToCartMusicRecordCommand_Execute, AddToCartMusicRecordCommand_CanExecute);
        public DelegateCommand Button3Click => _openUserAccauntWindowCommand ??= new DelegateCommand(OpenUserAccauntWindowCommand_Execute);


        private void OpenUserAccauntWindowCommand_Execute()
        {
            var userAccauntWindow = new UserAccauntWindow(User);
            if(userAccauntWindow.ShowDialog() == true)
            {
                try
                {
                    using (var dbContext = new AppDBContext())
                    {
                        dbContext.Entry(User).State = EntityState.Modified;
                        dbContext.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void AddToCartMusicRecordCommand_Execute()
        {
            using (var dbContext = new AppDBContext())
            {
                AddedToCart addedToCart = new AddedToCart
                {
                    UserId = User.Id,
                    MusicRecordId = SelectedMusicRecord.Id,
                    Id = new Guid()
                };
                dbContext.AddedToCart.Add(addedToCart);
                dbContext.SaveChanges();
            }
        }
        private bool AddToCartMusicRecordCommand_CanExecute()
        {
            return HasCanBuyOrAddToCartMusicRecord;
        }

        private void BuyMusicRecordCommand_Execute()
        {
            try
            {
                using (var dbContext = new AppDBContext())
                {
                    var trans = new Transaction
                    {
                        Id = new Guid(),
                        UserId = User.Id,
                        MusicRecordId = SelectedMusicRecord.Id
                    };
                    dbContext.Transactions.Add(trans);
                    dbContext.Users.Where(x => x.Id == User.Id).First().TotalAllPurchases += SelectedMusicRecord.PriceForSale;
                    dbContext.SaveChanges();
                    User.TotalAllPurchases = dbContext.Users.Where(x => x.Id == User.Id).First().TotalAllPurchases;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool BuyMusicRecordCommand_CanExecute()
        {
            return HasCanBuyOrAddToCartMusicRecord;
        }
    }
}
