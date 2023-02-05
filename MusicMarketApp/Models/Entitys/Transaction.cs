using System;

namespace MusicMarketKursach.Models.Entitys
{
    public class Transaction : Entity
    {
        public Guid UserId { get; set; }
        public Guid MusicRecordId { get; set; }
    }
}
