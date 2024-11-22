using MediatR;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Domain.RequestFeatures;

namespace AccountEnterprise.Application.Requests.Queries;

public record GetCategoriesQuery(CategoryParameters CategoryParameters) : IRequest<PagedList<CategoryDto>>;
