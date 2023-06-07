using CurriculumTemplateDemo.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace CurriculumTemplateDemo.Data
{
    public class CurriculumTemplateDemoContext : DbContext
    {
        public CurriculumTemplateDemoContext(DbContextOptions<CurriculumTemplateDemoContext> opt) : base(opt)
        {
            /*Database.EnsureDeleted();
            Database.Migrate();
            Database.EnsureCreated();
            
            //Add Curriculums
            var curriculums = new Curriculum[]
            {
                new Curriculum { Campus = "Calgary NE", Name = "ACMT_Y1_0923", Program = "Advanced Clinical Massage Therapy" },
                new Curriculum { Campus = "Bonnie Doon", Name = "BA_0923", Program = "Business Admin" },
                new Curriculum { Campus = "Calgary NE", Name = "HCA_0923", Program = "Health Care Aide" },
            };
            Curriculums.AddRange(curriculums);
            SaveChanges();

            //Add Curriculum Modules
            var curriculumModules = new CurriculumSection[]
            {
                //ACMT Curriculum
                new CurriculumSection { Curriculum = Curriculums.ToList().ElementAt(0), Name = "Lectures", required = true },
                new CurriculumSection { Curriculum = Curriculums.ToList().ElementAt(0), Name = "Quizzes", required = true },
                new CurriculumSection { Curriculum = Curriculums.ToList().ElementAt(0), Name = "Midterm", required = true },
                new CurriculumSection { Curriculum = Curriculums.ToList().ElementAt(0), Name = "Practicum", required = true },

                //BA Curriculum
                new CurriculumSection { Curriculum = Curriculums.ToList().ElementAt(1), Name = "Lectures", required = true },
                new CurriculumSection { Curriculum = Curriculums.ToList().ElementAt(1), Name = "Quizzes", required = true },
                new CurriculumSection { Curriculum = Curriculums.ToList().ElementAt(1), Name = "Midterm", required = true },
                new CurriculumSection { Curriculum = Curriculums.ToList().ElementAt(1), Name = "Final", required = true },

                //HCA Curriculum
                new CurriculumSection { Curriculum = Curriculums.ToList().ElementAt(2), Name = "Lectures", required = true },
                new CurriculumSection { Curriculum = Curriculums.ToList().ElementAt(2), Name = "Quizzes", required = true },
                new CurriculumSection { Curriculum = Curriculums.ToList().ElementAt(2), Name = "Midterm", required = true },
                new CurriculumSection { Curriculum = Curriculums.ToList().ElementAt(2), Name = "Final", required = true },
            };
            CurriculumsModules.AddRange(curriculumModules);
            SaveChanges();

            //Add Event Types
            var eventTypes = new CurriculumEventType[]
            {
                new CurriculumEventType { Name = "Lecture" },
                new CurriculumEventType { Name = "Quiz" },
                new CurriculumEventType { Name = "Midterm" },
                new CurriculumEventType { Name = "Final" },
                new CurriculumEventType { Name = "Practicum" },
            };
            CurriculumEventTypes.AddRange(eventTypes);
            SaveChanges();

            //Add CurriculumEventTemplates
            var eventTemplates = new CurriculumEventTemplate[]
            {
                //ACMT Lectures
                new CurriculumEventTemplate { Name = "Lecture 1", Date = new DateTime(2023, 09, 1), 
                    Section = CurriculumsModules.ToList().ElementAt(0), EventType = CurriculumEventTypes.ToList().ElementAt(0), },
                new CurriculumEventTemplate { Name = "Lecture 2", Date = new DateTime(2023, 09, 8),
                    Section = CurriculumsModules.ToList().ElementAt(0), EventType = CurriculumEventTypes.ToList().ElementAt(0), },
                new CurriculumEventTemplate { Name = "Lecture 3", Date = new DateTime(2023, 09, 15),
                    Section = CurriculumsModules.ToList().ElementAt(0), EventType = CurriculumEventTypes.ToList().ElementAt(0), },
                //ACMT Quiz
                new CurriculumEventTemplate { Name = "Quiz", Date = new DateTime(2023, 09, 22),
                    Section = CurriculumsModules.ToList().ElementAt(1), EventType = CurriculumEventTypes.ToList().ElementAt(1), },
                //ACMT Midterm
                new CurriculumEventTemplate { Name = "Midterm", Date = new DateTime(2023, 09, 25),
                    Section = CurriculumsModules.ToList().ElementAt(2), EventType = CurriculumEventTypes.ToList().ElementAt(2), },
                //ACMT Practicum
                new CurriculumEventTemplate { Name = "Practicum", Date = new DateTime(2023, 09, 30),
                    Section = CurriculumsModules.ToList().ElementAt(3), EventType = CurriculumEventTypes.ToList().ElementAt(4), },

                //BA Lectures
                new CurriculumEventTemplate { Name = "Lecture 1", Date = new DateTime(2023, 09, 2),
                    Section = CurriculumsModules.ToList().ElementAt(5), EventType = CurriculumEventTypes.ToList().ElementAt(0), },
                new CurriculumEventTemplate { Name = "Lecture 2", Date = new DateTime(2023, 09, 9),
                    Section = CurriculumsModules.ToList().ElementAt(5), EventType = CurriculumEventTypes.ToList().ElementAt(0), },
                new CurriculumEventTemplate { Name = "Lecture 3", Date = new DateTime(2023, 09, 16),
                    Section = CurriculumsModules.ToList().ElementAt(5), EventType = CurriculumEventTypes.ToList().ElementAt(0), },
            };*/
        }
        public DbSet<Curriculum> Curriculums { get; set; }
        public DbSet<CurriculumSection> CurriculumsModules { get; set; }
        public DbSet<CurriculumEventType> CurriculumEventTypes { get; set; }
        public DbSet<CurriculumEventTemplate> CurriculumEventTemplates { get; set; }
        public DbSet<StudentEvent> StudentEvents { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
    }
}
