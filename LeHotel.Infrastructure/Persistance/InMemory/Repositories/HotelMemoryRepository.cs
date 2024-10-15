using LeHotel.Application.Common.Interfaces.Persistance;
using LeHotel.Domain.HotelAggregate;
using LeHotel.Domain.HotelAggregate.ValueObjects;

namespace LeHotel.Infrastructure.Persistance.InMemory.Repositories
{
    public class HotelMemoryRepository : MemoryBaseRepository<Hotel, HotelId>, IHotelRepository
    {
        public HotelMemoryRepository(IDataStore<Hotel> dataStore) : base(dataStore)
        {
        }
    }
}
