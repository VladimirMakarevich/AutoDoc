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
using AutoMapper;
using AutoDoc.Mappers;
using Microsoft.EntityFrameworkCore;

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
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AutoDocContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddTransient<IDocumentService, DocumentService>();
            services.AddTransient<IBookmarkService, BookmarkService>();
            services.AddTransient<IRepositoryBase<Bookmark>, RepositoryBase<Bookmark>>();
            services.AddTransient<IRepositoryBase<Document>, RepositoryBase<Document>>();

            services.AddMvc();

            var config = AutoMapperConfig.GetMapper(services);
            var mapper = config.GetMappers();

            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                //{
                //    HotModuleReplacement = true
                //});
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
