using MediatR;
using AutoMapper;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.Domain.RequestFeatures;

namespace AccountEnterprise.Application.RequestHandlers.QueryHandlers;

public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, PagedList<DepartmentDto>>
{
	private readonly IDepartmentRepository _repository;
	private readonly IMapper _mapper;

	public GetDepartmentsQueryHandler(IDepartmentRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<PagedList<DepartmentDto>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
	{
        var departmentsWithMetaData = await _repository.Get(request.DepartmentParameters, trackChanges: false);

        var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departmentsWithMetaData);

        var departmentsDtoWithMetaData = new PagedList<DepartmentDto>(
            departmentDtos.ToList(),
            departmentsWithMetaData.MetaData.TotalCount,
            request.DepartmentParameters.PageNumber,
            request.DepartmentParameters.PageSize
        );

        return departmentsDtoWithMetaData;
    }
}
