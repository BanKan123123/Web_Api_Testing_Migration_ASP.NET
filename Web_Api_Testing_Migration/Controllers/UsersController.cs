using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web_Api_Testing_Migration.Dto;
using Web_Api_Testing_Migration.Models;
using Web_Api_Testing_Migration.Repositories.Interfaces;

namespace Web_Api_Testing_Migration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UsersController(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }


        [HttpPost("login")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult Login([FromBody] UserLoginDto user)
        {
            if (user == null) return BadRequest(ModelState);

            var userCheck = _usersRepository.isExist(user.user_name);
            if (!userCheck)
            {
                ModelState.AddModelError("Message", "Account was not exists");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var authorLogin = _mapper.Map<users>(user);

            if (!_usersRepository.signInUser(authorLogin.user_name, authorLogin.password))
            {
                ModelState.AddModelError("Message", "Login fail");
                return StatusCode(500, ModelState);
            }
            return Ok("Login Success Fully");
        }

        [HttpPost("register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult Register([FromBody] UserDto user)
        {
            if (user == null) return BadRequest(ModelState);

            var userCheck = _usersRepository.isExist(user.user_name);
            if (userCheck)
            {
                ModelState.AddModelError("Message", "Account was already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userCreated = _mapper.Map<users>(user);

            if (!_usersRepository.registerUser(userCreated))
            {
                ModelState.AddModelError("Message", "Something went wrong while register account process");
                return StatusCode(500, ModelState);
            }

            return Ok("Message: Register account successfully");
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult UpdaetUser(string username, [FromBody] UserDto user)
        {
            if (user == null) return BadRequest(ModelState);

            if (username != user.user_name) return BadRequest(ModelState);

            if (!_usersRepository.isExist(username)) return NotFound();

            if (!ModelState.IsValid) return BadRequest();

            var userMap = _mapper.Map<users>(user);

            if  (!_usersRepository.updateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong While updating user");
                return StatusCode(500, ModelState);
            }
            return Ok("Message: Updating user successfully");

        }
    }
}
