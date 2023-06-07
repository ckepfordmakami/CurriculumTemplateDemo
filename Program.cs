using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CurriculumTemplateDemo.Data;
using CurriculumTemplateDemo.Models;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
/*DbContextOptions<CurriculumTemplateDemoContext> opt = new DbContextOptions<CurriculumTemplateDemoContext>();
var context = new CurriculumTemplateDemoContext(opt);

var curriculums = context.Curriculums;
if(curriculums.Any())
{
    curriculums.RemoveRange(curriculums.ToList());
}*/
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CurriculumTemplateDemoContext>(options =>
{
    var connetionString = "server=localhost;port=3306;database=curriculumdemo;Uid=DemoUser;Pwd=password;";
    options.UseMySQL(connetionString,
    mySqlOptionsAction: options =>
    {
        options.EnableRetryOnFailure();
    }
    );
});
builder.Services.AddMvc()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

builder.Services.AddControllersWithViews().AddNewtonsoftJson(s =>
{
    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    s.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000");
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.SeedData();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();


app.MapControllers();

app.Run();


internal static class DbInitializerExtension
{
    public static IApplicationBuilder SeedData(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<CurriculumTemplateDemoContext>();
            DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {

        }

        return app;
    }
}

internal class DbInitializer
{
    internal static void Initialize(CurriculumTemplateDemoContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
        dbContext.Database.EnsureDeleted();
        dbContext.Database.Migrate();
        dbContext.Database.EnsureCreated();

        //Add Curriculums
        var curriculums = new Curriculum[]
        {
                new Curriculum { Campus = "Calgary NE", Name = "ACMT_Y1_0923", Program = "Advanced Clinical Massage Therapy" },
                new Curriculum { Campus = "Bonnie Doon", Name = "BA_0923", Program = "Business Admin" },
        };
        dbContext.Curriculums.AddRange(curriculums);
        dbContext.SaveChanges();

        //Add Curriculum Modules
        var curriculumModules = new CurriculumSection[]
        {
            //ACMT Curriculum
                new CurriculumSection { Curriculum = dbContext.Curriculums.ToList().ElementAt(0), Name = "Lectures", required = true },
                new CurriculumSection { Curriculum = dbContext.Curriculums.ToList().ElementAt(0), Name = "Quizzes", required = true },
                new CurriculumSection { Curriculum = dbContext.Curriculums.ToList().ElementAt(0), Name = "Midterm", required = true },
                new CurriculumSection { Curriculum = dbContext.Curriculums.ToList().ElementAt(0), Name = "Practicum", required = true },

            //BA Curriculum
                new CurriculumSection { Curriculum = dbContext.Curriculums.ToList().ElementAt(1), Name = "Lectures", required = true },
                new CurriculumSection { Curriculum = dbContext.Curriculums.ToList().ElementAt(1), Name = "Quizzes", required = true },
                new CurriculumSection { Curriculum = dbContext.Curriculums.ToList().ElementAt(1), Name = "Midterm", required = true },
                new CurriculumSection { Curriculum = dbContext.Curriculums.ToList().ElementAt(1), Name = "Final", required = true },
        };
        dbContext.CurriculumsModules.AddRange(curriculumModules);
        dbContext.SaveChanges();

        //Add Event Types
        var eventTypes = new CurriculumEventType[]
        {
                new CurriculumEventType { Name = "Lecture" },
                new CurriculumEventType { Name = "Quiz" },
                new CurriculumEventType { Name = "Midterm" },
                new CurriculumEventType { Name = "Final" },
                new CurriculumEventType { Name = "Practicum" },
        };
        dbContext.CurriculumEventTypes.AddRange(eventTypes);
        dbContext.SaveChanges();

        //Add CurriculumEventTemplates
        var eventTemplates = new CurriculumEventTemplate[]
        {
                //ACMT Lectures
                new CurriculumEventTemplate { Name = "Swedish practice", Date = new DateTime(2023, 09, 1, 08, 00, 00), Description = "Include face and abdomen",
                    CurriculumSection = dbContext.CurriculumsModules.ToList().ElementAt(0), CurriculumEventType = dbContext.CurriculumEventTypes.ToList().ElementAt(0), },
                new CurriculumEventTemplate { Name = "Lower Appendicular", Date = new DateTime(2023, 09, 8, 08, 00, 00), Description = "Review Lower appendicular skeleton bones, joints & bony landmarks",
                    CurriculumSection = dbContext.CurriculumsModules.ToList().ElementAt(0), CurriculumEventType = dbContext.CurriculumEventTypes.ToList().ElementAt(0), },
                new CurriculumEventTemplate { Name = "Intro Sciatica", Date = new DateTime(2023, 09, 15, 08, 00, 00), Description = "Review Leg DT, Handout theory, Review workbook",
                    CurriculumSection = dbContext.CurriculumsModules.ToList().ElementAt(0), CurriculumEventType = dbContext.CurriculumEventTypes.ToList().ElementAt(0), },
                //ACMT Quiz
                new CurriculumEventTemplate { Name = "Quiz", Date = new DateTime(2023, 09, 22, 12, 00, 00), Description = "Can you do Massage?",
                    CurriculumSection = dbContext.CurriculumsModules.ToList().ElementAt(1), CurriculumEventType = dbContext.CurriculumEventTypes.ToList().ElementAt(1), },
                //ACMT Midterm
                new CurriculumEventTemplate { Name = "Midterm", Date = new DateTime(2023, 09, 25, 12, 00, 00), Description = "Can you do Massage? Round 2",
                    CurriculumSection = dbContext.CurriculumsModules.ToList().ElementAt(2), CurriculumEventType = dbContext.CurriculumEventTypes.ToList().ElementAt(2), },
                //ACMT Practicum
                new CurriculumEventTemplate { Name = "Practicum", Date = new DateTime(2023, 09, 30, 12, 00, 00), Description = "Can you do Massage for Real?",
                    CurriculumSection = dbContext.CurriculumsModules.ToList().ElementAt(3), CurriculumEventType = dbContext.CurriculumEventTypes.ToList().ElementAt(4), },

                //BA Lectures
                new CurriculumEventTemplate { Name = "Lecture 1", Date = new DateTime(2023, 09, 2, 10, 00, 00), Description = "Intro to Business Admin",
                    CurriculumSection = dbContext.CurriculumsModules.ToList().ElementAt(4), CurriculumEventType = dbContext.CurriculumEventTypes.ToList().ElementAt(0), },
                new CurriculumEventTemplate { Name = "Lecture 2", Date = new DateTime(2023, 09, 9, 10, 00, 00), Description = "Intro to Customer Service",
                    CurriculumSection = dbContext.CurriculumsModules.ToList().ElementAt(4), CurriculumEventType = dbContext.CurriculumEventTypes.ToList().ElementAt(0), },
                new CurriculumEventTemplate { Name = "Lecture 3", Date = new DateTime(2023, 09, 16, 10, 00, 00), Description = "Intro to Patience",
                    CurriculumSection = dbContext.CurriculumsModules.ToList().ElementAt(4), CurriculumEventType = dbContext.CurriculumEventTypes.ToList().ElementAt(0), },
                //BA Quiz
                new CurriculumEventTemplate { Name = "Quiz", Date = new DateTime(2023, 09, 18, 13, 00, 00), Description = "Can you Administrate?",
                    CurriculumSection = dbContext.CurriculumsModules.ToList().ElementAt(5), CurriculumEventType = dbContext.CurriculumEventTypes.ToList().ElementAt(1), },
                //BA Midterm
                new CurriculumEventTemplate { Name = "Midterm", Date = new DateTime(2023, 09, 13, 08, 00, 00), Description = "Can you Administrate? Round 2?",
                    CurriculumSection = dbContext.CurriculumsModules.ToList().ElementAt(6), CurriculumEventType = dbContext.CurriculumEventTypes.ToList().ElementAt(2), },
                //BA Final
                new CurriculumEventTemplate { Name = "Final", Date = new DateTime(2023, 09, 28, 13, 00, 00), Description = "Can you Administrate? Round 3?",
                    CurriculumSection = dbContext.CurriculumsModules.ToList().ElementAt(7), CurriculumEventType = dbContext.CurriculumEventTypes.ToList().ElementAt(3), },
        };
        dbContext.CurriculumEventTemplates.AddRange(eventTemplates);
        dbContext.SaveChanges();

        //Populate Student Events
        var studentEvents = new StudentEvent[]
        {   
            //ACMT Student 1, Lecture 1
            new StudentEvent { CurriculumEventTemplate = dbContext.CurriculumEventTemplates.ToList().ElementAt(0),
                EventDate = dbContext.CurriculumEventTemplates.ToList().ElementAt(0).Date, StudentFirstName = "John",
                StudentLastName = "Smith", Cohort = "2309MB_MT", Active = true, },
            //ACMT Student 1, Lecture 2
            new StudentEvent { CurriculumEventTemplate = dbContext.CurriculumEventTemplates.ToList().ElementAt(1),
                EventDate = dbContext.CurriculumEventTemplates.ToList().ElementAt(1).Date, StudentFirstName = "John",
                StudentLastName = "Smith", Cohort = "2309MB_MT", Active = true },
            //ACMT Student 1, Lecture 3
            new StudentEvent { CurriculumEventTemplate = dbContext.CurriculumEventTemplates.ToList().ElementAt(2),
                EventDate = dbContext.CurriculumEventTemplates.ToList().ElementAt(2).Date, StudentFirstName = "John",
                StudentLastName = "Smith", Cohort = "2309MB_MT", Active = true },
            //ACMT Student 1, Quiz
            new StudentEvent { CurriculumEventTemplate = dbContext.CurriculumEventTemplates.ToList().ElementAt(3),
                EventDate = dbContext.CurriculumEventTemplates.ToList().ElementAt(3).Date, StudentFirstName = "John",
                StudentLastName = "Smith", Cohort = "2309MB_MT", Active = true },
            //ACMT Student 1, Midterm
            new StudentEvent { CurriculumEventTemplate = dbContext.CurriculumEventTemplates.ToList().ElementAt(4),
                EventDate = dbContext.CurriculumEventTemplates.ToList().ElementAt(4).Date, StudentFirstName = "John",
                StudentLastName = "Smith", Cohort = "2309MB_MT", Active = true },
            //ACMT Student 1, Practicum
            new StudentEvent { CurriculumEventTemplate = dbContext.CurriculumEventTemplates.ToList().ElementAt(5),
                EventDate = dbContext.CurriculumEventTemplates.ToList().ElementAt(5).Date, StudentFirstName = "John",
                StudentLastName = "Smith", Cohort = "2309MB_MT", Active = true },


            //ACMT Student 2, Lecture 1
            new StudentEvent { CurriculumEventTemplate = dbContext.CurriculumEventTemplates.ToList().ElementAt(0),
                EventDate = dbContext.CurriculumEventTemplates.ToList().ElementAt(0).Date.AddDays(2), StudentFirstName = "John",
                StudentLastName = "Smith", Cohort = "2309BD_MT", Active = true, },
            //ACMT Student 2, Lecture 2
            new StudentEvent { CurriculumEventTemplate = dbContext.CurriculumEventTemplates.ToList().ElementAt(1),
                EventDate = dbContext.CurriculumEventTemplates.ToList().ElementAt(1).Date.AddDays(2), StudentFirstName = "John",
                StudentLastName = "Smith", Cohort = "2309BD_MT", Active = true },
            //ACMT Student 2, Lecture 3
            new StudentEvent { CurriculumEventTemplate = dbContext.CurriculumEventTemplates.ToList().ElementAt(2),
                EventDate = dbContext.CurriculumEventTemplates.ToList().ElementAt(2).Date.AddDays(2), StudentFirstName = "John",
                StudentLastName = "Smith", Cohort = "2309BD_MT", Active = true },
            //ACMT Student 2, Quiz
            new StudentEvent { CurriculumEventTemplate = dbContext.CurriculumEventTemplates.ToList().ElementAt(3),
                EventDate = dbContext.CurriculumEventTemplates.ToList().ElementAt(3).Date.AddDays(2), StudentFirstName = "John",
                StudentLastName = "Smith", Cohort = "2309BD_MT", Active = true },
            //ACMT Student 2, Midterm
            new StudentEvent { CurriculumEventTemplate = dbContext.CurriculumEventTemplates.ToList().ElementAt(4),
                EventDate = dbContext.CurriculumEventTemplates.ToList().ElementAt(4).Date.AddDays(2), StudentFirstName = "John",
                StudentLastName = "Smith", Cohort = "2309BD_MT", Active = true },
            //ACMT Student 2, Practicum
            new StudentEvent { CurriculumEventTemplate = dbContext.CurriculumEventTemplates.ToList().ElementAt(5),
                EventDate = dbContext.CurriculumEventTemplates.ToList().ElementAt(5).Date.AddDays(2), StudentFirstName = "John",
                StudentLastName = "Smith", Cohort = "2309BD_MT", Active = true },

            //BA Student 1, Lecture 1
            new StudentEvent { CurriculumEventTemplate = dbContext.CurriculumEventTemplates.ToList().ElementAt(6),
                EventDate = dbContext.CurriculumEventTemplates.ToList().ElementAt(6).Date, StudentFirstName = "Jane",
                StudentLastName = "Doe", Cohort = "2309BD_BA", Active = true },
            //BA Student 1, Lecture 2
            new StudentEvent { CurriculumEventTemplate = dbContext.CurriculumEventTemplates.ToList().ElementAt(7),
                EventDate = dbContext.CurriculumEventTemplates.ToList().ElementAt(7).Date, StudentFirstName = "Jane",
                StudentLastName = "Doe", Cohort = "2309BD_BA", Active = true },
            //BA Student 1, Lecture 3
            new StudentEvent { CurriculumEventTemplate = dbContext.CurriculumEventTemplates.ToList().ElementAt(8),
                EventDate = dbContext.CurriculumEventTemplates.ToList().ElementAt(8).Date, StudentFirstName = "Jane",
                StudentLastName = "Doe", Cohort = "2309BD_BA", Active = true },
            //BA Student 1, Quiz 1
            new StudentEvent { CurriculumEventTemplate = dbContext.CurriculumEventTemplates.ToList().ElementAt(9),
                EventDate = dbContext.CurriculumEventTemplates.ToList().ElementAt(9).Date, StudentFirstName = "Jane",
                StudentLastName = "Doe", Cohort = "2309BD_BA", Active = true },
            //BA Student 1, Midterm
            new StudentEvent { CurriculumEventTemplate = dbContext.CurriculumEventTemplates.ToList().ElementAt(10),
                EventDate = dbContext.CurriculumEventTemplates.ToList().ElementAt(10).Date, StudentFirstName = "Jane",
                StudentLastName = "Doe", Cohort = "2309BD_BA", Active = true },
            //BA Student 1, Final
            new StudentEvent { CurriculumEventTemplate = dbContext.CurriculumEventTemplates.ToList().ElementAt(11),
                EventDate = dbContext.CurriculumEventTemplates.ToList().ElementAt(11).Date, StudentFirstName = "Jane",
                StudentLastName = "Doe", Cohort = "2309BD_BA", Active = true },

            //BA Student 2, Lecture 1
            new StudentEvent { CurriculumEventTemplate = dbContext.CurriculumEventTemplates.ToList().ElementAt(6),
                EventDate = dbContext.CurriculumEventTemplates.ToList().ElementAt(6).Date.AddDays(2), StudentFirstName = "Jane",
                StudentLastName = "Doe", Cohort = "2309MB_BA", Active = true },
            //BA Student 2, Lecture 2
            new StudentEvent { CurriculumEventTemplate = dbContext.CurriculumEventTemplates.ToList().ElementAt(7),
                EventDate = dbContext.CurriculumEventTemplates.ToList().ElementAt(7).Date.AddDays(2), StudentFirstName = "Jane",
                StudentLastName = "Doe", Cohort = "2309MB_BA", Active = true },
            //BA Student 2, Lecture 3
            new StudentEvent { CurriculumEventTemplate = dbContext.CurriculumEventTemplates.ToList().ElementAt(8),
                EventDate = dbContext.CurriculumEventTemplates.ToList().ElementAt(8).Date.AddDays(2), StudentFirstName = "Jane",
                StudentLastName = "Doe", Cohort = "2309MB_BA", Active = true },
            //BA Student 2, Quiz 1
            new StudentEvent { CurriculumEventTemplate = dbContext.CurriculumEventTemplates.ToList().ElementAt(9),
                EventDate = dbContext.CurriculumEventTemplates.ToList().ElementAt(9).Date.AddDays(2), StudentFirstName = "Jane",
                StudentLastName = "Doe", Cohort = "2309MB_BA", Active = true },
            //BA Student 2, Midterm
            new StudentEvent { CurriculumEventTemplate = dbContext.CurriculumEventTemplates.ToList().ElementAt(10),
                EventDate = dbContext.CurriculumEventTemplates.ToList().ElementAt(10).Date.AddDays(2), StudentFirstName = "Jane",
                StudentLastName = "Doe", Cohort = "2309MB_BA", Active = true },
            //BA Student 2, Final
            new StudentEvent { CurriculumEventTemplate = dbContext.CurriculumEventTemplates.ToList().ElementAt(11),
                EventDate = dbContext.CurriculumEventTemplates.ToList().ElementAt(11).Date.AddDays(2), StudentFirstName = "Jane",
                StudentLastName = "Doe", Cohort = "2309MB_BA", Active = true },
        };
        dbContext.StudentEvents.AddRange(studentEvents);
        dbContext.SaveChanges();
    }
}
