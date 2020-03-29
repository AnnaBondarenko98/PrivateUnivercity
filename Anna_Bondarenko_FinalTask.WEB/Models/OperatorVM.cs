using System.ComponentModel.DataAnnotations;

namespace Anna_Bondarenko_FinalTask.WEB.Models
{
    public class OperatorVm
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Name length must be from 3 to 25 characters.")]
        [Required(ErrorMessage = "Please,enter operator name.")]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Surname length must be from 3 to 25 characters.")]
        [Required(ErrorMessage = "Please,enter operator surname.")]
        public string Surname { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Enter your E-mail correctly.")]
        [Required(ErrorMessage = "Please,enter your E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$", ErrorMessage = "Password must have lower and upper-case character,numerals")]
        [StringLength(10, ErrorMessage = "Length of password must be less than 10 characters ")]
        public string Password { get; set; }
    }
}