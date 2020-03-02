using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using golf.Core.Interfaces;
using golf.Core.Models;
using golf.Core.Models.Entities;
using golf.Core.Repositories;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace golf.Core
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Appsettings

            var DefaultConnection = "Data Source=ws1.dankospark.co.za\\ws1staging,5768;Database=golfDb_dev;User Id=bsjc;Password=Jaap777?";

            //var appSettingsSection = Configuration.GetSection("AppSettings");
            //services.Configure<AppSettings>(appSettingsSection);
            //var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.UTF8.GetBytes("test");

            #endregion Appsettings

            #region JwtOptions

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.ClaimsIssuer = "https://localhost:44314/";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:44314/",
                    ValidAudience = "https://localhost:44314/",
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            #endregion

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyHeader()
                                .AllowAnyMethod();
                });
            });

            services.AddDbContext<golfdbContext>(options =>
                options.UseSqlServer(DefaultConnection, x => x.MigrationsAssembly(typeof(golfdbContext).GetTypeInfo().Assembly.GetName().Name)));
            services.AddIdentity<IdentityUser, IdentityRole>()
      // services.AddDefaultIdentity<IdentityUser>()
      .AddEntityFrameworkStores<golfdbContext>()
      .AddDefaultTokenProviders();

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("1", new OpenApiInfo
                {
                    Title = "Dalevest Golf API",
                    Version = "1.0.0",
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer" }
                        }, new List<string>() }
                });
            });

            #endregion

            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddTransient<IScoreCardRepository, ScoreCardRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<ITeamRepository, TeamRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors(MyAllowSpecificOrigins);
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/1/swagger.json", "Dalevest Golf API V1");
                c.RoutePrefix = String.Empty;
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMvc();
        }
    }
}
