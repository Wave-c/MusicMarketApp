using System;
using System.ComponentModel.DataAnnotations;

namespace MusicMarketKursach.Models.Entitys
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
