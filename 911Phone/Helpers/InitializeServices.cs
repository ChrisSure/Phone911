using Microsoft.Extensions.DependencyInjection;
using Phone.Repositories.User;
using Phone.Repositories.User.Interfaces;
using Phone.Services.User;
using Phone.Services.User.Interfaces;

namespace Phone.Helpers
{
    public static class InitializeServices
    {
        public static void InitializeService(this IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
        }
    }
}
