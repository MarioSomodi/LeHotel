using ErrorOr;
using LeHotel.Application.Common.Interfaces.Persistance;
using LeHotel.Domain.HotelAggregate.ValueObjects;
using LeHotel.Domain.HotelAggregate;
using MediatR;
using LeHotel.Domain.Common.AppErrors;

namespace LeHotel.Application.Hotels.Commands.DeleteHotel
{
    public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand, ErrorOr<bool>>
    {
        private readonly IHotelRepository _hotelRepository;

        public DeleteHotelCommandHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<ErrorOr<bool>> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
        {
            if (await _hotelRepository.FindOne(h => h.Id == HotelId.Create(Guid.Parse(request.Id))) is not Hotel hotel)
            {
                return Errors.Common.NotFound(nameof(Hotel));
            }

            await _hotelRepository.Delete(hotel);

            return true;
        }
    }
}
