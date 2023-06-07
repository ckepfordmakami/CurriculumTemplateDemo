using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CurriculumTemplateDemo.Models
{
    public class CurriculumSection
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public virtual Curriculum Curriculum { get; set; }
        public bool required { get; set; }
        public virtual ICollection<CurriculumEventTemplate> CurriculumEventTemplates { get; set; }
    }
}
