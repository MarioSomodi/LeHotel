using ErrorOr;
using LeHotel.Domain.HotelAggregate;
using LeHotel.Domain.HotelAggregate.ValueObjects;
using MediatR;

namespace LeHotel.Application.Hotels.Commands.UpdateHotel
{
    public record UpdateHotelCommand(string Id, string Name, decimal Price, GeoLocation GeoLocation) : IRequest<ErrorOr<Hotel>>;
}
