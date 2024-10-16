using FluentValidation;
using LeHotel.Application.Common.CustomValidators;

namespace LeHotel.Application.Hotels.Commands.DeleteHotel
{
    public class DeleteHotelCommandValidator : AbstractValidator<DeleteHotelCommand>
    {
        public DeleteHotelCommandValidator()
        {
            RuleFor(dHC => dHC.Id).Must(GuidValidator.ValidateGuid).WithMessage("Id contains invalid id format, id should be GUID.");
        }
    }
}
