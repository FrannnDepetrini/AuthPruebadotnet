using Application.Interfaces;
using Application.Models.Requests;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAutenticationUser _autenticationUser;
        //private readonly IConfiguration _configuration;
        
        public UserController(IUserService userService, IAutenticationUser autenticationUser, IConfiguration configuration)
        {
            _userService = userService;
            _autenticationUser = autenticationUser;
            //_configuration = configuration;
        }

        [HttpPost("[action]")]
        public IActionResult Register(UserRegisterRequest user)
        {
            string passwordHashed = _autenticationUser.encodeSHA256(user.Password);
            return Ok(_userService.Add(new User { Name = user.Name, Email = user.Email, Password = passwordHashed, Rol = user.Rol }));
        }

        [HttpPost("[action]")]
        public IActionResult LogIn(UserLoginRequest user)
        {
            User? userFound = _autenticationUser.ValidateUser(user);
            if (userFound == null)
            {
                return StatusCode(404, new { token = ""});
            } else
            {
                return Ok(new { token = _autenticationUser.Autenticar(userFound)});
            }
        }

     

        [HttpDelete("[action]")]
        public IActionResult Delete(int id)
        {
            return Ok(_userService.Delete(id));
        }

        [HttpGet("[action]")]
        public IActionResult Get()
        {
            return Ok(_userService.Get());
        }

        [HttpGet("[action]")]
        public IActionResult GetById(int id)
        {
            return Ok(_userService.GetById(id));
        }

        [HttpPost("[action]")]
        public IActionResult hashText(string text) {
            string textHashed = _autenticationUser.encodeSHA256(text);
            return Ok(textHashed);
        }

        //[HttpPost("[action]")]
        //public IActionResult validate(UserLoginRequest userRequest)
        //{
        //    User? userValidate = _autenticationUser.ValidateUser(userRequest);
        //    return Ok(userValidate);
        //}
    }
}

        