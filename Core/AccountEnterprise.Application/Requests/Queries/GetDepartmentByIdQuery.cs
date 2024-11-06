using MediatR;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application.Requests.Queries;

public record GetDepartmentByIdQuery(Guid Id) : IRequest<DepartmentDto?>;
