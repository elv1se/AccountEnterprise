using MediatR;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application.Requests.Queries;

public record GetCategoriesQuery : IRequest<IEnumerable<CategoryDto>>;
