using CurriculumTemplateDemo.Data;
using CurriculumTemplateDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace CurriculumTemplateDemo.Controllers
{
    [ApiController]
    [Route("/curriculum")]
    public class CurriculumController : ControllerBase
    {
        private CurriculumTemplateDemoContext _context;
        public CurriculumController(CurriculumTemplateDemoContext context)
        {
            _context = context;
        }
        [HttpGet("getCurriculums")]
        public ActionResult<Curriculum> GetCurriculums()
        {
            var curriculums = _context.Curriculums.Include(c=>c.Sections).ThenInclude(c=>c.CurriculumEvents).ToList();
            var cohorts = _context.CohortEventTemplates.Include(c => c.CurriculumEvent.CurriculumSection.Curriculum).GroupBy(c => c.Cohort)
                .Select(c=> new
                {
                    Cohort = c.Key,
                    CurriculumId = c.First().CurriculumEvent.CurriculumSection.Curriculum.Id,
                }); 
            return Ok(new Tuple<dynamic, dynamic>(curriculums, cohorts));
        }

        [HttpGet("getCohorts")]
        public ActionResult<dynamic> GetCohorts()
        {
            return Ok(_context.StudentEvents.GroupBy(c => c.Cohort).Select(c => new
            {
                name = c.First().Cohort
            }));
        }
        [HttpGet("getCurriculumModules")]
        public ActionResult<dynamic> GetCurriculumModules()
        {
            var list = _context.CurriculumsModules.Include(c=>c.Curriculum).ToList();
            
            return Ok(list);
        }

        [HttpGet("getAllCurriculumEvents")]
        public ActionResult<dynamic> GetCurriculumEvents()
        {
            return Ok(_context.CurriculumEvents.Include(c=>c.CurriculumSection).Include(c=>c.CurriculumEventType));
        }

        [HttpGet("getAllCohortEventTemplates")]
        public ActionResult<dynamic> GetCohortEventTemplates()
        {
            return Ok(_context.CohortEventTemplates.Include(c=>c.CurriculumEvent)
                .ThenInclude(c=>c.CurriculumEventType).OrderBy(c=> c.Date).ToList());
        }

        [HttpGet("getCurriculumEventTemplatesByProgram/{program}")]
        public ActionResult<dynamic> GetCurriculumEventTemplates(string program)
        {
            return Ok(_context.CohortEventTemplates.Include(c=>c.CurriculumEvent.CurriculumSection.Curriculum)
                .Where(c=>c.CurriculumEvent.CurriculumSection.Curriculum.Program == program).OrderBy(c=> c.Date).Select(
                c=> new
                {
                    Id = c.Id,
                    Name = c.Name,
                    Date = c.Date,
                    Description = c.Description
                }).ToList());
        }

        [HttpGet("getStudentEventsByCohort/{cohort}")]
        public ActionResult<dynamic> GetStudentEventsByCohort(string cohort)
        {
            return Ok(_context.StudentEvents.Where(c => c.Cohort == cohort).Include(c => c.CurriculumEventTemplate.CurriculumEvent.CurriculumEventType).ToList());
        }
    }
}
