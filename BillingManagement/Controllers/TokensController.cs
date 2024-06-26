﻿using BillingManagement.Contracts.Abstrations;
using BillingManagement.Contracts.Enums;
using BillingManagement.Dto;
using BillingManagement.Helpers;
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
    private readonly IUsersRepository _usersRepository;

    public TokensController(IConfiguration configuration, IUsersRepository usersRepository)
    {
        _configuration = configuration;
        _usersRepository = usersRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UserDto userDto)
    {
        try
        {
            var user = await _usersRepository.GetUserByName(userDto.UserName);

            if (user.IsEmpty == false && CryptoHelper.DecryptPassword(userDto.Password, user.Password))
            {
                var expiry = DateTime.UtcNow.AddMinutes(20);

                var claims = new[] {
                        new Claim(ClaimTypes.Name, userDto.UserName),
                        new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
                    };
                //create claims details based on the user information
                JwtSecurityToken token = GenerateToken(claims, expiry);
                return ReturnToken(expiry, token);
            }

            return BadRequest(new
            {
                Message = "Invalid Username or Password.",
                FailureReason = FailureReason.InvalidCredentials.ToString()
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("refresh")]
    public IActionResult Get()
    {
        try
        {
            var currentAccessToken = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(currentAccessToken))
            {
                return BadRequest(new
                {
                    Message = "Invalid access token",
                    FailureReason = FailureReason.InvalidAccessToken
                });
            }

            var accessToken = currentAccessToken.ToString().Replace("Bearer ", "");

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid access token",
                    FailureReason = FailureReason.InvalidAccessToken.ToString()
                });
            }

            var expiry = DateTime.UtcNow.AddMinutes(20);
            var token = GenerateToken(principal.Claims.ToArray(), expiry);

            return ReturnToken(expiry, token);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("validate")]
    public IActionResult Validate()
    {
        try
        {
            var currentAccessToken = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(currentAccessToken))
            {
                return BadRequest(new
                {
                    Message = "Invalid access token",
                    FailureReason = FailureReason.InvalidAccessToken.ToString()
                });
            }

            var accessToken = currentAccessToken.ToString().Replace("Bearer ", "");

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid access token",
                    FailureReason = FailureReason.InvalidAccessToken.ToString()
                });
            }

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
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
