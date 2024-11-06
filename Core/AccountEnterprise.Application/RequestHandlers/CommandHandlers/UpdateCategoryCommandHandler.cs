using MediatR;
using AutoMapper;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Commands;

namespace AccountEnterprise.Application.RequestHandlers.CommandHandlers;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
{
	private readonly ICategoryRepository _repository;
	private readonly IMapper _mapper;

	public UpdateCategoryCommandHandler(ICategoryRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.Category.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.Category, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
