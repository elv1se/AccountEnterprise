using MediatR;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Domain.RequestFeatures;

namespace AccountEnterprise.Application.Requests.Queries;

public record GetEmployeesQuery(EmployeeParameters EmployeeParameters) : IRequest<PagedList<EmployeeDto>>;
