using ErrorOr;

namespace LeHotel.Domain.Common.AppErrors
{
    public static partial class Errors
    {
        public static class Common
        {
            public static Error NotFound(string resource) => Error.NotFound("Common.NotFound", $"Resource : {resource}, that you have requested was not found.");
        }
    }
}
