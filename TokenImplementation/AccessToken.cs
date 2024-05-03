namespace HotelManagement.TokenImplementation
{
    public class AccessToken
    {
        public static async Task<IResult> Connect(
            HttpContext ctx,
            JwtOptions jwtOptions)
        {
            // validates the content type of the request
            if (ctx.Request.ContentType != "application/x-www-form-urlencoded")
                return Results.BadRequest(new { Error = "Invalid Request" });

            var formCollection = await ctx.Request.ReadFormAsync();

            // pulls information from the form
            if (formCollection.TryGetValue("grant_type", out var grantType) == false)
                return Results.BadRequest(new { Error = "Invalid Request" });

            if (formCollection.TryGetValue("username", out var userName) == false)
                return Results.BadRequest(new { Error = "Invalid Request" });

            if (formCollection.TryGetValue("password", out var password) == false)
                return Results.BadRequest(new { Error = "Invalid Request" });

            //creates the access token (jwt token)
            var tokenExpiration = TimeSpan.FromSeconds(jwtOptions.ExpirationSeconds);
            var accessToken = TokenEndPoint.CreateAccessToken(
                jwtOptions,
                userName,
                TimeSpan.FromMinutes(60),
                new[] { "read_todo", "create_todo" });

            //returns a json response with the access token
            return Results.Ok(new
            {
                access_token = accessToken,
                expiration = (int)tokenExpiration.TotalSeconds,
                type = "bearer"
            });
        }
    }
}
