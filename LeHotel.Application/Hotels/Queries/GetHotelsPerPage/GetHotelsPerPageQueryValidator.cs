using FluentValidation;

namespace LeHotel.Application.Hotels.Queries.GetHotelsPerPage
{
    public class GetHotelsPerPageQueryValidator : AbstractValidator<GetHotelsPerPageQuery>
    {
        public GetHotelsPerPageQueryValidator()
        {
            RuleFor(gHPP => gHPP.Page).NotEmpty().GreaterThan(0);
            RuleFor(cPC => cPC.PageSize).NotEmpty().GreaterThan(0);
        }
    }
}
