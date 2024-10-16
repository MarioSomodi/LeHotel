using MediatR;

namespace LeHotel.Domain.HotelAggregate.Events
{
    public class HotelUpdatedEvent : INotification
    {
        public Hotel Hotel { get; }

        public HotelUpdatedEvent(Hotel hotel)
        {
            Hotel = hotel;
        }
    }
}
