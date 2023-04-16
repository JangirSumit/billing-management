using BillingManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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

    public void Post([FromBody] UserDto userDetail)
    {
        //create claims details based on the user information
        var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        //new Claim("UserId", user.UserId.ToString()),
                        //new Claim("DisplayName", user.DisplayName),
                        //new Claim("UserName", user.UserName),
                        //new Claim("Email", user.Email)
                    };
    }
}
