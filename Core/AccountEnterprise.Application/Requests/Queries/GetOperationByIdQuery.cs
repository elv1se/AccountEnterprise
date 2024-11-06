using MediatR;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application.Requests.Queries;

public record GetOperationByIdQuery(Guid Id) : IRequest<OperationDto?>;
