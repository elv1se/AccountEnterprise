using MediatR;
using AutoMapper;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Queries;

namespace AccountEnterprise.Application.RequestHandlers.QueryHandlers;

public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDto?>
{
	private readonly ITransactionRepository _repository;
	private readonly IMapper _mapper;

	public GetTransactionByIdQueryHandler(ITransactionRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<TransactionDto?> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<TransactionDto>(await _repository.GetById(request.Id, trackChanges: false));
}
