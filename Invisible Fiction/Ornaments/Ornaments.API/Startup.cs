using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ornaments.API.Service;
using Ornaments.API.Service.Interface;
using Ornaments.API.Code;

namespace Ornaments.API
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
            services.AddSwaggerGen();
            services.AddScoped<ISqlService, SqlService>();
            services.AddScoped<IGeneralService, GeneralService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IOrnamentsPositionService, OrnamentsPositionService>();
            services.AddScoped<IOrnamentsService, OrnamentsService>();
            //var contact = new OpenApiContact()
            //{
            //    Name = "Invisible Fiction",
            //    Email = "admin@invisiblefiction.com",
            //    Url = new Uri("http://invisiblefiction.com/")
            //};

            //var license = new OpenApiLicense()
            //{
            //    Name = "My License",
            //    Url = new Uri("http://invisiblefiction.com/")
            //};

            //var info = new OpenApiInfo()
            //{
            //    Version = "v1",
            //    Title = "Swagger Ornaments  API",
            //    Description = "Swagger Ornaments API Description",
            //    TermsOfService = new Uri("http://invisiblefiction.com/"),
            //    Contact = contact,
            //    License = license
            //};

            ////services.AddSwaggerGen(c =>
            ////{
            ////    c.SwaggerDoc("v1", info);
            ////}
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

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                "Swagger Demo API v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
