using System;
using AutoMapper;
using HeadmanBot.Data;
using HeadmanBot.Repositories.Implementations;
using HeadmanBot.Repositories.Interfaces;
using HeadmanBot.Services.Implementations;
using HeadmanBot.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace HeadmanBot
{
    public class Startup
    {
        private readonly IWebHostEnvironment env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            this.env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUpdateService, UpdateService>();
            services.AddSingleton<IBotService, BotService>();
            services.AddSingleton<IScheduleService, ScheduleService>();
            services.AddSingleton<IGroupRepository, GroupRepository>();
            
            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(GetDbConnectionString()));

            services.AddSingleton<Func<DataContext>>(sp => () =>
            {
                var context = sp.GetRequiredService<IHttpContextAccessor>().HttpContext;
                return context.RequestServices.GetRequiredService<DataContext>();
            });

            services.Configure<BotConfiguration>(botOptions =>
            {
                botOptions.BotToken = Environment.GetEnvironmentVariable("BOT_TOKEN");
                botOptions.Socks5Host = Environment.GetEnvironmentVariable("SOCKS_5_HOST");
                botOptions.Socks5Port = Convert.ToInt32(Environment.GetEnvironmentVariable("SOCKS_5_PORT"));
            });

            services.AddControllers().AddNewtonsoftJson();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private string GetDbConnectionString()
        {
            NpgsqlConnectionStringBuilder dbConnectionStringBuilder;
            if (env.IsProduction())
                dbConnectionStringBuilder = new NpgsqlConnectionStringBuilder
                {
                    Host = Environment.GetEnvironmentVariable("POSTRES_HOST"),
                    Port = Convert.ToInt32(Environment.GetEnvironmentVariable("POSTRES_PORT")),
                    Database = Environment.GetEnvironmentVariable("POSTGRES_DB"),
                    Username = Environment.GetEnvironmentVariable("POSTRES_USER"),
                    Password = Environment.GetEnvironmentVariable("POSTRES_PASSWORD")
                };
            else
                dbConnectionStringBuilder = new NpgsqlConnectionStringBuilder
                {
                    Host = "localhost",
                    Port = 5432,
                    Database = "postgres",
                    Username = "postgres",
                    Password = "123456789"
                };

            return dbConnectionStringBuilder.ToString();
        }
    }
}