
namespace backend
{
    using backend.Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabase(this.Configuration)
                    .AddIdentity()
                    .AddJwtAuthentication(services.GetApplicationSettings(this.Configuration))
                    .AddApplicationServices()
                    .AddSwagger()
                    .AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            if (env.IsDevelopment())
            {
                app.UseDatabaseErrorPage();
            }
            app.UseSwagger()
               .UseSwaggerUI(c =>
                  {
                      c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                      c.RoutePrefix = string.Empty;
                  }) 
               .UseRouting()
               .UseCors(options => options
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader())
               .UseAuthentication()
               .UseAuthorization()
               .UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    })
               .ApplyMigrations();
        }
    }
}
