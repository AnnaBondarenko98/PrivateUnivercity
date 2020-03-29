using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Anna_Bondarenko_FinalTask.Models.Models
{
    public class Mark
    {
        public int Id { get; set; }

        [DisplayName("Mark")]
        [Range(0, 100, ErrorMessage = "The range must be from 0 to 100")]
        public int? StudentMark { get; set; }

        public virtual Enrollee Enrollee { get; set; }
        public virtual ExaminationSubject ExaminationSubject { get; set; }
        public virtual SchoolSubject SchoolSubject { get; set; }

    }
}
