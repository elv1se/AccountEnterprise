using MediatR;

namespace AccountEnterprise.Application.Requests.Commands;

public record DeleteCategoryCommand(Guid Id) : IRequest<bool>;
