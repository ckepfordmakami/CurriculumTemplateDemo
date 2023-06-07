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
            Console.WriteLine("Requested");
            return Ok(_context.Curriculums.Include(c=>c.Sections).ThenInclude(c=>c.CurriculumEventTemplates).ToList());
        }
        [HttpGet("getCurriculumModules")]
        public ActionResult<dynamic> GetCurriculumModules()
        {
            /*return Ok(_context.CurriculumsModules.Select(c => new
            {
                Id = c.Id,
                Name = c.Name,
                Required = c.required,
                Curriculum = c.Curriculum,
            }));*/

            var list = _context.CurriculumsModules.Include(c=>c.Curriculum).ToList();
            
            return Ok(list);
        }
        [HttpGet("getAllCurriculumEventTemplates")]
        public ActionResult<dynamic> GetCurriculumEventTemplates()
        {
            return Ok(_context.CurriculumEventTemplates.Include(c=>c.CurriculumSection)
                .Include(c=>c.CurriculumEventType).OrderBy(c=> c.Date).ToList());
        }

        [HttpGet("getCurriculumEventTemplatesByProgram/{program}")]
        public ActionResult<dynamic> GetCurriculumEventTemplates(string program)
        {
            return Ok(_context.CurriculumEventTemplates.Include(c => c.CurriculumSection).ThenInclude(c=>c.Curriculum)
                .Include(c => c.CurriculumEventType).Where(c=>c.CurriculumSection.Curriculum.Program == program).OrderBy(c=> c.Date).Select(
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
            return Ok(_context.StudentEvents.Where(c => c.Cohort == cohort).Include(c => c.CurriculumEventTemplate).ToList());
        }
    }
}
