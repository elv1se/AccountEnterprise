using MediatR;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application.Requests.Queries;

public record GetTransactionsQuery : IRequest<IEnumerable<TransactionDto>>;
