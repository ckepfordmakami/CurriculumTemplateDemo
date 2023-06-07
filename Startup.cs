using CurriculumTemplateDemo.Data;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumTemplateDemo
{
    public class Startup
    {
        /*private IWebHostEnvironment CurrentEnvironment { get; set; }
        public IConfiguration Configuration { get; }
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            CurrentEnvironment = env;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CurriculumTemplateDemoContext>(options =>
            {
                options.UseLazyLoadingProxies();
                var connetionString = "server=localhost;port=3306;database=curriculumdemo;Uid=DemoUser;Pwd=password;";
                options.UseMySQL(connetionString,
                mySqlOptionsAction: options =>
                {
                    options.EnableRetryOnFailure();
                }
                );
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddMvc()
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                });
           

        }*/
    }
}
