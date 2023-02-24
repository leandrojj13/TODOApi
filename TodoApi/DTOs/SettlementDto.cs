using System;

namespace TodoApi.DTOs
{
    public class SettlementDto
    {
        public string SettlementLocationName { get; set; }

        public DateTime DateOfService { get; set; }

        public double PricePerMWh { get; set; }

        public double VolumeMWh { get; set; }
    }
}
