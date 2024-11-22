using MediatR;
using AutoMapper;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Domain.Abstractions;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.Domain.RequestFeatures;

namespace AccountEnterprise.Application.RequestHandlers.QueryHandlers;

public class GetOperationsQueryHandler(IOperationRepository repository, IMapper mapper) : IRequestHandler<GetOperationsQuery, PagedList<OperationDto>>
{
	private readonly IOperationRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<PagedList<OperationDto>> Handle(GetOperationsQuery request, CancellationToken cancellationToken)
    {

        var operationsWithMetaData = await _repository.Get(request.OperationParameters, trackChanges: false);

        var operationDtos = _mapper.Map<IEnumerable<OperationDto>>(operationsWithMetaData);

        var operationsDtoWithMetaData = new PagedList<OperationDto>(
            operationDtos.ToList(),
            operationsWithMetaData.MetaData.TotalCount,
            request.OperationParameters.PageNumber,
            request.OperationParameters.PageSize
        );

        return operationsDtoWithMetaData;
    }
}
