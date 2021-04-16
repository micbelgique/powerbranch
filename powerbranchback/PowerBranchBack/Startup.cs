using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using PowerBranchBack.Model;
using System;
using System.Text;

namespace PowerBranchBack
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

                services.AddIdentityCore<ApplicationUser>(options => { });
            new IdentityBuilder(typeof(ApplicationUser), typeof(IdentityRole), services)
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddEntityFrameworkStores<Context>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

            var key = Encoding.ASCII.GetBytes("hMlBevhXPdc9ucEZYcxyMmZ2p11RWGteFbH56YYRoAsvGKHkAS3-Tqg_nPNA9S9V_OZE1XqTLQRNWwGc1roEtd-NatZI6AJ1tHXfQnpMJZiUW8FQKF4il2Km9Im3raVnk5A9G1l6r51C-4YsCUGrRA1oamJFvrmTe3rh2Z0OoB6L2xS9hRnw9p3US939JY7LH_zh3NhwJ3o2D91TlxrLgaCLEy0pnHfL0PItTLAb1fFnVb6OBwp33ICTWfR617ozyb6Bgvr7jhqtY_OrvsnPmGFLuhrnzUqUJNbL37zyPtbvUxMM0S1rtSwVh700fGVhKSQYbIOkl23vrk4dR2DwlQ");
            services
                .AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            services.
                Configure<IdentityOptions>(options =>
                {
                    // Password settings.
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 1;
                    options.Password.RequiredUniqueChars = 0;

                    // Lockout settings.
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;

                    // User settings.
                    options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                    options.User.RequireUniqueEmail = true;
                });
            

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie("Identity.Application");

            services.AddControllers();

            services.AddDbContext<Context>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("Context")));

            services.AddCors();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCors(x => x
               .WithOrigins(
               "http://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials());
        }
    }
}
