using MediatR;
using AutoMapper;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Queries;

namespace AccountEnterprise.Application.RequestHandlers.QueryHandlers;

public class GetOperationTypeByIdQueryHandler : IRequestHandler<GetOperationTypeByIdQuery, OperationTypeDto?>
{
	private readonly IOperationTypeRepository _repository;
	private readonly IMapper _mapper;

	public GetOperationTypeByIdQueryHandler(IOperationTypeRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<OperationTypeDto?> Handle(GetOperationTypeByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<OperationTypeDto>(await _repository.GetById(request.Id, trackChanges: false));
}
