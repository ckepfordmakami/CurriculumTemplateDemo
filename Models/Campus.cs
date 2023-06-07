using System.ComponentModel.DataAnnotations;

namespace CurriculumTemplateDemo.Models
{
    public class Campus
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool required { get; set; }
    }
}
