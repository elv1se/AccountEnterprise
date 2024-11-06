using MediatR;
using AutoMapper;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Queries;

namespace AccountEnterprise.Application.RequestHandlers.QueryHandlers;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto?>
{
	private readonly ICategoryRepository _repository;
	private readonly IMapper _mapper;

	public GetCategoryByIdQueryHandler(ICategoryRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<CategoryDto?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<CategoryDto>(await _repository.GetById(request.Id, trackChanges: false));
}
