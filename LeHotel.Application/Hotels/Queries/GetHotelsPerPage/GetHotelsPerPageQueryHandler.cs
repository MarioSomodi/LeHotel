using ErrorOr;
using LeHotel.Application.Common;
using LeHotel.Application.Common.Interfaces.Persistance;
using LeHotel.Domain.HotelAggregate;
using MediatR;

namespace LeHotel.Application.Hotels.Queries.GetHotelsPerPage
{
    public class GetHotelsPerPageQueryHandler : IRequestHandler<GetHotelsPerPageQuery, ErrorOr<PagedResult<Hotel>>>
    {
        private readonly IHotelRepository _hotelRepository;

        public GetHotelsPerPageQueryHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<ErrorOr<PagedResult<Hotel>>> Handle(GetHotelsPerPageQuery request, CancellationToken cancellationToken)
        {
            int hotelsToSkip = request.Page == 1 ? 0 : request.PageSize * request.Page - 1;
            int totalRecords = await _hotelRepository.Count(h => true);

            IEnumerable<Hotel> hotels = await _hotelRepository.SkipAndTakeSpecific(hotelsToSkip, request.PageSize);

            return new PagedResult<Hotel>(hotels, request.Page, request.PageSize, totalRecords);
        }
    }
}
