using MediatR;
using AutoMapper;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Queries;

namespace AccountEnterprise.Application.RequestHandlers.QueryHandlers;

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto?>
{
	private readonly IEmployeeRepository _repository;
	private readonly IMapper _mapper;

	public GetEmployeeByIdQueryHandler(IEmployeeRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<EmployeeDto?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<EmployeeDto>(await _repository.GetById(request.Id, trackChanges: false));
}
