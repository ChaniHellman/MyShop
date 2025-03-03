using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Entities;
using Services;
using DTO;
using AutoMapper;
using DTO.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OurShop.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userService;
        IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserByIdDto>> Get(int id)
        {
            User checkUser = await _userService.getUserById(id);
            if (checkUser != null)
                return Ok(_mapper.Map<User,UserByIdDto>(checkUser));
            else
                return NotFound();
        }

        // POST api/<UsersController>
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
        [HttpPost("login")]
        public async Task<ActionResult<returnLoginUserDto>> Login([FromBody] LoginDto loginDto)
        {
            
            string email = loginDto.email;
            string password = loginDto.password;

            User checkUser = await _userService.loginUser(email, password);
            if (checkUser != null)
                return Ok(_mapper.Map<User, returnLoginUserDto>(checkUser));
            else
               return NotFound();


        }
        [HttpPost("passwordStrength")]
        public ActionResult<int> checkPasswordStrength([FromQuery] string password)

        {
            int passwordStrength = _userService.checkPasswordStrength(password);
            return passwordStrength;


        }



    }
}
