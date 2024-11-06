using MediatR;

namespace AccountEnterprise.Application.Requests.Commands;

public record DeleteOperationCommand(Guid Id) : IRequest<bool>;
