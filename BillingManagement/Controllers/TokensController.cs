using Azure.Core;
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
[AllowAnonymous]
public class TokensController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public TokensController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    public IActionResult Post([FromBody] UserDto userDetail)
    {
        var expiry = DateTime.UtcNow.AddMinutes(20);

        var claims = new[] {
                        new Claim(ClaimTypes.Name, userDetail.UserName),
                        new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
                    };
        //create claims details based on the user information
        JwtSecurityToken token = GenerateToken(claims, expiry);
        return ReturnToken(expiry, token);
    }

    [HttpGet]
    [Authorize]
    [Route("refresh")]
    public IActionResult Get()
    {
        var currentAccessToken = Request.Headers["Authorization"];
        if (string.IsNullOrEmpty(currentAccessToken))
        {
            return BadRequest("Invalid access token");
        }

        var accessToken = currentAccessToken.ToString().Replace("Bearer ", "");

        var principal = GetPrincipalFromExpiredToken(accessToken);
        if (principal == null)
        {
            return BadRequest("Invalid access token");
        }

        var expiry = DateTime.UtcNow.AddMinutes(20);
        var token = GenerateToken(principal.Claims.ToArray(), expiry);

        return ReturnToken(expiry, token);
    }

    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }

    private IActionResult ReturnToken(DateTime expiry, JwtSecurityToken token)
    {
        return Ok(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(token),
            expiry
        });
    }

    private JwtSecurityToken GenerateToken(Claim[] claims, DateTime expiry)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: expiry,
            signingCredentials: signIn);
        return token;
    }
}
