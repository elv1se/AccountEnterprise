using MediatR;
using AutoMapper;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Commands;

namespace AccountEnterprise.Application.RequestHandlers.CommandHandlers;

public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, bool>
{
	private readonly ITransactionRepository _repository;
	private readonly IMapper _mapper;

	public UpdateTransactionCommandHandler(ITransactionRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.Transaction.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.Transaction, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
