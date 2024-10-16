using LeHotel.Contracts.GeoLocation;
using LeHotel.Domain.HotelAggregate.ValueObjects;
using Mapster;

namespace LeHotel.Api.Common.Mapping
{
    public class GeoLocationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GeoLocation, GeoLocationDto>();
            config.ForType<GeoLocationDto, GeoLocation>()
                .ConstructUsing(
                    src => GeoLocation.Create(src.Latitude, src.Longitude)
                );
        }
    }
}
