using System.ComponentModel.DataAnnotations;

namespace Anna_Bondarenko_FinalTask.BLL.DTO
{
    public class MessageDto
    {
        [Display(Name = "Theme")]
        public string Subject { get; set; }

        [Display(Name = "From")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter Email correctly")]
        public string From { get; set; }

        [Display(Name = "To")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter Email correctly")]
        public string To { get; set; }

        [Display(Name = "Text")]
        public string Body { get; set; }
    }
}
