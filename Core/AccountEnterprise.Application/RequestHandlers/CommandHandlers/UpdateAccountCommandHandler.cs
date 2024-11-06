using MediatR;
using AutoMapper;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Commands;

namespace AccountEnterprise.Application.RequestHandlers.CommandHandlers;

public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, bool>
{
	private readonly IAccountRepository _repository;
	private readonly IMapper _mapper;

	public UpdateAccountCommandHandler(IAccountRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.Account.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.Account, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
