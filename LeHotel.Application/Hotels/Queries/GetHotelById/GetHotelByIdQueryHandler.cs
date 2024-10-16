using ErrorOr;
using LeHotel.Application.Common.Interfaces.Persistance;
using LeHotel.Domain.HotelAggregate;
using LeHotel.Domain.HotelAggregate.ValueObjects;
using MediatR;
using LeHotel.Domain.Common.AppErrors;

namespace LeHotel.Application.Hotels.Queries.GetHotelById
{
    public class GetHotelByIdQueryHandler : IRequestHandler<GetHotelByIdQuery, ErrorOr<Hotel>>
    {
        private readonly IHotelRepository _hotelRepository;

        public GetHotelByIdQueryHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<ErrorOr<Hotel>> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
        {
            if (await _hotelRepository.FindOne(h => h.Id == HotelId.Create(Guid.Parse(request.Id))) is not Hotel hotel)
            {
                return Errors.Common.NotFound(nameof(Hotel));
            }

            return hotel;
        }
    }
}
