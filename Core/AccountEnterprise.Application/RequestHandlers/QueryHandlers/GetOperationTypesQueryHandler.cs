using MediatR;
using AutoMapper;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Queries;

namespace AccountEnterprise.Application.RequestHandlers.QueryHandlers;

public class GetOperationTypesQueryHandler : IRequestHandler<GetOperationTypesQuery, IEnumerable<OperationTypeDto>>
{
	private readonly IOperationTypeRepository _repository;
	private readonly IMapper _mapper;

	public GetOperationTypesQueryHandler(IOperationTypeRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<OperationTypeDto>> Handle(GetOperationTypesQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<OperationTypeDto>>(await _repository.Get(trackChanges: false));
}
