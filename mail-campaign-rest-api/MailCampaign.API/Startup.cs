using MailCampaign.Core.Interface.Repositories;
using MailCampaign.Core.Interface.Services;
using MailCampaign.Core.Services;
using MailCampaign.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace MailCampaign.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IRecipientService, RecipientService>();
            services.AddScoped<IRecipientRepository, RecipientRepository>();

            services.AddScoped<IRecipientGroupService, RecipientGroupService>();
            services.AddScoped<IRecipientGroupRepository, RecipientGroupRepository>();

            services.AddScoped<IUserEmailTemplateService, UserEmailTemplateService>();
            services.AddScoped<IUserEmailTemplateRepository, UserEmailTemplateRepository>();

            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x=> x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
