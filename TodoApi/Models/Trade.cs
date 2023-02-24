using System;
using System.ComponentModel.DataAnnotations;
using TodoApi.Models.Base;
using TodoApi.Models.Enums;

namespace TodoApi.Models
{
    public class Trade : IBaseEntity
    {
        [Key]
        public int Id { get; set; }

        public TradeType Type { get; set; }

        public int? UserId { get; set; }

        public string Symbol { get; set; }

        public int Shares { get; set; }

        public int? Price { get; set; }

        public long? Timestamp { get; set; }
    }
}
