using System.ComponentModel.DataAnnotations;

namespace Anna_Bondarenko_FinalTask.WEB.Areas.Enrollee.Models
{
    public class EmailModel
    {
        [Display(Name = "Theme")]
        public string Subject { get; set; }

        [Display(Name = "From")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter Email correctly")]
        [Required(ErrorMessage = "Please, enter E-mail address")]
        public string From { get; set; }

        [Display(Name = "To")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter Email correctly")]
        [Required(ErrorMessage = "Please, enter E-mail address")]
        public string To { get; set; }

        [Display(Name = "Text")]
        [Required]
        public string Body { get; set; }
    }
}