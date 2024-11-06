using MediatR;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application.Requests.Commands;

public record UpdateAccountCommand(AccountForUpdateDto Account) : IRequest<bool>;
