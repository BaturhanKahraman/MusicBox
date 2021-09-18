using System.Net.Http;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicBox.API.Extensions;
using MusicBox.Business.Abstract;
using MusicBox.Business.Concrete;
using MusicBox.Business.Utility.ValidationRules;
using MusicBox.DataAccess.Abstract;
using MusicBox.DataAccess.Concrete.NewFolder.AppleSearch;
using MusicBox.Model.Concrete;

namespace MusicBox.API
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
            services.AddControllers().ConfigureApiBehaviorOptions(x =>
            {
                x.SuppressModelStateInvalidFilter = true;
            });
            services.AddScoped<IMusicAfferentService, MusicAfferentManager>();
            services.AddScoped<IMusicAfferentDal,AppleMusicAfferentDal >();
            services.AddScoped<IValidator<SearchModel>, SearchModelValidator>();
            services.AddScoped<HttpClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseMiddleware<ExceptionHandler>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
