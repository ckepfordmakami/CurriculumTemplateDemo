using System.ComponentModel.DataAnnotations;

namespace CurriculumTemplateDemo.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }
        public string Student { get; set; }
        public string description { get; set; }
        public string staff { get; set; }
    }
}
