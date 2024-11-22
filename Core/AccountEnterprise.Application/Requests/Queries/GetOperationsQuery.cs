using MediatR;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Domain.RequestFeatures;

namespace AccountEnterprise.Application.Requests.Queries;

public record GetOperationsQuery(OperationParameters OperationParameters) : IRequest<PagedList<OperationDto>>;
