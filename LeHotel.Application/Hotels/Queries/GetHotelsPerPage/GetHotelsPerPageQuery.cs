using ErrorOr;
using LeHotel.Application.Common;
using LeHotel.Domain.HotelAggregate;
using MediatR;

namespace LeHotel.Application.Hotels.Queries.GetHotelsPerPage
{
    public record GetHotelsPerPageQuery(int Page, int PageSize) : IRequest<ErrorOr<PagedResult<Hotel>>>;
}
