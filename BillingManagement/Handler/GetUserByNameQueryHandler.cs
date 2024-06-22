using BillingManagement.Contracts.Abstrations;
using BillingManagement.Contracts.Models;
using BillingManagement.Query;
using MediatR;

namespace BillingManagement.Handler;

public class GetUserByNameQueryHandler : IRequestHandler<GetUserByNameQuery, UserDetail>
{
    private readonly IUsersRepository _usersRepository;

    public GetUserByNameQueryHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<UserDetail> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
    {
        return await _usersRepository.GetUserByName(request.UserName);
    }
}
