using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Anna_Bondarenko_FinalTask.Models.Models
{
    public class Enrollee
    {
        public Enrollee()
        {
            Faculties=new List<Faculty>();
        }

        public int Id { get; set; }

        [DisplayName( "Name")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Name length must be from 3 to 25 characters.")]
        [Required(ErrorMessage = "Please,enter your name.")]
        public string FirstName { get; set; }

        [DisplayName("Surname")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Surname length must be from 3 to 25 characters.")]
        [Required(ErrorMessage = "Please,enter your surname.")]
        public string Surname { get; set; }

        [DisplayName( "Patronymic")]
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

        [DisplayName("School")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Name length must be from 3 to 25 characters.")]
        [Required(ErrorMessage = "Please,enter the name of the school or college you graduated from.")]
        public string SchoolName { get; set; }

        public byte[] ImageData { get; set; }

        [DisplayName("Image")]
        public string ImageMimeType { get; set; }

        [DisplayName("Lock Status")]
        public bool Lock { get; set; }

        public bool Added { get; set; }

        public virtual ICollection<Faculty> Faculties { get; set; }

        public virtual ICollection<StudentStatus> StudentStatuses { get; set; }

        public virtual IdentityUser AppCustomer { get; set; }

        public virtual ICollection<SchoolSubject> SchoolSubjects { get; set; }

        public virtual ICollection<ExaminationSubject> ExaminationSubjects { get; set; }
    }
}
