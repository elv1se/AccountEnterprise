using MediatR;
using AutoMapper;
using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Commands;

namespace AccountEnterprise.Application.RequestHandlers.CommandHandlers;

public class CreateOperationTypeCommandHandler : IRequestHandler<CreateOperationTypeCommand>
{
	private readonly IOperationTypeRepository _repository;
	private readonly IMapper _mapper;

	public CreateOperationTypeCommandHandler(IOperationTypeRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateOperationTypeCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<OperationType>(request.OperationType));
		await _repository.SaveChanges();
	}
}
