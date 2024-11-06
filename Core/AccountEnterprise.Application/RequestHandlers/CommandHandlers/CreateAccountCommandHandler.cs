using MediatR;
using AutoMapper;
using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Commands;

namespace AccountEnterprise.Application.RequestHandlers.CommandHandlers;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand>
{
	private readonly IAccountRepository _repository;
	private readonly IMapper _mapper;

	public CreateAccountCommandHandler(IAccountRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateAccountCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<Account>(request.Account));
		await _repository.SaveChanges();
	}
}
