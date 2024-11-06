using MediatR;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application.Requests.Commands;

public record UpdateDepartmentCommand(DepartmentForUpdateDto Department) : IRequest<bool>;
