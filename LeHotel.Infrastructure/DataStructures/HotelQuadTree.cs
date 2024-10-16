using LeHotel.Application.Common.Interfaces.DataStructures;
using LeHotel.Application.Common.Interfaces.Persistance;
using LeHotel.Application.Hotels.Common;
using LeHotel.Domain.HotelAggregate;
using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite.Geometries;
using NetTopologySuite.Index;
using NetTopologySuite.Index.Quadtree;

namespace LeHotel.Infrastructure.DataStructures
{
    public class HotelQuadTree : IHotelQuadTree
    {
        private readonly Quadtree<Hotel> _hotelQuadTree;
        private readonly IServiceProvider _serviceProvider;
        private bool _isInitialized = false;

        public HotelQuadTree(IServiceProvider serviceProvider)
        {
            _hotelQuadTree = new Quadtree<Hotel>();
            _serviceProvider = serviceProvider;
        }

        private void Initialize()
        {
            if (_isInitialized) return;

            using (var scope = _serviceProvider.CreateScope())
            {
                var hotelRepository = scope.ServiceProvider.GetRequiredService<IHotelRepository>();
                var allHotels = hotelRepository.GetAll().Result;

                foreach (var hotel in allHotels)
                {
                    _hotelQuadTree.Insert(new Point(hotel.GeoLocation.Latitude, hotel.GeoLocation.Longitude).EnvelopeInternal, hotel);
                }

                _isInitialized = true;
            }
        }

        public void AddHotel(Hotel hotel)
        {
            Initialize();
            var point = new Point(hotel.GeoLocation.Latitude, hotel.GeoLocation.Longitude);
            _hotelQuadTree.Insert(point.EnvelopeInternal, hotel);
        }

        public void RemoveHotel(Hotel hotel)
        {
            Initialize();
            var point = new Point(hotel.GeoLocation.Latitude, hotel.GeoLocation.Longitude);
            _hotelQuadTree.Remove(point.EnvelopeInternal, hotel);
        }
        public void UpdateHotel(Hotel hotel)
        {
            RemoveHotel(hotel);
            AddHotel(hotel);
        }

        public IEnumerable<HotelWithDistance> SearchHotelsByProximity(double lat, double lng)
        {
            Initialize();
            var userPoint = new Point(lat, lng);

            var visitor = new HotelVisitor();

            _hotelQuadTree.Query(new Envelope(double.MinValue, double.MaxValue, double.MinValue, double.MaxValue), visitor);

            var allHotels = visitor.GetResults();

            IEnumerable<HotelWithDistance> hotelsWithDistances = allHotels.Select(hotel => new HotelWithDistance(
                    hotel, 
                    userPoint.Distance(new Point(hotel.GeoLocation.Latitude, hotel.GeoLocation.Longitude))
                )
            );

            var sortedHotels = hotelsWithDistances
                .OrderBy(h => h.Distance);

            return sortedHotels;
        }

        public class HotelVisitor : IItemVisitor<Hotel>
        {
            private readonly List<Hotel> _hotels = new List<Hotel>();

            public void VisitItem(Hotel item)
            {
                _hotels.Add(item);
            }

            public IEnumerable<Hotel> GetResults()
            {
                return _hotels;
            }
        }
    }
}
