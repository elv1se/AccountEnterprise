using MediatR;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application.Requests.Commands;

public record UpdateCategoryCommand(CategoryForUpdateDto Category) : IRequest<bool>;
