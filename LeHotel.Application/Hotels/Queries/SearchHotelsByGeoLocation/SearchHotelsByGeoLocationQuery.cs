using ErrorOr;
using LeHotel.Application.Common;
using LeHotel.Application.Hotels.Common;
using LeHotel.Domain.HotelAggregate;
using MediatR;

namespace LeHotel.Application.Hotels.Queries.SearchHotelsByGeoLocation
{
    public record SearchHotelsByGeoLocationQuery(int Page, int PageSize, double Latitude, double Longitude) : IRequest<ErrorOr<PagedResult<HotelWithDistance>>>;
}
