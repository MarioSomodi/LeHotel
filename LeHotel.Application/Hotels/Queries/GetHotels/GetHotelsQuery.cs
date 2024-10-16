using ErrorOr;
using LeHotel.Domain.HotelAggregate;
using MediatR;

namespace LeHotel.Application.Hotels.Queries.GetHotels
{
    public record GetHotelsQuery : IRequest<ErrorOr<IQueryable<Hotel>>>;
}
