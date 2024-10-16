using MediatR;

namespace LeHotel.Domain.HotelAggregate.Events
{
    public class HotelCreatedEvent : INotification
    {
        public Hotel Hotel { get; }

        public HotelCreatedEvent(Hotel hotel)
        {
            Hotel = hotel;
        }
    }
}
