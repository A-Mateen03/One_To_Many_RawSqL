using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace One_To_Many_RawSqL.Models
{
    public class Sizes
    {
        [Key]
        public int SizeId { get; set; }

        public required string Size { get; set; }

        [ForeignKey("Products")]
        public int P_ID { get; set; }
        public Products? Products { get; set; }
    }
}
