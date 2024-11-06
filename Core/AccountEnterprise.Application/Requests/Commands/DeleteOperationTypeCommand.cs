using MediatR;

namespace AccountEnterprise.Application.Requests.Commands;

public record DeleteOperationTypeCommand(Guid Id) : IRequest<bool>;
