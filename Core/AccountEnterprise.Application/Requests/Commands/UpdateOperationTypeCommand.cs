using MediatR;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application.Requests.Commands;

public record UpdateOperationTypeCommand(OperationTypeForUpdateDto OperationType) : IRequest<bool>;
