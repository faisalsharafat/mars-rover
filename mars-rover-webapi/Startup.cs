using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using mars_rover_BL.Services;
using mars_rover_models.DTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace mars_rover_webapi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            var settingsSection = Configuration.GetSection("AppSettings");
            // Inject AppSettings so that others can use too
            services.Configure<WepApiAppSettings>(settingsSection);


            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new mars_rover_BL.AutoMapper.GalleryImageProfile());
            });

            IMapper mapper = autoMapperConfig.CreateMapper();
            services.AddSingleton(mapper);


            services.AddScoped<IMarsRoverService, MarsRoverService>();
            services.AddScoped<IWebService, WebApiService>();
            services.AddScoped<IFileService, FileService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
