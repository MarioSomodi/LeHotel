using LeHotel.Contracts.GeoLocation;

namespace LeHotel.Contracts.Hotel
{
    public record HotelResponse(string Id, string Name, decimal Price, GeoLocationDto GeoLocation);
}
