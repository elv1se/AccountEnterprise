using MediatR;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application.Requests.Commands;

public record UpdateEmployeeCommand(EmployeeForUpdateDto Employee) : IRequest<bool>;
