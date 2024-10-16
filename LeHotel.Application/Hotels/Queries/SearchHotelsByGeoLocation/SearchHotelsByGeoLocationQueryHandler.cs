using ErrorOr;
using LeHotel.Application.Common;
using LeHotel.Application.Common.Interfaces.DataStructures;
using LeHotel.Application.Hotels.Common;
using LeHotel.Domain.HotelAggregate;
using MediatR;

namespace LeHotel.Application.Hotels.Queries.SearchHotelsByGeoLocation
{
    public class SearchHotelsByGeoLocationQueryHandler : IRequestHandler<SearchHotelsByGeoLocationQuery, ErrorOr<PagedResult<HotelWithDistance>>>
    {
        private readonly IHotelQuadTree _hotelQuadTree;

        public SearchHotelsByGeoLocationQueryHandler(IHotelQuadTree hotelQuadTree)
        {
            _hotelQuadTree = hotelQuadTree;
        }

        public async Task<ErrorOr<PagedResult<HotelWithDistance>>> Handle(SearchHotelsByGeoLocationQuery request, CancellationToken cancellationToken)
        {
            int hotelsToSkip = request.Page == 1 ? 0 : request.PageSize * request.Page - 1;

            IEnumerable<HotelWithDistance> nearbyHotels = _hotelQuadTree.SearchHotelsByProximity(request.Latitude, request.Longitude);

            double maxDistance = nearbyHotels.Max(h => h.Distance);
            double maxPrice = (double)nearbyHotels.Max(h => h.Hotel.Price);

            List<HotelWithDistance> pagedHotels = nearbyHotels
                .Select(hotelWDistance =>
                {
                    double normalizedDistance = maxDistance > 0 ? hotelWDistance.Distance / maxDistance : 0;

                    double normalizedPrice = maxPrice > 0 ? (double)hotelWDistance.Hotel.Price / maxPrice : 0;

                    double combinedScore = 0.75 * normalizedDistance + 0.25 * (double)normalizedPrice;

                    return new
                    {
                        Hotel = hotelWDistance,
                        Score = combinedScore
                    };
                })
                .OrderBy(h => h.Score)
                .Select(h => h.Hotel)
                .Skip(hotelsToSkip)
                .Take(request.PageSize)
                .ToList();

            pagedHotels.ToList().ForEach(h => h.Distance = HaversineDistance(request.Latitude, request.Longitude, h.Hotel.GeoLocation.Latitude, h.Hotel.GeoLocation.Longitude));

            return new PagedResult<HotelWithDistance>(pagedHotels, request.Page, request.PageSize, nearbyHotels.Count());
        }

        private double HaversineDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371;
            var lat1Rad = Math.PI * lat1 / 180;
            var lat2Rad = Math.PI * lat2 / 180;
            var deltaLat = Math.PI * (lat2 - lat1) / 180;
            var deltaLon = Math.PI * (lon2 - lon1) / 180;

            var a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                    Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                    Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }
    }
}
