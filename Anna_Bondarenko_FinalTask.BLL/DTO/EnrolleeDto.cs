using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Anna_Bondarenko_FinalTask.BLL.DTO
{
    public class EnrolleeDto
    {
        [Display(Name = "Name")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Name length must be from 3 to 25 characters.")]
        [Required(ErrorMessage = "Please,enter your name.")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Surname length must be from 3 to 25 characters.")]
        [Required(ErrorMessage = "Please,enter your surname.")]
        public string Surname { get; set; }

        [Display(Name = "Patronymic")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Patronymic length must be from 3 to 25 characters.")]
        [Required(ErrorMessage = "Please,enter your patronymic.")]
        public string Patronymic { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Enter your E-mail correctly.")]
        [Required(ErrorMessage = "Please,enter your E-mail")]
        public string Email { get; set; }

        [StringLength(30, MinimumLength = 3, ErrorMessage = "The length of the city name must be from 2 to 30 characters")]
        [Required(ErrorMessage = "Please, enter city name.")]
        public string City { get; set; }

        [StringLength(30, MinimumLength = 3, ErrorMessage = "The length of the region must be from 2 to 30 characters")]
        [Required(ErrorMessage = "Please, enter your region.")]
        public string Region { get; set; }

        [Display(Name = "School")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Name length must be from 3 to 25 characters.")]
        [Required(ErrorMessage = "Please,enter the name of the school or college you graduated from.")]
        public string SchoolName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter your password")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$", ErrorMessage = "The password must contain upper and lowercase Latin letters, numbers")]
        [StringLength(30, ErrorMessage = "The password must be between 5 and 100 characters in length ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public byte[] ImageData { get; set; }

        [DisplayName("Image")]
        public string ImageMimeType { get; set; }
    }
}
