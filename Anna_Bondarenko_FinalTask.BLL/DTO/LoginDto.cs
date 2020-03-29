using System.ComponentModel.DataAnnotations;

namespace Anna_Bondarenko_FinalTask.BLL.DTO
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Enter Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter Email correctly")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$", ErrorMessage = "Password must have lower and upper-case character,numerals")]
        [StringLength(10, ErrorMessage = "Length of password must be less than 10 characters ")]
        public string Password { get; set; }
    }
}
