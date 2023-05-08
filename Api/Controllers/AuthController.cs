using Api.Dtos;
using Api.Services;
using Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly AuthService _authService;

        public AuthController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _authService = new(configuration["TokenString"]!);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(User user)
        {
            Data.Entities.User dbUser = await _context.Users.SingleOrDefaultAsync(u => u.Name == user.Name);

            if(dbUser == null)
                return NotFound();

            if (!_authService.VerifyPasswordHash(user.Password, dbUser.PasswordHash, dbUser.PasswordSalt))
                return Forbid("wrong pwd");

            return new JsonResult(new { Token = _authService.CreateToken(dbUser) });
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(User user)
        {
            Data.Entities.User dbUser = await _context.Users.SingleOrDefaultAsync(u => u.Name == user.Name);

            if (dbUser != null)
                return BadRequest("user already exists");

            _authService.CreatePasswordHash(user.Password, out string hash, out string salt);

            await _context.Users.AddAsync(new Data.Entities.User() 
            { 
                Name = user.Name,
                PasswordHash = hash, 
                PasswordSalt = salt
            });

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
