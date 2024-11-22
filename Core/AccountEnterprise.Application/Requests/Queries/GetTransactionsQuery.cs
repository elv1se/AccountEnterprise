using MediatR;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Domain.RequestFeatures;

namespace AccountEnterprise.Application.Requests.Queries;

public record GetTransactionsQuery(TransactionParameters TransactionParameters) : IRequest<PagedList<TransactionDto>>;
