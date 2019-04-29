using Microsoft.Extensions.DependencyInjection;
using Phone.Services.User;
using Phone.Services.User.Interfaces;

namespace Phone.Helpers.User
{
    public static class InitializeServices
    {
        public static void InitializeService(this IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
