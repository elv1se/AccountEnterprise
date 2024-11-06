using MediatR;

namespace AccountEnterprise.Application.Requests.Commands;

public record DeleteAccountCommand(Guid Id) : IRequest<bool>;
