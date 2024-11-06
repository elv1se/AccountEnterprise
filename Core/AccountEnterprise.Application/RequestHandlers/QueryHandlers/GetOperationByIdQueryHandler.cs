using MediatR;
using AutoMapper;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Queries;

namespace AccountEnterprise.Application.RequestHandlers.QueryHandlers;

public class GetOperationByIdQueryHandler : IRequestHandler<GetOperationByIdQuery, OperationDto?>
{
	private readonly IOperationRepository _repository;
	private readonly IMapper _mapper;

	public GetOperationByIdQueryHandler(IOperationRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<OperationDto?> Handle(GetOperationByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<OperationDto>(await _repository.GetById(request.Id, trackChanges: false));
}
