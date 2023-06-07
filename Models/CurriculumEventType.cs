using System.ComponentModel.DataAnnotations;

namespace CurriculumTemplateDemo.Models
{
    public class CurriculumEventType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
