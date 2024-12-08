using MediatR;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Domain.RequestFeatures;

namespace AccountEnterprise.Application.Requests.Queries;

public record GetDepartmentsQuery(DepartmentParameters DepartmentParameters) : IRequest<PagedList<DepartmentDto>>;