using System.Collections.Generic;

namespace Anna_Bondarenko_FinalTask.Models.Models
{
    public class Statement
    {
        public int Id { get; set; }

        public virtual ICollection<Enrollee> Enrollee { get; set; }

        public bool IsEnrolled { get; set; }
    }
}
