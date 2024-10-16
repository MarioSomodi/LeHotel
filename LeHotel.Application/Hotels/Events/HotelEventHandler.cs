using LeHotel.Application.Common.Interfaces.DataStructures;
using LeHotel.Domain.HotelAggregate.Events;
using MediatR;

namespace LeHotel.Application.Hotels.Events
{
    public class HotelEventHandler :
        INotificationHandler<HotelCreatedEvent>,
        INotificationHandler<HotelUpdatedEvent>,
        INotificationHandler<HotelDeletedEvent>
    {
        private readonly IHotelQuadTree _hotelQuadTree;

        public HotelEventHandler(IHotelQuadTree hotelQuadTree)
        {
            _hotelQuadTree = hotelQuadTree;
        }

        public Task Handle(HotelCreatedEvent notification, CancellationToken cancellationToken)
        {
            _hotelQuadTree.AddHotel(notification.Hotel);
            return Task.CompletedTask;
        }

        public Task Handle(HotelUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _hotelQuadTree.UpdateHotel(notification.Hotel);
            return Task.CompletedTask;
        }

        public Task Handle(HotelDeletedEvent notification, CancellationToken cancellationToken)
        {
            _hotelQuadTree.RemoveHotel(notification.Hotel);
            return Task.CompletedTask;
        }
    }
}
