

using System.ComponentModel.DataAnnotations;

namespace CurriculumTemplateDemo.Models
{
    public class CurriculumEvent
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public virtual CurriculumSection CurriculumSection { get; set; }
        public virtual CurriculumEventType CurriculumEventType { get; set; }
    }
}
