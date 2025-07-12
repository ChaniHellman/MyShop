using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Entities;
using Services;
using DTO;
using AutoMapper;
using DTO.DTO;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OurShop.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        IUserService _userService;
        IMapper _mapper;
        ILogger<User> _logger;
        IConfiguration _configuration;

        public UsersController(IUserService userService, IMapper mapper, ILogger<User> logger, IConfiguration configuration)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserByIdDto>> Get(int id)
        {
            User checkUser = await _userService.getUserById(id);
            if (checkUser != null)
                return Ok(_mapper.Map<User, UserByIdDto>(checkUser));
            else
                return NotFound();
        }

        // POST api/<UsersController>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] addUserDto user)
        {
            try
            {

                User ParsedUser = _mapper.Map<addUserDto, User>(user);
                User returnedUser = await _userService.AddUser(ParsedUser);
                if (returnedUser == null)
                {
                    return Conflict();
                }
                return CreatedAtAction(nameof(Get), new { id = ParsedUser.UserId }, _mapper.Map<addUserDto, returnPostUserDto>(user));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] addUserDto userToUpdate)
        {
            try
            {
                User user = _mapper.Map<addUserDto, User>(userToUpdate);
                await _userService.updateUser(id, user);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
            return Ok();

        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<returnLoginUserDto>> Login([FromBody] LoginDto loginDto)
        {
            string email = loginDto.email;
            string password = loginDto.password;

            User checkUser = await _userService.loginUser(email, password);
            if (checkUser != null)
            {
                _logger.LogInformation("User {UserId} logged in!", checkUser.UserId);
                // Generate JWT token using the service
                var token = _userService.GenerateJwtToken(checkUser);
                var userDto = _mapper.Map<User, returnLoginUserDto>(checkUser);
                // Set JWT as HTTP-only cookie
                Response.Cookies.Append("jwt", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.Now.AddMinutes(Convert.ToDouble((_configuration)["Jwt:ExpiresInMinutes"]))
                });
                return Ok(userDto);
            }
            else
                return NotFound();
        }

        [AllowAnonymous]
        [HttpPost("passwordStrength")]
        public ActionResult<int> checkPasswordStrength([FromQuery] string password)

        {
            int passwordStrength = _userService.checkPasswordStrength(password);
            return passwordStrength;


        }



    }
}
