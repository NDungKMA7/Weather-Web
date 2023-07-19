using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectKMAIOT.Models
{
    [Table("HourlyData")]
    public class HourlyDataRecord
    {

        [Key]
        public int ID { get; set; }
        public double Humidity { get; set; }

        public double Temperature { get; set; }

        public double Air { get; set; }

        public int Rain{ get; set; }

        public string Hour { get; set; }
        public string Day { get; set; }
    }
}
