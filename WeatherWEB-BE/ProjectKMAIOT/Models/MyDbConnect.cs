using Microsoft.EntityFrameworkCore;

namespace ProjectKMAIOT.Models
{
    public class MyDbConnect:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            var builder = new ConfigurationBuilder();
           
            builder.SetBasePath(Directory.GetCurrentDirectory());
           
            builder.AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            
            string strDbConnectString = configuration.GetConnectionString("DBConnectString").ToString();
          
            optionsBuilder.UseSqlServer(strDbConnectString);
        }
        public DbSet<DailyDataRecord> DailyRecord { get; set; }
        public DbSet<HourlyDataRecord> HourRecord { get; set; }
    }
}
