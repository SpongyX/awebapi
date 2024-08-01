using awebapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace awebapi.Controllers
{
    [Route("api/[controller]")]

    public class TestController : Controller
    {

        private readonly TestService _testService;
        public TestController(TestService testService)
        {
            _testService = testService;
        }

        [HttpGet]
        [Route("GetUsers")]

        public async Task<IActionResult> GetUsers()
        {
            var test1 = await _testService.FetchAllAsync();
            return Ok(test1);
        }
    }
}