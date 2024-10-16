using FluentValidation;

namespace LeHotel.Application.Hotels.Commands.CreateHotel
{
    public class CreateHotelCommandValidator : AbstractValidator<CreateHotelCommand>
    {
        public CreateHotelCommandValidator()
        {
            RuleFor(cHC => cHC.Name).NotEmpty();
            RuleFor(cHC => cHC.Price).NotEmpty().GreaterThan(0);
            RuleFor(cHC => cHC.GeoLocation.Latitude).GreaterThanOrEqualTo(-90).LessThanOrEqualTo(90);
            RuleFor(cHC => cHC.GeoLocation.Longitude).GreaterThanOrEqualTo(-180).LessThanOrEqualTo(180);
        }
    }
}
