using BillingManagement.Contracts.Abstrations;
using BillingManagement.Contracts.Enums;
using BillingManagement.Contracts.Models;
using BillingManagement.Dto;
using BillingManagement.Helpers;
using BillingManagement.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillingManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class UsersController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUsersRepository _usersRepository;
    private readonly IMediator _mediator;

    public UsersController(IConfiguration configuration, IUsersRepository usersRepository, IMediator mediator)
    {
        _configuration = configuration;
        _usersRepository = usersRepository;
        _mediator = mediator;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> PostAddUser([FromBody] GetUserByNameQuery getUserByNameQuery)
    {
        try
        {
            var user = await _mediator.Send(getUserByNameQuery);

            if (user.IsEmpty == false)
            {
                return BadRequest(new
                {
                    Message = "User already exists.",
                    FailureReason = FailureReason.UserAlreadyExists.ToString()
                });
            }

            var passwordEncrypted = CryptoHelper.EncryptPassword(getUserByNameQuery.Password);

            if (await _usersRepository.Add(new UserDetail(getUserByNameQuery.UserName, passwordEncrypted)) > 0)
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
    public async Task<IActionResult> PostChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        try
        {
            var user = await _usersRepository.GetUserByName(changePasswordDto.UserName);

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

            if (await _usersRepository.ChangePassword(changePasswordDto.UserName, password) > 0)
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
