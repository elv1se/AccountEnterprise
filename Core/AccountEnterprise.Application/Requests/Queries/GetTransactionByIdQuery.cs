using MediatR;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application.Requests.Queries;

public record GetTransactionByIdQuery(Guid Id) : IRequest<TransactionDto?>;
