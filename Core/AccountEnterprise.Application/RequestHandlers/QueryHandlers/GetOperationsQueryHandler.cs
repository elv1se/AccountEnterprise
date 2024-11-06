using MediatR;
using AutoMapper;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Queries;

namespace AccountEnterprise.Application.RequestHandlers.QueryHandlers;

public class GetOperationsQueryHandler : IRequestHandler<GetOperationsQuery, IEnumerable<OperationDto>>
{
	private readonly IOperationRepository _repository;
	private readonly IMapper _mapper;

	public GetOperationsQueryHandler(IOperationRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<OperationDto>> Handle(GetOperationsQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<IEnumerable<OperationDto>>(await _repository.Get(trackChanges: false));
}
