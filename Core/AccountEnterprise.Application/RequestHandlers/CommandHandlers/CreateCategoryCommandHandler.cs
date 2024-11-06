using MediatR;
using AutoMapper;
using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Commands;

namespace AccountEnterprise.Application.RequestHandlers.CommandHandlers;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
{
	private readonly ICategoryRepository _repository;
	private readonly IMapper _mapper;

	public CreateCategoryCommandHandler(ICategoryRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<Category>(request.Category));
		await _repository.SaveChanges();
	}
}
