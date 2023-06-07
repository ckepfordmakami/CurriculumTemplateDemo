using CurriculumTemplateDemo.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace CurriculumTemplateDemo.Data
{
    public class CurriculumTemplateDemoContext : DbContext
    {
        public CurriculumTemplateDemoContext(DbContextOptions<CurriculumTemplateDemoContext> opt) : base(opt)
        {
           
        }
        public DbSet<Curriculum> Curriculums { get; set; }
        public DbSet<CurriculumSection> CurriculumsModules { get; set; }
        public DbSet<CurriculumEventType> CurriculumEventTypes { get; set; }
        public DbSet<CurriculumEvent> CurriculumEvents { get; set; }
        public DbSet<CohortEventTemplate> CohortEventTemplates { get; set; }
        public DbSet<StudentEvent> StudentEvents { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
    }
}
