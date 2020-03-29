using System.ComponentModel.DataAnnotations;

namespace Anna_Bondarenko_FinalTask.BLL.DTO
{
    public class ForgotPasswordDto
    {
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter the valid Email")]
        public string Email { get; set; }
    }
}
