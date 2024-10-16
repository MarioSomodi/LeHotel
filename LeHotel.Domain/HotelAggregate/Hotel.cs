using LeHotel.Domain.HotelAggregate.ValueObjects;
using LeHotel.Domain.Models;

namespace LeHotel.Domain.HotelAggregate
{
    public class Hotel : AggregateRoot<HotelId>
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public GeoLocation GeoLocation { get; private set; }

        private Hotel(HotelId id, string name, decimal price, GeoLocation geoLocation) : base(id)
        {
            Name = name;
            Price = price;
            GeoLocation = geoLocation;
        }

        public static Hotel Create(string name, decimal price, GeoLocation geoLocation)
        {
            return new(HotelId.CreateUnique(), name, price, geoLocation);
        }

        public void UpdateName(string name)
        {
            Name = name;
        }
        
        public void UpdatePrice(decimal price)
        {
            Price = price;
        }

        public void UpdateGeoLocation(GeoLocation geoLocation)
        {
            GeoLocation = geoLocation;
        }

        /// <summary>
        /// Needed for instancing in ef core
        /// </summary>
#pragma warning disable CS8618
        protected Hotel()
        {
        }
#pragma warning restore CS8618
    }
}
