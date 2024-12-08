using MediatR;
using AutoMapper;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.Domain.RequestFeatures;

namespace AccountEnterprise.Application.RequestHandlers.QueryHandlers;

public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, PagedList<AccountDto>>
{
	private readonly IAccountRepository _repository;
	private readonly IMapper _mapper;

	public GetAccountsQueryHandler(IAccountRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<PagedList<AccountDto>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
	{

        var accountsWithMetaData = await _repository.Get(request.AccountParameters, trackChanges: false);

        var accountDtos = _mapper.Map<IEnumerable<AccountDto>>(accountsWithMetaData);

        var accountsDtoWithMetaData = new PagedList<AccountDto>(
            accountDtos.ToList(),
            accountsWithMetaData.MetaData.TotalCount,
            request.AccountParameters.PageNumber,
            request.AccountParameters.PageSize
        );

        return accountsDtoWithMetaData;

    }
}
