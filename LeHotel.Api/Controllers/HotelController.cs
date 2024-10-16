using LeHotel.Application.Common;
using LeHotel.Application.Hotels.Queries.GetHotels;
using LeHotel.Application.Hotels.Queries.GetHotelsPerPage;
using LeHotel.Contracts.Common;
using LeHotel.Contracts.Hotel;
using LeHotel.Domain.HotelAggregate;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LeHotel.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public HotelController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet("{page}")]
        public async Task<ActionResult<PagedResult<HotelResponse>>> Get(int page, int pageSize)
        {
            GetHotelsPerPageQuery getHotelsPerPageQuery = new GetHotelsPerPageQuery(page, pageSize);

            PagedResult<Hotel> result = await _sender.Send(getHotelsPerPageQuery);

            return Ok(_mapper.Map<PagedResultResponse<HotelResponse>>(result));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelResponse>>> Get()
        {
            GetHotelsQuery getHotelsQuery = new GetHotelsQuery();

            IQueryable<Hotel> result = await _sender.Send(getHotelsQuery);

            return Ok(result.ProjectToType<HotelResponse>());
        }
    }
}
