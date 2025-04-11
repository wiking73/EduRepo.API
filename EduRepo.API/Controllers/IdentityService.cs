﻿using EduRepo.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EduRepo.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using EduRepo.Infrastructure;

namespace EduRepo.API.Controllers
{
    public static class IdentityService
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
IConfiguration config)
        {
            services.AddIdentityCore<Uzytkownik>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<DataContext>();

            // Pobierz klucz z konfiguracji
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:TokenKey"]));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //services.AddScoped<TokenService>();

            return services;
        }
    }
}