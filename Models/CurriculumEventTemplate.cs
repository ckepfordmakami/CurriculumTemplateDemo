using System.ComponentModel.DataAnnotations;

namespace CurriculumTemplateDemo.Models
{
    public class CurriculumEventTemplate
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public virtual CurriculumSection CurriculumSection { get; set; }
        public virtual CurriculumEventType CurriculumEventType { get; set; }
        public virtual ICollection<StudentEvent> StudentEvents { get; set; }
    }
}
