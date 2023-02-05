using MusicMarketKursach.Models.Entitys;
using MusicMarketKursach.Views;
using MusicMarketKursach.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MusicMarketKursach.Helpers
{
    public class AddEditWindowFactory
    {
        public IAddEditWindow CreateAddEditWindow<TEntity>(TEntity entity) where TEntity : Entity
        {
            var type = typeof(TEntity);
            switch (type.Name)
            {
                case nameof(MusicRecord):
                    return new AddEditMusicRecordWindow(entity as MusicRecord);
                default:
                    throw new ArgumentException(
                        $@"Класс ""{type.Name}"" не определён для фабрики окон создания/редактирования");
            }
        }
    }
}
