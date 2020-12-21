using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Services;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Filters;
using SocialMedia.Infrastructure.Interfaces;
using SocialMedia.Infrastructure.Options;
using SocialMedia.Infrastructure.Repositories;
using SocialMedia.Infrastructure.Services;
using System;
using System.IO;
using System.Reflection;
using System.Text;

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
            services.AddControllers(options => 
            {
                options.Filters.Add<GlobalExceptionFilter>();
            }).AddNewtonsoftJson(options =>
               {
                   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                   options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
               }).ConfigureApiBehaviorOptions (options => {
                   //options.SuppressModelStateInvalidFilter = true; //Para suprimir validaciones de Apicontroller
               });

            services.AddDbContext<SocialMediaContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DevConnection")));

           

            //Cargar valores del AppSetting
            services.Configure<PaginationOptions>(Configuration.GetSection("Pagination"));
            services.Configure<PasswordOptions>(Configuration.GetSection("PasswordOptions"));


            //--------------------------
            //Dependencia
            //--------------------------
            //services.AddTransient<IPostRepository, PostMongoRepository>();
            //services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IPostServices, PostServices>();
            services.AddTransient<ISecurityService, SecurityService>();
            //services.AddTransient<IUserRepository, UserRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IPasswordService, PasswordServices>();

            //Para sacar el UrlBase
            services.AddSingleton<IUriService>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteUri);
            });

            //TODO : Primer paso para la documentacion            
            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1", new OpenApiInfo { Title = "Social Media", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                doc.IncludeXmlComments(xmlPath);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


            }).AddJwtBearer(Options =>
            {
                Options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true, //validar la firma
                    ValidIssuer = Configuration["Authentication:Issuer"],
                    ValidAudience = Configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:Secretkey"]))
                };

            });

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

            //TODO : Segunto paso para definir la documentacion
            //segundo paso para generar el json: ir a la url y escribir: ttps://localhost:44327/swagger/v1/swagger.json
            //de esta forma se genera el json de la documentacion, luego guardar archivo
            ////luego abrirlo desde el editor web de swagger editor editor.swagger.io
            app.UseSwagger();

            //Generar Interce de usuario desde el json
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("../swagger/v1/swagger.json","Social Media API V1" );
                //options.RoutePrefix = String.Empty; //para arrancar desde la cumentacion
                //Para ver el resultado, escribir solo swagger desde la ruta principal
                // y se visualizara la documentacion

            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
