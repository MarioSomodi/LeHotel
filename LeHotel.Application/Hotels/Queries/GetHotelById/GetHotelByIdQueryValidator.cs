using FluentValidation;
using LeHotel.Application.Common.CustomValidators;

namespace LeHotel.Application.Hotels.Queries.GetHotelById
{
    public class GetHotelByIdQueryValidator : AbstractValidator<GetHotelByIdQuery>
    {
        public GetHotelByIdQueryValidator()
        {
            RuleFor(gHBID => gHBID.Id).Must(GuidValidator.ValidateGuid).WithMessage("Id contains invalid id format, id should be GUID.");
        }
    }
}
