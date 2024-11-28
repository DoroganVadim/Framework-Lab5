using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Nu a fost introdus login")]
        public string login { get; set; }

        [Required(ErrorMessage = "Nu a fost introdusa parola")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(ErrorMessage = "Nu a fost introdusa confirmarea parola")]
        [Compare("password",ErrorMessage ="Nu coincide parola")]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }
    }
}
