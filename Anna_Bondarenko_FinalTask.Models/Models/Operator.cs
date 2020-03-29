using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Anna_Bondarenko_FinalTask.Models.Models
{
    public class Operator
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

        [DataType(DataType.EmailAddress, ErrorMessage = "Enter operator's E-mail correctly.")]
        [Required(ErrorMessage = "Please,enter operator's E-mail")]
        public string Email { get; set; }

        public virtual IdentityUser AppCustomer { get; set; }

    }
}
