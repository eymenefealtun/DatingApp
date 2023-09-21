using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            services.AddCors();

            services.AddScoped<ITokenService, TokenService>();
            // builder.Services.AddTransient(); // this AddTransient method's Service has a short lifetime // Token service is going to created and disposed of within the request as soon as it used and finished with
            // builder.Services.AddSingleton();  // create a instance of a controller and it never disposed until the application has closed down 

            services.AddScoped<IUserRepository, UserRepository>();  //this is going to  make this injectiable into our usercontroller

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            return services;
        }


    }
}