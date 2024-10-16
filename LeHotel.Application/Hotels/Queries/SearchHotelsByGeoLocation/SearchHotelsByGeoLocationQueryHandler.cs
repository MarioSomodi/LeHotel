using ErrorOr;
using LeHotel.Application.Common;
using LeHotel.Application.Common.Interfaces.DataStructures;
using LeHotel.Application.Hotels.Common;
using LeHotel.Domain.HotelAggregate;
using MediatR;

namespace LeHotel.Application.Hotels.Queries.SearchHotelsByGeoLocation
{
    public class SearchHotelsByGeoLocationQueryHandler : IRequestHandler<SearchHotelsByGeoLocationQuery, ErrorOr<PagedResult<Hotel>>>
    {
        private readonly IHotelQuadTree _hotelQuadTree;

        public SearchHotelsByGeoLocationQueryHandler(IHotelQuadTree hotelQuadTree)
        {
            _hotelQuadTree = hotelQuadTree;
        }

        public async Task<ErrorOr<PagedResult<Hotel>>> Handle(SearchHotelsByGeoLocationQuery request, CancellationToken cancellationToken)
        {
            int hotelsToSkip = request.Page == 1 ? 0 : request.PageSize * request.Page - 1;

            IEnumerable<HotelWithDistance> nearbyHotels = _hotelQuadTree.SearchHotelsByProximity(request.Latitude, request.Longitude);

            IEnumerable<Hotel> pagedHotels = nearbyHotels
                .Select(hotelWDistance =>
                {
                    double combinedScore = 0.6 * hotelWDistance.Distance + 0.4 * (double)hotelWDistance.Hotel.Price;

                    return new
                    {
                        Hotel = hotelWDistance.Hotel,
                        Score = combinedScore
                    };
                })
                .OrderBy(h => h.Score)
                .Select(h => h.Hotel)
                .Skip(hotelsToSkip)
                .Take(request.PageSize);

            return new PagedResult<Hotel>(pagedHotels, request.Page, request.PageSize, nearbyHotels.Count());
        }
    }
}
