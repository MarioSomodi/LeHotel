using LeHotel.Contracts.GeoLocation;

namespace LeHotel.Contracts.Hotel
{
    public record HotelPostRequest(string Name, decimal Price, GeoLocationDto GeoLocation);
}
