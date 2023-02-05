using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicMarketKursach.Models.Entitys
{
    public class User : Entity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public UsersType Role { get; set; }
        public float TotalAllPurchases { get; set; }
    }
    public enum UsersType { ADMIN, USER }
}
