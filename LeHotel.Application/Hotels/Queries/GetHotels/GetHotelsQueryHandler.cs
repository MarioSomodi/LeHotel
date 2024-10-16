using ErrorOr;
using LeHotel.Application.Common.Interfaces.Persistance;
using LeHotel.Domain.HotelAggregate;
using MediatR;

namespace LeHotel.Application.Hotels.Queries.GetHotels
{
    public class GetHotelsQueryHandler : IRequestHandler<GetHotelsQuery, ErrorOr<IQueryable<Hotel>>>
    {
        private readonly IHotelRepository _hotelRepository;

        public GetHotelsQueryHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<ErrorOr<IQueryable<Hotel>>> Handle(GetHotelsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Hotel> hotels = await _hotelRepository.GetAll();
            return hotels.ToErrorOr();
        }
    }
}
