using MediatR;
using AutoMapper;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.Domain.RequestFeatures;

namespace AccountEnterprise.Application.RequestHandlers.QueryHandlers;

public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, PagedList<EmployeeDto>>
{
	private readonly IEmployeeRepository _repository;
	private readonly IMapper _mapper;

	public GetEmployeesQueryHandler(IEmployeeRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<PagedList<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
	{
        var employeesWithMetaData = await _repository.Get(request.EmployeeParameters, trackChanges: false);

        var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employeesWithMetaData);

        var employeesDtoWithMetaData = new PagedList<EmployeeDto>(
            employeeDtos.ToList(),
            employeesWithMetaData.MetaData.TotalCount,
            request.EmployeeParameters.PageNumber,
            request.EmployeeParameters.PageSize
        );

        return employeesDtoWithMetaData;
    }
}
