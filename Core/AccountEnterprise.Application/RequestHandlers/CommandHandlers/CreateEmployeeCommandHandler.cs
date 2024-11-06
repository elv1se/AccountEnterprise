using MediatR;
using AutoMapper;
using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Commands;

namespace AccountEnterprise.Application.RequestHandlers.CommandHandlers;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand>
{
	private readonly IEmployeeRepository _repository;
	private readonly IMapper _mapper;

	public CreateEmployeeCommandHandler(IEmployeeRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<Employee>(request.Employee));
		await _repository.SaveChanges();
	}
}
