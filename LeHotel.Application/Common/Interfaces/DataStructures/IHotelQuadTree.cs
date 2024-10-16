using LeHotel.Application.Hotels.Common;
using LeHotel.Domain.HotelAggregate;

namespace LeHotel.Application.Common.Interfaces.DataStructures
{
    public interface IHotelQuadTree
    {
        void AddHotel(Hotel hotel);
        void RemoveHotel(Hotel hotel);
        void UpdateHotel(Hotel hotel);
        IEnumerable<HotelWithDistance> SearchHotelsByProximity(double lat, double lng);
    }
}
