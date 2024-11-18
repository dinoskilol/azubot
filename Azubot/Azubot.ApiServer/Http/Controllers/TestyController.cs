using Azubot.ApiServer.Database.Entities;
using Azubot.Shared.Http.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoonCore.Exceptions;
using MoonCore.Extended.Abstractions;

namespace Azubot.ApiServer.Http.Controllers
{
    [Route("api/testy")]
    [ApiController]
    public class TestyController : Controller
    {
        private readonly DatabaseRepository<User> UserRepository;

        public TestyController(DatabaseRepository<User> userRepository)
        {
            UserRepository = userRepository;
        }

        [HttpGet]
        public async Task<TestyResponse> Get()
        {
            var users = UserRepository
                .Get()
                .ToArray();

            var response = new TestyResponse()
            {
                Usernames = users.Select(x => x.Username).ToArray()
            };

            return response;
        }
    }
}
