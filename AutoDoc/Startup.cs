using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AutoMapper;
using AutoDoc.Mappers;
using AutoDoc.DAL.Repository;
using AutoDoc.DAL.Entities;
using AutoDoc.DAL.Context;
using Microsoft.EntityFrameworkCore;
using AutoDoc.DAL.Services;
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
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AutoDocContext>(options => options.UseSqlServer(connection));

            services.AddMvc();
            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton<BookmarkMapper, BookmarkMapper>();
            services.AddSingleton<DocumentMapper, DocumentMapper>();

            services.AddTransient<IDocumentCore, DocumentCore>();
            services.AddTransient<ITableUtil, TableUtil>();
            services.AddTransient<ITextUtil, TextUtil>();
            services.AddTransient<IImageUtil, ImageUtil>();
            services.AddTransient<IWordBookmarkParser, WordBookmarkParser>();



            services.AddTransient<IRepositoryDocument<Document>, RepositoryDocument>();
            services.AddTransient<IDocumentService, DocumentService>();
            services.AddTransient<IRepositoryBookmark<Bookmark>, RepositoryBookmark>();
            services.AddTransient<IBookmarkService, BookmarkService>();

            services.AddCors(o => o.AddPolicy("EnableCors", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            app.UseCors("EnableCors");

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
