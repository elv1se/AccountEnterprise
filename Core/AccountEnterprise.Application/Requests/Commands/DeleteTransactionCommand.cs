using MediatR;

namespace AccountEnterprise.Application.Requests.Commands;

public record DeleteTransactionCommand(Guid Id) : IRequest<bool>;
