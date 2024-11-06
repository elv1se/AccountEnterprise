using MediatR;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application.Requests.Queries;

public record GetDepartmentsQuery : IRequest<IEnumerable<DepartmentDto>>;
