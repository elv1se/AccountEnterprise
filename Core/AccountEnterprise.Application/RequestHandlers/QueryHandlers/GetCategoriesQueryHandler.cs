using MediatR;
using AutoMapper;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.Domain.RequestFeatures;

namespace AccountEnterprise.Application.RequestHandlers.QueryHandlers;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, PagedList<CategoryDto>>
{
	private readonly ICategoryRepository _repository;
	private readonly IMapper _mapper;

	public GetCategoriesQueryHandler(ICategoryRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<PagedList<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
	{
        var categorysWithMetaData = await _repository.Get(request.CategoryParameters, trackChanges: false);

        var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categorysWithMetaData);

        var categorysDtoWithMetaData = new PagedList<CategoryDto>(
            categoryDtos.ToList(),
            categorysWithMetaData.MetaData.TotalCount,
            request.CategoryParameters.PageNumber,
            request.CategoryParameters.PageSize
        );

        return categorysDtoWithMetaData;
    }
}
