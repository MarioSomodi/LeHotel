using LeHotel.Application.Common.Interfaces.Persistance;
using LeHotel.Domain.HotelAggregate;
using LeHotel.Domain.HotelAggregate.ValueObjects;
using LeHotel.Infrastructure.Persistance.InMemory;
using LeHotel.Infrastructure.Persistance.InMemory.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LeHotel.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddPersistance();

            return services;
        }

        public static IServiceCollection AddPersistance(this IServiceCollection services)
        {
            services.AddSingleton<IDataStore<Hotel>>(implFactory =>
            {
                return new DataStore<Hotel>(new List<Hotel> {
                        Hotel.Create("Hotel Sunrise", 100, GeoLocation.Create(10, 10)),
                        Hotel.Create("Hotel Sundown", 120, GeoLocation.Create(15, 10))
                    }
                );
            });

            services
                .AddScoped(typeof(IRepository<,>), typeof(MemoryBaseRepository<,>))
                .AddScoped<IHotelRepository, HotelMemoryRepository>();

            return services;
        }
    }
}
