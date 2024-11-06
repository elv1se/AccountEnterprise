using MediatR;
using AutoMapper;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Queries;

namespace AccountEnterprise.Application.RequestHandlers.QueryHandlers;

public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, IEnumerable<TransactionDto>>
{
	private readonly ITransactionRepository _repository;
	private readonly IMapper _mapper;

	public GetTransactionsQueryHandler(ITransactionRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<TransactionDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<TransactionDto>>(await _repository.Get(trackChanges: false));
}
