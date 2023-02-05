using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicMarketKursach.Models.Entitys
{
    #pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    public class MusicRecord : Entity
    {
        public string Title { set; get; }
        public string GroupName { set; get; }
        public string PublisherName { set; get; }
        public int NumberTracks { set; get; }
        public string Genre { set; get; }
        public int YearPublication { set; get; }
        public float CostPrice { set; get; }
        public float PriceForSale { set; get; }
    }
}
