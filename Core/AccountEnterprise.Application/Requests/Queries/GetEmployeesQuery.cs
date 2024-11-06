using MediatR;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application.Requests.Queries;

public record GetEmployeesQuery : IRequest<IEnumerable<EmployeeDto>>;
