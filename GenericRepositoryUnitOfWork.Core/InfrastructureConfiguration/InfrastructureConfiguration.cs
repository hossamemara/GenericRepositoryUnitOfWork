using GenericRepositoryUnitOfWork.Core.Interface;
using GenericRepositoryUnitOfWork.Core.MicrosoftIdentity;
using GenericRepositoryUnitOfWork.Core.Repository;
using GenericRepositoryUnitOfWork.DAL.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace GenericRepositoryUnitOfWork.Core.DependencyInjection
{
    public static class InfrastructureConfiguration
    {

        public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IMicrosoftIdentity, MicrosoftIdentityService>();
            services.AddTransient<ITokenService, TokenService>();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 5;
                options.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
                        .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);
            services.AddMemoryCache();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:key"])),
                        ValidIssuer = configuration["Token:Issuer"],
                        ValidateIssuer = true

                    };
                });

            return services;
        }
    }
}
