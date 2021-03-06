﻿using System.ComponentModel.DataAnnotations;

namespace Anna_Bondarenko_FinalTask.WEB.Models
{
    public class FacultySubjectVm
    {
        public int Id { get; set; }

        [Display(Name = "Subject name")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Name length must be from 3 to 25 characters.")]
        [Required(ErrorMessage = "Please,enter subject name.")]
        public string Name { get; set; }

        [StringLength(1000, MinimumLength = 10, ErrorMessage = "The length of description must be from 10 to 1000 characters.")]
        [Required(ErrorMessage = "Enter the description please.")]
        public string Description { get; set; }
    }
}