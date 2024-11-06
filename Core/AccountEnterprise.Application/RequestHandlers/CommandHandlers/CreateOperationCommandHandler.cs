using MediatR;
using AutoMapper;
using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Commands;

namespace AccountEnterprise.Application.RequestHandlers.CommandHandlers;

public class CreateOperationCommandHandler : IRequestHandler<CreateOperationCommand>
{
	private readonly IOperationRepository _repository;
	private readonly IMapper _mapper;

	public CreateOperationCommandHandler(IOperationRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateOperationCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<Operation>(request.Operation));
		await _repository.SaveChanges();
	}
}
