using System.ComponentModel.DataAnnotations;

namespace Anna_Bondarenko_FinalTask.Models.Models
{
    public class StudentStatus
    {
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 3, ErrorMessage = "The length of the status must be from 2 to 30 characters")]
        [Required(ErrorMessage = "Please, enter student status .")]
        public string Status { get; set; }

        public bool FacultyStatus { get; set; }

        public virtual Enrollee Enrollee { get; set; }
        public virtual Faculty Faculty { get; set; }

    }
}
