using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartParking.Models
{
    public class Sensor
    {
        [Key]
        public int Id { get; set; }
        [Column("Name", TypeName = "VARCHAR(200)")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

    }
}
