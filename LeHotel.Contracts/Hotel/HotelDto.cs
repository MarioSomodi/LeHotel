using LeHotel.Contracts.GeoLocation;

namespace LeHotel.Contracts.Hotel
{
    public record HotelDto(string Id, string Name, decimal Price, GeoLocationDto GeoLocation);
}
