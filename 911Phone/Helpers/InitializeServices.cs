using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserAuthService, AuthService>();
            services.AddScoped<IUserAdminService, AdminService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IUserAuthRepository, AuthRepository>();
            services.AddScoped<IUserAdminRepository, AdminRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
        }
    }
}
