using Microsoft.EntityFrameworkCore;
using MusicMarketKursach.Helpers;
using MusicMarketKursach.Models;
using MusicMarketKursach.Models.Entitys;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MusicMarketKursach.ViewModels.BaseClasses
{
    public abstract class EntityViewModelBase<TEntity>
        : BindableBase where TEntity : Entity, new()
    {
        private readonly AddEditWindowFactory _addEditFactory;

        public EntityViewModelBase()
        {
            _addEditFactory = new AddEditWindowFactory();
        }

        private DelegateCommand _addCommand;
        public DelegateCommand AddCommand =>
                    _addCommand ??= new DelegateCommand(AddCommand_Execute);

        protected virtual void AddCommand_Execute()
        {
            var addingEntity = new TEntity();
            var addWindow = _addEditFactory.CreateAddEditWindow(addingEntity);
            if (addWindow.ShowDialog() == true)
            {
                try
                {
                    using (var dbContext = new AppDBContext())
                    {
                        DbSet<TEntity> dbSet = dbContext.Set<TEntity>();
                        dbSet.Add(addingEntity);
                        dbContext.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                var collection = (ICollection<TEntity>)EntitiesCollectionExtractor();
                collection.Add(addingEntity);
            }
        }

        private DelegateCommand _editCommand;
        public DelegateCommand EditCommand =>
                    _editCommand ??= new DelegateCommand(EditCommand_Execute, EditCommand_CanExecute);

        protected virtual void EditCommand_Execute()
        {
            var editingEntity = SelectedItemExtractor();
            var addWindow = _addEditFactory.CreateAddEditWindow(editingEntity);
            if (addWindow.ShowDialog() == true)
            {
                try
                {
                    using (var dbContext = new AppDBContext())
                    {
                        dbContext.Entry(editingEntity).State = EntityState.Modified;
                        dbContext.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        protected abstract bool EditCommand_CanExecute();

        private DelegateCommand _deleteCommand;

        public DelegateCommand DeleteCommand =>
                            _deleteCommand ??= new DelegateCommand(DeleteCommand_Execute, DeleteCommand_CanExecute);

        protected virtual void DeleteCommand_Execute()
        {
            TEntity selectedItem = SelectedItemExtractor();
            try
            {
                using (var dbContext = new AppDBContext())
                {
                    DbSet<TEntity> dbSet = dbContext.Set<TEntity>();
                    dbSet.Remove(selectedItem);
                    dbContext.SaveChanges();
                }
                ICollection<TEntity> itemsCollection = (ICollection<TEntity>)EntitiesCollectionExtractor();
                itemsCollection.Remove(selectedItem);
                
                itemsCollection = null;
                selectedItem = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected abstract bool DeleteCommand_CanExecute();

        protected abstract TEntity SelectedItemExtractor();
        protected abstract IEnumerable<TEntity> EntitiesCollectionExtractor();
    }
}
