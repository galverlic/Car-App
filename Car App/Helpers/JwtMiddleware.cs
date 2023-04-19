//using Car_App.Data.Context;
//using Car_App.Service.Interface;
//using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Text;

//namespace Car_App.Helpers
//{
//    public class JwtMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly AppSettings _appSettings;

//        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
//        {
//            _next = next;
//            _appSettings = appSettings.Value;
//        }

//        public async Task Invoke(HttpContext context, IOwnerService ownerService)
//        {
//            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
//            if (token != null)
//                attachOwnerToContext(context, ownerService, token);

//            await _next(context);
//        }

//        private void attachOwnerToContext(HttpContext context, IOwnerService ownerService, string token)
//        {
//            try
//            {
//                var tokenHandler = new JwtSecurityTokenHandler();
//                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
//                tokenHandler.ValidateToken(token, new TokenValidationParameters
//                {
//                    ValidateIssuerSigningKey = true,
//                    IssuerSigningKey = new SymmetricSecurityKey(key),
//                    ValidateIssuer = false,
//                    ValidateAudience = false,
//                    ClockSkew = TimeSpan.Zero
//                }, out SecurityToken validatedToken);

//                var jwtToken = (JwtSecurityToken)validatedToken;
//                var ownerId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

//                // attach owner to context on successful jwt validation
//                context.Items["Owner"] = ownerService.GetOwnerByIdAsync(ownerId);
//            }
//            catch
//            {
//                // do nothing if jwt validation fails
//                // owner is not attached to context so request won't have access to secure routes
//            }
//        }
//    }
//}
