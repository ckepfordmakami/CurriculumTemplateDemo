using System.ComponentModel.DataAnnotations;

namespace CurriculumTemplateDemo.Models
{
    public class CohortEventTemplate
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public String Cohort { get; set; }
        public virtual CurriculumEvent CurriculumEvent { get; set; }
        public virtual ICollection<StudentEvent> StudentEvents { get; set; }
    }
}
