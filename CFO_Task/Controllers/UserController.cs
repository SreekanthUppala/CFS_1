using CFO_Task.Models;
using CFO_Task.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CFO_Task.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;

        public UserController(AppDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }


        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var usersResponse = await _userService.GetAllUsers();
            if(!usersResponse.Succeeded)
            {
                return new OkObjectResult(usersResponse);
            }

            return new OkObjectResult(usersResponse);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var userResponse = await _userService.GetUser(id);
                if (!userResponse.Succeeded)
                {
                    return new BadRequestObjectResult(userResponse);
                }
                else if(userResponse.Data== null)
                {
                    return NotFound(new
                    {
                        message = "User not found"
                    });
                }
                else
                {
                    return new OkObjectResult(userResponse);
                }

            }
            catch(Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while processing the request.",
                    error = ex.Message
                });
            }
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var response = await _userService.AddUser(user);
            if (!response.Succeeded)
            {
                return BadRequest(response.Error);
            }

            return new OkObjectResult(response);
        }
        
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id,User user)
        {
            if (id != user.Id)
            {
                return BadRequest(new
                {
                    message = "ID mismatch"
                });
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var existingUserResponse = await _userService.GetUser(id);

            if (!existingUserResponse.Succeeded || existingUserResponse.Data == null)
            {
                return new BadRequestObjectResult(existingUserResponse);
            }

            var existingUser = await _userService.UpdateUser(id, user);

            if(existingUser.Succeeded) 
            {
                return new OkObjectResult(existingUser);
            }
            else 
            {
                return new BadRequestObjectResult(existingUser);
            }
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUser(id);
            if(!user.Succeeded)
            {
                return new BadRequestObjectResult(user);
            }
            else if(user.Data == null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "User not found"
                }); 
            }
            else
            {
                var deleteResponse = await _userService.DeleteUser(user.Data);
                if(!deleteResponse.Succeeded)
                {
                    return new BadRequestObjectResult(deleteResponse.Error);
                }
                else
                {
                    return new OkObjectResult(deleteResponse);
                }
            }
        }
    }
}
