using MediatR;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application.Requests.Queries;

public record GetOperationTypeByIdQuery(Guid Id) : IRequest<OperationTypeDto?>;
