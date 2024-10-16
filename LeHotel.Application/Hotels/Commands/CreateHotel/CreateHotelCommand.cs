using ErrorOr;
using LeHotel.Domain.HotelAggregate;
using LeHotel.Domain.HotelAggregate.ValueObjects;
using MediatR;

namespace LeHotel.Application.Hotels.Commands.CreateHotel
{
    public record CreateHotelCommand(string Name, decimal Price, GeoLocation GeoLocation) : IRequest<ErrorOr<Hotel>>;
}
