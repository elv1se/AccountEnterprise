using MediatR;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Commands;

namespace AccountEnterprise.Application.RequestHandlers.CommandHandlers;

public class DeleteDepartmentCommandHandler(IDepartmentRepository repository) : IRequestHandler<DeleteDepartmentCommand, bool>
{
	private readonly IDepartmentRepository _repository = repository;

	public async Task<bool> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
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
