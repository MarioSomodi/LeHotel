using FluentValidation;
using LeHotel.Application.Common.CustomValidators;

namespace LeHotel.Application.Hotels.Commands.UpdateHotel
{
    public class UpdateHotelCommandValidator : AbstractValidator<UpdateHotelCommand>
    {
        public UpdateHotelCommandValidator()
        {
            RuleFor(uHC => uHC.Id).Must(GuidValidator.ValidateGuid).WithMessage("Id contains invalid id format, id should be GUID.");
            RuleFor(cHC => cHC.Name).NotEmpty();
            RuleFor(cHC => cHC.Price).NotEmpty().GreaterThan(0);
            RuleFor(cHC => cHC.GeoLocation.Latitude).GreaterThanOrEqualTo(-90).LessThanOrEqualTo(90);
            RuleFor(cHC => cHC.GeoLocation.Longitude).GreaterThanOrEqualTo(-180).LessThanOrEqualTo(180);
        }
    }
}
