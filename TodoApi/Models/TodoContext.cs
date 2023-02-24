using ExcelDataReader;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<Settlement> Settlements { get; set; }

        public DbSet<Trade> Trades { get; set; }
        
        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Settlement>()
            .HasKey(s => new { s.SettlementLocationID, s.DateOfService });

            foreach (var settlement in LoadSettlements())
            {
                modelBuilder.Entity<Settlement>().HasData(settlement);
            }
        }
        #endregion

        private List<Settlement> LoadSettlements()
        {
            var settlements = new List<Settlement>();

            var fileName = "../resources/DataResults.xlsx";

            //https://stackoverflow.com/questions/50858209/system-notsupportedexception-no-data-is-available-for-encoding-1252
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using var reader = ExcelReaderFactory.CreateReader(stream);

                reader.Read();

                while (reader.Read()) //Each row of the file
                {
                    settlements.Add(new Settlement
                    {
                        SettlementLocationName = reader.GetValue(0).ToString(),
                        SettlementLocationID = int.Parse(reader.GetValue(1).ToString()),
                        DateOfService = (DateTime)reader.GetValue(2),
                        PricePerMWh = (double)reader.GetValue(3),
                        VolumeMWh = (double)reader.GetValue(4),
                        InsertDate = (DateTime)reader.GetValue(5),
                        ModifiedDate = (DateTime?)reader.GetValue(6),
                    });
                }
            }

            return settlements;
        }
    }
}