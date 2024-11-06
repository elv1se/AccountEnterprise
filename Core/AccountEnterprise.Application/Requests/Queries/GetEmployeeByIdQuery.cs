using MediatR;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application.Requests.Queries;

public record GetEmployeeByIdQuery(Guid Id) : IRequest<EmployeeDto?>;
