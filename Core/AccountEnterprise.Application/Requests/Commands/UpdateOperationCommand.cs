using MediatR;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application.Requests.Commands;

public record UpdateOperationCommand(OperationForUpdateDto Operation) : IRequest<bool>;
