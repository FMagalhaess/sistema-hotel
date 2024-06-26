using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using TrybeHotel.Dto;
using TrybeHotel.Services;
using Microsoft.AspNetCore.Authorization;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("login")]

    public class LoginController : Controller
    {

        private readonly IUserRepository _repository;
        public LoginController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginDto login){
            UserDto? user = _repository.Login(login);
            if (user == null)
            {
                return Unauthorized(new { message = "Incorrect e-mail or password" });
            }
            return Ok(new { token = new TokenGenerator().Generate(user) }); 
        }
    }
}