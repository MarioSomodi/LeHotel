using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeHotel.Contracts.Hotel
{
    public record HotelWithDistanceDto(string Name, decimal Price, double Distance);
}
