﻿using ErrorOr;
using LeHotel.Application.Common;
using LeHotel.Application.Hotels.Commands.CreateHotel;
using LeHotel.Application.Hotels.Queries.GetHotelById;
using LeHotel.Application.Hotels.Queries.GetHotels;
using LeHotel.Application.Hotels.Queries.GetHotelsPerPage;
using LeHotel.Contracts.Common;
using LeHotel.Contracts.Hotel;
using LeHotel.Domain.HotelAggregate;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LeHotel.Api.Controllers
{
    public class HotelController : ApiController
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public HotelController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet("Page/{page}")]
        [ProducesResponseType<PagedResultResponse<HotelDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ValidationProblem>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int page, int pageSize)
        {
            GetHotelsPerPageQuery getHotelsPerPageQuery = new GetHotelsPerPageQuery(page, pageSize);

            ErrorOr<PagedResult<Hotel>> result = await _sender.Send(getHotelsPerPageQuery);

            return result.Match(
                result => Ok(_mapper.Map<PagedResultResponse<HotelDto>>(result)),
                errors => Problem(errors));
        }

        [HttpGet]
        [ProducesResponseType<IEnumerable<HotelDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            GetHotelsQuery getHotelsQuery = new GetHotelsQuery();

            ErrorOr<IQueryable<Hotel>> result = await _sender.Send(getHotelsQuery);

            return result.Match(
                result => Ok(result.ProjectToType<HotelDto>()),
                errors => Problem(errors));
        }

        [HttpGet("{id}")]
        [ProducesResponseType<HotelDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<ValidationProblem>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            GetHotelByIdQuery getHotelByIdQuery = new GetHotelByIdQuery(id);

            ErrorOr<Hotel> result = await _sender.Send(getHotelByIdQuery);

            return result.Match(
                result => Ok(_mapper.Map<HotelDto>(result)),
                errors => Problem(errors));
        }

        [HttpPost]
        [ProducesResponseType<HotelDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<ValidationProblem>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(HotelPostRequest hotelPostRequest)
        {
            CreateHotelCommand createHotelCommand = _mapper.Map<CreateHotelCommand>(hotelPostRequest);

            ErrorOr<Hotel> result = await _sender.Send(createHotelCommand);

            return result.Match(
                result => Ok(_mapper.Map<HotelDto>(result)),
                errors => Problem(errors));
        }
    }
}
