using MediatR;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Commands;

namespace AccountEnterprise.Application.RequestHandlers.CommandHandlers;

public class DeleteOperationTypeCommandHandler(IOperationTypeRepository repository) : IRequestHandler<DeleteOperationTypeCommand, bool>
{
	private readonly IOperationTypeRepository _repository = repository;

	public async Task<bool> Handle(DeleteOperationTypeCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.Id, trackChanges: false);

        if (entity is null)
        {
            return false;
        }

        _repository.Delete(entity);
        await _repository.SaveChanges();

        return true;
	}
}
