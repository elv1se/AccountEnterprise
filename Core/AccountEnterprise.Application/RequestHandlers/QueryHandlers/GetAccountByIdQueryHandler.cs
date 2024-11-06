using MediatR;
using AutoMapper;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Queries;

namespace AccountEnterprise.Application.RequestHandlers.QueryHandlers;

public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDto?>
{
	private readonly IAccountRepository _repository;
	private readonly IMapper _mapper;

	public GetAccountByIdQueryHandler(IAccountRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<AccountDto?> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<AccountDto>(await _repository.GetById(request.Id, trackChanges: false));
}
