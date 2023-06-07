using System.ComponentModel.DataAnnotations;

namespace CurriculumTemplateDemo.Models
{
    public class StudentEvent
    {
        [Key]
        public int Id { get; set; }
        public DateTime EventDate { get; set; }
        public virtual CurriculumEventTemplate CurriculumEventTemplate { get; set; }
        public String StudentFirstName { get; set; }
        public String StudentLastName { get; set; }
        public String Cohort { get; set; }
        public bool Active { get; set; }
        public virtual Attendance? Attendance { get; set; }
    }
}
