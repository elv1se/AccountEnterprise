using MediatR;

namespace AccountEnterprise.Application.Requests.Commands;

public record DeleteDepartmentCommand(Guid Id) : IRequest<bool>;
