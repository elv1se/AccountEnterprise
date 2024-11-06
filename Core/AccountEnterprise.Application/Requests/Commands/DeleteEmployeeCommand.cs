using MediatR;

namespace AccountEnterprise.Application.Requests.Commands;

public record DeleteEmployeeCommand(Guid Id) : IRequest<bool>;
