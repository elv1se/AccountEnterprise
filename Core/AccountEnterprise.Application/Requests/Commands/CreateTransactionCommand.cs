using MediatR;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application.Requests.Commands;

public record CreateTransactionCommand(TransactionForCreationDto Transaction) : IRequest;
