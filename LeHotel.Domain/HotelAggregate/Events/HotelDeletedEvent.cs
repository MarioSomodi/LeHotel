using MediatR;

namespace LeHotel.Domain.HotelAggregate.Events
{
    public class HotelDeletedEvent : INotification
    {
        public Hotel Hotel { get; }

        public HotelDeletedEvent(Hotel hotel)
        {
            Hotel = hotel;
        }
    }
}
