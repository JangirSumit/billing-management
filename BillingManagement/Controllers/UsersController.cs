using BillingManagement.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BillingManagement.Dto;
using BillingManagement.Contracts.Abstrations;
using BillingManagement.Contracts.Enums;
using BillingManagement.Contracts.Models;

namespace BillingManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class UsersController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUsersRepository _usersRepository;

    public UsersController(IConfiguration configuration, IUsersRepository usersRepository)
    {
        _configuration = configuration;
        _usersRepository = usersRepository;
    }

    [HttpPost]
    [Route("register")]
    public IActionResult PostAddUser([FromBody] UserDto userDto)
    {
        try
        {
            var user = _usersRepository.GetUserByName(userDto.UserName);

            if (user.IsEmpty == false)
            {
                return BadRequest(new
                {
                    Message = "User already exists.",
                    FailureReason = FailureReason.UserAlreadyExists.ToString()
                });
            }

            var passwordEncrypted = CryptoHelper.EncryptPassword(userDto.Password);

            if (_usersRepository.Add(new UserDetail(userDto.UserName, passwordEncrypted)) > 0)
            {
                return Ok();
            }

            return BadRequest(new
            {
                Message = "Failed to create User.",
                FailureReason = FailureReason.UserCreationFailed.ToString()
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    [Route("changePassword")]
    public IActionResult PostChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        try
        {
            var user = _usersRepository.GetUserByName(changePasswordDto.UserName);

            if (user.IsEmpty)
            {
                return BadRequest(new
                {
                    Message = "User does not exists.",
                    FailureReason = FailureReason.UserAlreadyExists.ToString()
                });
            }

            if (CryptoHelper.DecryptPassword(changePasswordDto.CurrentPassword, user.Password) == false)
            {
                return BadRequest(new
                {
                    Message = "Current password does not match.",
                    FailureReason = FailureReason.IncorrectCurrentPassword.ToString()
                });
            }

            var password = CryptoHelper.EncryptPassword(changePasswordDto.NewPassword);

            if (_usersRepository.ChangePassword(changePasswordDto.UserName, password) > 0)
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong.");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
