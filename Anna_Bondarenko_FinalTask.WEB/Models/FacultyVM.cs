using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Anna_Bondarenko_FinalTask.Models.Models;

namespace Anna_Bondarenko_FinalTask.WEB.Models
{
    public class FacultyVm
    {
        public int Id { get; set; }

        [Display(Name = "Faculty name")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Name length must be from 3 to 25 characters.")]
        [Required(ErrorMessage = "Please,enter faculty name.")]
        public string Name { get; set; }

        [Display(Name = "Budget places number")]
        [Range(0, 1000, ErrorMessage = "Enter the number of budget places correctly.")]
        [Required(ErrorMessage = "Please,enter the number of budget places")]
        public int BudgetPlaces { get; set; }

        [Display(Name = "Total number of seats")]
        [Range(0, 1000, ErrorMessage = "Enter the total number of seats correctly.")]
        [Required(ErrorMessage = "Please,enter the total number of seats")]
        public int AllPlaces { get; set; }

        [Display(Name = "Faculty number")]
        [Range(0, 1000, ErrorMessage = "Enter the number of faculty correctly.")]
        [Required(ErrorMessage = "Please,enter the number of faculty")]
        public int FacultyNumber { get; set; }

        public bool Added { get; set; }

        [Display(Name = "Faculty Subjects")]
        public virtual ICollection<FacultySubject> FacultySubjects { get; set; }

        [Display(Name = "Examine Subjects")]
        public virtual ICollection<ExaminationSubject> ExaminationSubjects { get; set; }

        public virtual ICollection<Enrollee> Enrollees { get; set; }
    }
}