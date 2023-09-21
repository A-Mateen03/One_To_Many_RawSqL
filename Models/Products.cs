using System.ComponentModel.DataAnnotations;

namespace One_To_Many_RawSqL.Models
{
    public class Products
    {
        [Key]
        public int P_ID { get; set; }

        public required string P_Name { get; set; }

        public int P_Price { get; set; }
        public required string P_Detail { get; set; }

        public required string P_ImgUrl { get; set; }



        public List<Sizes> Sizes { get; set; } = new List<Sizes>();
    }
}
