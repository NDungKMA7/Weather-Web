using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProjectKMAIOT.Models
{
    [Table("DailyData")]
    public class DailyDataRecord
    {
        
        [Key]
        public int ID { get; set; }
        public double HumidityMin { get; set; }
        public double HumidityMax { get; set; }

        public double TemperatureMax { get; set; }

        public double TemperatureMin { get; set; }
        public double AirSensor { get; set; }
        
        public int RainSensor { get; set; }
        public string Day { get; set; }
    }
}
