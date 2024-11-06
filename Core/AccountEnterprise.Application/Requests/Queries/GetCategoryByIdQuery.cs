using MediatR;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application.Requests.Queries;

public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto?>;
