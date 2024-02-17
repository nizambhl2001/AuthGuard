using AuthGuad.Data;
using AuthGuad.Helper;
using AuthGuad.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace AuthGuad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicaitonDbContext dbContext;

        public UserController(ApplicaitonDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost("UserLogin")]
        public async Task<IActionResult> Login(User obj)
        {
            if (obj == null) BadRequest();
            var user = await dbContext.users.FirstOrDefaultAsync(x => x.Username == obj.Username);

            if (user == null) return NotFound(new { Message = "user Not found" });

            if (!HashPasswrod.VeryfyPassWord(obj.Password, user.Password))
            {
                return Ok(new { Message = "password is incorrect" });
            }
            user.Token = CreateJwt(user);
            return Ok(new
            {
                Token = user.Token,
                Message = "Login Success"
            });
        }
        [HttpPost("UserRegister")]
        public async Task<IActionResult> Register([FromBody] User obj)
        {
            if (obj == null) return NotFound();
            //chick User
            if (await ChickUserNameExitAync(obj.Username))
                return BadRequest(new { Message = "User Name Already Exit!" });

            //chick Email
            if (await ChickEmailExitAync(obj.Email))
                return BadRequest(new { Message = "Email Name Already Exit!" });

            //chick password Stength
            var pass = ChickPasswordStength(obj.Password);
            if (!string.IsNullOrEmpty(pass))
                return BadRequest(new { Message = pass.ToString() });

            obj.Password = HashPasswrod.PasswrodHash(obj.Password);
            obj.Role = "User";
            obj.Token = "";
            await dbContext.users.AddAsync(obj);
            await dbContext.SaveChangesAsync();
            return Ok(new { Message = "User Register" });
        }
        [HttpGet]
        public async Task<ActionResult<User>> GetAllUser()
        {
            return Ok(await dbContext.users.ToListAsync());
        }


        private Task<bool> ChickUserNameExitAync(string username)
        => dbContext.users.AnyAsync(x => x.Username == username);
        private Task<bool> ChickEmailExitAync(string email)
        => dbContext.users.AnyAsync(x => x.Email == email);

        private string ChickPasswordStength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if(password.Length <8)
                sb.Append("Minimum password length should be 8"+Environment.NewLine);
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]")
                && Regex.IsMatch(password, "[0-9]")))
                sb.Append("password should be Alhpanumeric" + Environment.NewLine);
            if(!(Regex.IsMatch(password, "[!, @, #, $, %, ^, &, *, (, ), ?, <, >, {, }, [,\\, ~, !, \", ?, +, _, =, :, /, ;]")))
                sb.Append("password should contain spacil charecter" + Environment.NewLine);
            return sb.ToString();   
        }
        private string CreateJwt(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryveryscrite....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(ClaimTypes.Name,$"{user.FirstName}{user.LastName}")
            });
            var credentails = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentails,
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);


        }

    }
}
