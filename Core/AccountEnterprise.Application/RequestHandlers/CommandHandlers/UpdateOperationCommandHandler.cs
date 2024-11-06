using MediatR;
using AutoMapper;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Commands;

namespace AccountEnterprise.Application.RequestHandlers.CommandHandlers;

public class UpdateOperationCommandHandler : IRequestHandler<UpdateOperationCommand, bool>
{
	private readonly IOperationRepository _repository;
	private readonly IMapper _mapper;

	public UpdateOperationCommandHandler(IOperationRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateOperationCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.Operation.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.Operation, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
