using MediatR;
using AutoMapper;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.Domain.RequestFeatures;

namespace AccountEnterprise.Application.RequestHandlers.QueryHandlers;

public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, PagedList<TransactionDto>>
{
	private readonly ITransactionRepository _repository;
	private readonly IMapper _mapper;

	public GetTransactionsQueryHandler(ITransactionRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<PagedList<TransactionDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
	{
        var transactionsWithMetaData = await _repository.Get(request.TransactionParameters, trackChanges: false);

        var transactionDtos = _mapper.Map<IEnumerable<TransactionDto>>(transactionsWithMetaData);

        var transactionsDtoWithMetaData = new PagedList<TransactionDto>(
            transactionDtos.ToList(),
            transactionsWithMetaData.MetaData.TotalCount,
            request.TransactionParameters.PageNumber,
            request.TransactionParameters.PageSize
        );

        return transactionsDtoWithMetaData;
    }
}
