using Microsoft.EntityFrameworkCore;
using MusicMarketKursach.Helpers;
using MusicMarketKursach.Models;
using MusicMarketKursach.Models.Entitys;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MusicMarketKursach.ViewModels
{
    class ViewModelCart : BindableBase
    {
        private IEnumerable<MusicRecord> _cartMusicRecords;
        public IEnumerable<MusicRecord> CartMusicRecords 
        {
            get => _cartMusicRecords;
            set
            {
                _cartMusicRecords = value;
                RaisePropertyChanged();
            }
        }

        private MusicRecord _selectedMusicRecord;
        public MusicRecord SelectedMusicRecord
        {
            get => _selectedMusicRecord;
            set
            {
                _selectedMusicRecord = value;
                BuyMusicRecordCommand.RaiseCanExecuteChanged();
                DeleteMusicRecordCommand.RaiseCanExecuteChanged();
            }
        }

        private User _user; 

        public ViewModelCart(User user)
        {
            CartMusicRecords = new List<MusicRecord>();
            _user = user;
            using (var dbContext = new AppDBContext())
            {
                foreach(var item in QueryableToEnumerable<AddedToCart>.Convert(dbContext.AddedToCart.Where(x => x.UserId == _user.Id)))
                {
                    try
                    {
                        CartMusicRecords = CartMusicRecords.Append(dbContext.MusicRecords.Where(x => x.Id == item.MusicRecordId).First());
                    }
                    catch
                    {
                        dbContext.AddedToCart.Remove(item);
                    }
                }
            }
        }

        private DelegateCommand _buyMusicRecordCommand;
        public DelegateCommand BuyMusicRecordCommand => _buyMusicRecordCommand ??= new DelegateCommand(BuyMusicRecordCommand_Execute, DeleteOrBuyMusicRecordCommand_CanExecute);
        private DelegateCommand _deleteMusicRecord;
        public DelegateCommand DeleteMusicRecordCommand => _deleteMusicRecord ??= new DelegateCommand(DeleteMusicRecordCommand_Execute, DeleteOrBuyMusicRecordCommand_CanExecute);

        private void DeleteMusicRecordCommand_Execute()
        {
            AddedToCart deletedMR = new AddedToCart
            {
                MusicRecordId = SelectedMusicRecord.Id,
                UserId = _user.Id,
            };

            using(var dbContext = new AppDBContext())
            {
                deletedMR.Id = dbContext.AddedToCart.Where(x=>x.UserId == _user.Id).First().Id;
                dbContext.AddedToCart.Remove(dbContext.AddedToCart.Where(x=>x.Id == deletedMR.Id).First());
                dbContext.SaveChanges();
            }

            var cartMusicRecordsList = CartMusicRecords.ToList();
            cartMusicRecordsList.Remove(SelectedMusicRecord);
            CartMusicRecords = cartMusicRecordsList;
            SelectedMusicRecord = null;
        }
        private bool DeleteOrBuyMusicRecordCommand_CanExecute()
        {
            return SelectedMusicRecord != null;
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
                        UserId = _user.Id,
                        MusicRecordId = SelectedMusicRecord.Id
                    };
                    dbContext.Transactions.Add(trans);
                    dbContext.Users.Where(x => x.Id == _user.Id).First().TotalAllPurchases += SelectedMusicRecord.PriceForSale;
                    dbContext.SaveChanges();
                    _user.TotalAllPurchases = dbContext.Users.Where(x => x.Id == _user.Id).First().TotalAllPurchases;
                }
                DeleteMusicRecordCommand_Execute();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
