using LeHotel.Domain.HotelAggregate;
using LeHotel.Domain.HotelAggregate.ValueObjects;

namespace LeHotel.Application.Common.Interfaces.Persistance
{
    public interface IHotelRepository : IRepository<Hotel, HotelId>
    {
    }
}
