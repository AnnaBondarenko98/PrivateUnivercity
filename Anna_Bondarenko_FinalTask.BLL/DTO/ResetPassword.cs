using System.ComponentModel.DataAnnotations;

namespace Anna_Bondarenko_FinalTask.BLL.DTO
{
    public class ResetPassword
    {
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter the correct Email")]
        [Required(ErrorMessage = "Enter Email")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter the Password ")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$", ErrorMessage = "The Password should have lower and uppercase letters and numerics")]
        [StringLength(30, ErrorMessage = "The lenght of password must be from 5 to 100 characters ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Password Confimation")]
        [Compare("Password", ErrorMessage = "The password is not coincide")]
        [Required(ErrorMessage = "Enter the password ")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
