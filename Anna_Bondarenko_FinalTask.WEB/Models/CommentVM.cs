using System;
using System.ComponentModel.DataAnnotations;

namespace Anna_Bondarenko_FinalTask.WEB.Models
{
    public class CommentVm
    {
        public int Id { get; set; }
        [Display(Name = "User Name")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "The length of the name must be between 5 and 25 characters")]
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "Enter the Date")]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Display(Name = "Text")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The length of the name must be between 3 and 100 characters")]
        [Required(ErrorMessage = "Enter the text of your comment")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
    }
}