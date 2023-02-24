namespace TodoApi.Domain
{
    public class SettlementQuery
    {
        public int? SettlementLocationID { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
}
