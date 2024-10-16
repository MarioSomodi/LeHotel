using ErrorOr;
using MediatR;

namespace LeHotel.Application.Hotels.Commands.DeleteHotel
{
    public record DeleteHotelCommand(string Id) : IRequest<ErrorOr<bool>>;
}
