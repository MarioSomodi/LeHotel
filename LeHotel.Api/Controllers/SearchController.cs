using ErrorOr;
using LeHotel.Application.Common;
using LeHotel.Application.Hotels.Common;
using LeHotel.Application.Hotels.Queries.SearchHotelsByGeoLocation;
using LeHotel.Contracts.Common;
using LeHotel.Contracts.Hotel;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LeHotel.Api.Controllers
{
    public class SearchController : ApiController
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public SearchController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType<PagedResultResponse<HotelWithDistanceDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ValidationProblem>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Search(int page, int pageSize, double latitude, double longitude)
        {
            SearchHotelsByGeoLocationQuery searchHotelsByGeoLocationQuery = new SearchHotelsByGeoLocationQuery(page, pageSize, latitude, longitude);

            ErrorOr<PagedResult<HotelWithDistance>> result = await _sender.Send(searchHotelsByGeoLocationQuery);

            return result.Match(
                result => Ok(_mapper.Map<PagedResultResponse<HotelWithDistanceDto>>(result)),
                errors => Problem(errors));
        }
    }
}
