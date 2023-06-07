using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CurriculumTemplateDemo.Models
{
    public class Curriculum
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Program { get; set; }
        public string Campus { get; set; }
        public virtual ICollection<CurriculumSection> Sections { get; set; }
    }
}
