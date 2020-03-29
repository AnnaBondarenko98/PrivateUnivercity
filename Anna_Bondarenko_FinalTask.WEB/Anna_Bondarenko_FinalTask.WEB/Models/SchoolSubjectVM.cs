using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Anna_Bondarenko_FinalTask.Models.Models;

namespace Anna_Bondarenko_FinalTask.WEB.Models
{
    public class SchoolSubjectVm
    {
        public int Id { get; set; }

        [Display(Name = "Subject name")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Name length must be from 3 to 25 characters.")]
        [Required(ErrorMessage = "Please,enter subject name.")]
        public string Name { get; set; }

        public virtual ICollection<Mark> Marks { get; set; }
    }
}