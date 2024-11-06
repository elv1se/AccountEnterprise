using MediatR;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application.Requests.Commands;

public record CreateDepartmentCommand(DepartmentForCreationDto Department) : IRequest;
