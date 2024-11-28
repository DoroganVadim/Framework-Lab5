using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage ="Nu a fost introdus login")]
        public string login { get; set; }

        [Required(ErrorMessage = "Nu a fost introdusa parola")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public int role { get; set; }
    }
}
