using MediatR;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application.Requests.Commands;

public record CreateCategoryCommand(CategoryForCreationDto Category) : IRequest;
