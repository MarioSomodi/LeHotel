﻿using ErrorOr;
using LeHotel.Application.Common.Interfaces.Persistance;
using LeHotel.Domain.HotelAggregate;
using LeHotel.Domain.HotelAggregate.Events;
using LeHotel.Domain.HotelAggregate.ValueObjects;
using MediatR;

namespace LeHotel.Application.Hotels.Commands.CreateHotel
{
    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, ErrorOr<Hotel>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IPublisher _publisher;

        public CreateHotelCommandHandler(IHotelRepository hotelRepository, IPublisher publisher)
        {
            _hotelRepository = hotelRepository;
            _publisher = publisher;
        }

        public async Task<ErrorOr<Hotel>> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            GeoLocation geoLocation = GeoLocation.Create(request.GeoLocation.Latitude, request.GeoLocation.Longitude);

            Hotel hotel = Hotel.Create(request.Name, request.Price, geoLocation);

            await _hotelRepository.Add(hotel);

            await _publisher.Publish(new HotelCreatedEvent(hotel));

            return hotel;
        }
    }
}
