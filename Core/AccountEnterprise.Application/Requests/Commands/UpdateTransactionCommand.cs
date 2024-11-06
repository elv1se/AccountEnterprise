using MediatR;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application.Requests.Commands;

public record UpdateTransactionCommand(TransactionForUpdateDto Transaction) : IRequest<bool>;
