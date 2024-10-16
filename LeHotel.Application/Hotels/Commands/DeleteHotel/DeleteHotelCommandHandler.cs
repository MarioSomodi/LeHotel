using ErrorOr;
using LeHotel.Application.Common.Interfaces.Persistance;
using LeHotel.Domain.HotelAggregate.ValueObjects;
using LeHotel.Domain.HotelAggregate;
using MediatR;
using LeHotel.Domain.Common.AppErrors;
using LeHotel.Domain.HotelAggregate.Events;

namespace LeHotel.Application.Hotels.Commands.DeleteHotel
{
    public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand, ErrorOr<bool>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IPublisher _publisher;

        public DeleteHotelCommandHandler(IHotelRepository hotelRepository, IPublisher publisher)
        {
            _hotelRepository = hotelRepository;
            _publisher = publisher;
        }

        public async Task<ErrorOr<bool>> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
        {
            if (await _hotelRepository.FindOne(h => h.Id == HotelId.Create(Guid.Parse(request.Id))) is not Hotel hotel)
            {
                return Errors.Common.NotFound(nameof(Hotel));
            }

            await _hotelRepository.Delete(hotel);

            await _publisher.Publish(new HotelDeletedEvent(hotel));

            return true;
        }
    }
}
