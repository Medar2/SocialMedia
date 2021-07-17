using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Services;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Interfaces;
using SocialMedia.Infrastructure.Options;
using SocialMedia.Infrastructure.Repositories;
using SocialMedia.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace SocialMedia.Infrastructure.Extensions
{
    public static class ServiceColletionExensions
    {
        public static IServiceCollection AddExtensionDbContext(this IServiceCollection services, IConfiguration Configuration)
        {
            //el this, se utiliza para ser implemntado en IServiceCollection
            services.AddDbContext<SocialMediaContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DevConnection")));

            return services;
        }
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<PaginationOptions>(options => Configuration.GetSection("Pagination").Bind(options));
            services.Configure<PasswordOptions>(options =>  Configuration.GetSection("PasswordOptions").Bind(options));

            return services;

        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
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

            return services;

        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, string xmlFileName)
        {

            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1", new OpenApiInfo { Title = "Social Media", Version = "v1" });
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
                doc.IncludeXmlComments(xmlPath);
            });


            return services;

        }
        //public static IServiceCollection AddJWTAuthentication(this IServiceCollection services)
        //{
        //    services.AddAuthentication(options =>
        //    {
        //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


        //    }).AddJwtBearer(Options =>
        //    {
        //        Options.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuer = true,
        //            ValidateAudience = true,
        //            ValidateLifetime = true,
        //            ValidateIssuerSigningKey = true, //validar la firma
        //            ValidIssuer = Configuration["Authentication:Issuer"],
        //            ValidAudience = Configuration["Authentication:Audience"],
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:Secretkey"]))
        //        };

        //    });

        //    return services;

        //}
    }
}
