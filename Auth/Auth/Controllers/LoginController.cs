using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SistemaGestionePratiche.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LoginController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _configuration;

        public LoginController(IConfiguration config)
        {
            _configuration = config;
        }


        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if(loginRequest.Username != "admin" || loginRequest.Password != "aruba")
            {
                return NotFound("Dati di login errati");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddDays(7),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return Ok(token);
        }
    }
}
