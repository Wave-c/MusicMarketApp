using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicMarketKursach.Models.Entitys
{
    public class AddedToCart : Entity
    {
        public Guid UserId { get; set; }
        public Guid MusicRecordId { get; set; }
    }
}
