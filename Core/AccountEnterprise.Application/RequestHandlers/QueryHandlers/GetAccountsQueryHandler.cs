using MediatR;
using AutoMapper;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Queries;

namespace AccountEnterprise.Application.RequestHandlers.QueryHandlers;

public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, IEnumerable<AccountDto>>
{
	private readonly IAccountRepository _repository;
	private readonly IMapper _mapper;

	public GetAccountsQueryHandler(IAccountRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<AccountDto>> Handle(GetAccountsQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<AccountDto>>(await _repository.Get(trackChanges: false));
}
