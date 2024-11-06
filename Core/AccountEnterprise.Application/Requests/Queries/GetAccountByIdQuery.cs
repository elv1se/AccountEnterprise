using MediatR;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application.Requests.Queries;

public record GetAccountByIdQuery(Guid Id) : IRequest<AccountDto?>;
