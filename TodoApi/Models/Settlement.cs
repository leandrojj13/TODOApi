using System;
using TodoApi.Models.Base;

namespace TodoApi.Models
{
    public class Settlement : IAuditEntity
    {
        public int SettlementLocationID { get; set; }

        public string SettlementLocationName { get; set; }

        public DateTime DateOfService { get; set; }

        public double PricePerMWh { get; set; }

        public double VolumeMWh { get; set; }

        public DateTime InsertDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
