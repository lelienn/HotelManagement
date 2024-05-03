using Microsoft.Extensions.Primitives;

namespace HotelManagement.TokenImplementation
{
    public class TokenEndPoint
    {
        public static async Task<IResult> Connect(
            HttpContext ctx,
            JwtOptions jwtOptions)
        {
            throw new NotImplementedException();
        }

        internal static object CreateAccessToken(JwtOptions jwtOptions, StringValues userName, TimeSpan timeSpan, string[] permissions)
        {
            throw new NotImplementedException();
        }

        public static (string, DateTime) CreateAccessToken(
            JwtOptions jwtOptions,
            string username,
            string[] permissions)
        {
            throw new NotImplementedException();
        }
    }
}
