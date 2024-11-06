using MediatR;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application.Requests.Queries;

public record GetOperationsQuery : IRequest<IEnumerable<OperationDto>>;
