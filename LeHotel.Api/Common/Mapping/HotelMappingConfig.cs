using LeHotel.Application.Common;
using LeHotel.Contracts.Common;
using LeHotel.Contracts.Hotel;
using LeHotel.Domain.HotelAggregate;
using Mapster;

namespace LeHotel.Api.Common.Mapping
{
    public class HotelMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Hotel, HotelResponse>()
                 .Map(dest => dest.Id, src => src.Id.Value);

            config.NewConfig<PagedResult<Hotel>, PagedResultResponse<HotelResponse>>();
        }
    }
}
