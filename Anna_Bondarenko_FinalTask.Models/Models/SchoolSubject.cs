﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Anna_Bondarenko_FinalTask.Models.Models
{
    public class SchoolSubject
    {
        public int Id { get; set; }

        [Display(Name = "Subject name")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Name length must be from 3 to 25 characters.")]
        [Required(ErrorMessage = "Please,enter subject name.")]
        public string Name { get; set; }

        public virtual ICollection<Mark> Marks { get; set; }
        public virtual ICollection<Enrollee> Enrollees { get; set; }

    }
}
