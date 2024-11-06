using MediatR;
using AutoMapper;
using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Commands;

namespace AccountEnterprise.Application.RequestHandlers.CommandHandlers;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand>
{
	private readonly ITransactionRepository _repository;
	private readonly IMapper _mapper;

	public CreateTransactionCommandHandler(ITransactionRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<Transaction>(request.Transaction));
		await _repository.SaveChanges();
	}
}
