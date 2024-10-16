using ErrorOr;
using LeHotel.Application.Common.Interfaces.Persistance;
using LeHotel.Domain.HotelAggregate;
using LeHotel.Domain.HotelAggregate.ValueObjects;
using MediatR;
using LeHotel.Domain.Common.AppErrors;
using LeHotel.Domain.HotelAggregate.Events;

namespace LeHotel.Application.Hotels.Commands.UpdateHotel
{
    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, ErrorOr<Hotel>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IPublisher _publisher;

        public UpdateHotelCommandHandler(IHotelRepository hotelRepository, IPublisher publisher)
        {
            _hotelRepository = hotelRepository;
            _publisher = publisher;
        }

        public async Task<ErrorOr<Hotel>> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            if (await _hotelRepository.FindOne(h => h.Id == HotelId.Create(Guid.Parse(request.Id))) is not Hotel hotel)
            {
                return Errors.Common.NotFound(nameof(Hotel));
            }

            if(!hotel.Name.Equals(request.Name)) hotel.UpdateName(request.Name);
            if(!hotel.Price.Equals(request.Price)) hotel.UpdatePrice(request.Price);
            if(!hotel.GeoLocation.Equals(request.GeoLocation)) hotel.UpdateGeoLocation(request.GeoLocation);

            await _hotelRepository.Update(hotel);

            await _publisher.Publish(new HotelUpdatedEvent(hotel));

            return hotel;
        }
    }
}
