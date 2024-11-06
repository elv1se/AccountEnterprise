using MediatR;
using AutoMapper;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Commands;

namespace AccountEnterprise.Application.RequestHandlers.CommandHandlers;

public class UpdateOperationTypeCommandHandler : IRequestHandler<UpdateOperationTypeCommand, bool>
{
	private readonly IOperationTypeRepository _repository;
	private readonly IMapper _mapper;

	public UpdateOperationTypeCommandHandler(IOperationTypeRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateOperationTypeCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.OperationType.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.OperationType, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
