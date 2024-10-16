using LeHotel.Application.Common;
using LeHotel.Application.Hotels.Commands.CreateHotel;
using LeHotel.Application.Hotels.Commands.UpdateHotel;
using LeHotel.Application.Hotels.Common;
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
            config.NewConfig<Hotel, HotelDto>()
                 .Map(dest => dest.Id, src => src.Id.Value);

            config.NewConfig<HotelWithDistance, HotelWithDistanceDto>()
                .Map(dest => dest.Distance, src => src.Distance)
                .Map(dest => dest.Name, src => src.Hotel.Name)
                .Map(dest => dest.Price, src => src.Hotel.Price);

            config.NewConfig<PagedResult<Hotel>, PagedResultResponse<HotelDto>>();

            config.NewConfig<HotelPostRequest, CreateHotelCommand>();
            config.NewConfig<HotelDto, UpdateHotelCommand>();
            config.NewConfig<PagedResult<HotelWithDistance>, PagedResultResponse<HotelWithDistanceDto>>();
        }
    }
}
