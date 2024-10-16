using FluentValidation;

namespace LeHotel.Application.Hotels.Queries.SearchHotelsByGeoLocation
{
    public class SearchHotelsByGeoLocationQueryValidator : AbstractValidator<SearchHotelsByGeoLocationQuery>
    {
        public SearchHotelsByGeoLocationQueryValidator()
        {
            RuleFor(sHBGLQ => sHBGLQ.Page).NotEmpty().GreaterThan(0);
            RuleFor(sHBGLQ => sHBGLQ.PageSize).NotEmpty().GreaterThan(0);
            RuleFor(sHBGLQ => sHBGLQ.Latitude).GreaterThanOrEqualTo(-90).LessThanOrEqualTo(90);
            RuleFor(sHBGLQ => sHBGLQ.Longitude).GreaterThanOrEqualTo(-180).LessThanOrEqualTo(180);
        }
    }
}
