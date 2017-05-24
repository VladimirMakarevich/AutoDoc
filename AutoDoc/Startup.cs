using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoDoc.DAL.Context;
using AutoDoc.DAL.Entities;
using AutoDoc.DAL.Repository;
using AutoDoc.DAL.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AutoDoc.Mappers;
using AutoDoc.Mappers.Profiles;
using AutoDoc.BL.Core;
using AutoDoc.BL.ModelsUtilities;
using AutoDoc.BL.Parsers;

namespace AutoDoc
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AutoDocContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LocalConnection")));
   
            services.AddTransient<IDocumentService, DocumentService>();
            services.AddTransient<IBookmarkService, BookmarkService>();
            services.AddTransient<IRepositoryBase<Bookmark>, RepositoryBase<Bookmark>>();
            services.AddTransient<IRepositoryBase<Document>, RepositoryBase<Document>>();
            services.AddTransient<IDocumentCore, DocumentCore>();
            services.AddTransient<ITableUtil, TableUtil>();
            services.AddTransient<IImageUtil, ImageUtil>();
            services.AddTransient<ITextUtil, TextUtil>();
            services.AddTransient<IWordTagParser, WordTagParser>();
            services.AddTransient<IWordBookmarkParser, WordBookmarkParser>();

            var config = new AutoMapper.MapperConfiguration(cfg => {
                cfg.AddProfile(new EntityToModel());
                cfg.AddProfile(new ModelToEntity());
            });
            //services.AddAutoMapper();
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc();

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("CorsPolicy"));
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
         
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
