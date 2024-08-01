using awebapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace awebapi.Controllers
{
     [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UsersService _userService;
        public UsersController(UsersService userService)
        {
            _userService = userService;
        }



        
        [HttpGet]
        [Route("GetUsers")]

        public async Task<IActionResult> GetUsers()
        {
            var response = await _userService.FetchAllAsync();
            return Ok(response);
        }
    }
}