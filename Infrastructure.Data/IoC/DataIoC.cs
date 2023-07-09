using Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data.IoC
{
    public static class DataIoC
    {
        public static void ConfigureDataServices(this IServiceCollection services)
        {
            //services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
