﻿using Microsoft.Extensions.DependencyInjection;
using Phone.Repositories.User;
using Phone.Repositories.User.Interfaces;
using Phone.Services.User;
using Phone.Services.User.Interfaces;

namespace Phone.Helpers
{
    /// <summary>
    /// Class for add injections for other interfaces
    /// <summary>
    public static class InitializeServices
    {
        public static void InitializeService(this IServiceCollection services)
        {
            //User
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ISellerService, SellerService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProfileService, ProfileService>();

            services.AddScoped<IUserAuthRepository, AuthRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<ISellerRepository, SellerRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();



        }
    }
}
