using LeHotel.Domain.Models;

namespace LeHotel.Domain.HotelAggregate.ValueObjects
{
    public class HotelId : ValueObject
    {
        public Guid Value { get; }

        private HotelId(Guid value)
        {
            Value = value;
        }

        public static HotelId Create(Guid value)
        {
            return new(value);
        }

        public static HotelId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
