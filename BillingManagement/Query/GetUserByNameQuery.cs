using BillingManagement.Contracts.Models;
using MediatR;

namespace BillingManagement.Query;

public record GetUserByNameQuery(string UserName, string Password) : IRequest<UserDetail>;
