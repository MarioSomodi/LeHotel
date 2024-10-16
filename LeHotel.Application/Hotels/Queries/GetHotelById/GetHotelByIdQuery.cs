using ErrorOr;
using LeHotel.Domain.HotelAggregate;
using MediatR;

namespace LeHotel.Application.Hotels.Queries.GetHotelById
{
    public record GetHotelByIdQuery(string Id) : IRequest<ErrorOr<Hotel>>;
}
