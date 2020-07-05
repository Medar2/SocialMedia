using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Services;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Filters;
using SocialMedia.Infrastructure.Repositories;
using System;

namespace SocialMedia.Api
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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //.AddNewtonsoftJson Ignora referencias Circulares
            services.AddControllers().AddNewtonsoftJson(options =>
               {
                   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
               }).ConfigureApiBehaviorOptions (options => {
                   //options.SuppressModelStateInvalidFilter = true; //Para suprimir validaciones de Apicontroller
               });

            services.AddDbContext<SocialMediaContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DevConnection")));


            
            //--------------------------
            //Dependencia
            //--------------------------
            //services.AddTransient<IPostRepository, PostMongoRepository>();
            //services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IPostServices, PostServices>();
            //services.AddTransient<IUserRepository, UserRepository>();
            services.AddScoped(typeof(IRespository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
            }).AddFluentValidation(val => //registrar validaciones
            {
                val.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            
            });
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
