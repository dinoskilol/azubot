using Azubot.ApiServer.Database.Entities;
using Azubot.Shared.Http.Responses.Auth;
using Microsoft.AspNetCore.Mvc;
using MoonCore.Attributes;
using MoonCore.Extensions;

namespace Azubot.ApiServer.Http.Controllers.Auth
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        [HttpGet("check")]
        [RequirePermission("meta.authenticated")]
        public async Task<CheckResponse> Check()
        {
            var user = HttpContext.User.AsIdentity<User>();

            return new CheckResponse()
            {
                Email = user.Email,
                Username = user.Username
            };
        }
    }
}