using BillingManagement.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BillingManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokensController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public TokensController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Post([FromBody] UserDto userDetail)
    {
        //create claims details based on the user information
        var claims = new[] {
                        new Claim(ClaimTypes.Name, userDetail.UserName),
                        new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
                    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(20),
            signingCredentials: signIn);

        return Ok(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(token),
            expiry = 20
        });
    }
}
