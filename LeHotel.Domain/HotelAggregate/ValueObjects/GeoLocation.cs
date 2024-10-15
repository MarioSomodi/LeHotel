using LeHotel.Domain.Models;

namespace LeHotel.Domain.HotelAggregate.ValueObjects
{
    public class GeoLocation : ValueObject
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        private GeoLocation(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
        public static GeoLocation Create(double latitude, double longitude)
        {
            return new(latitude, longitude);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Latitude;
            yield return Longitude;
        }
    }
}
