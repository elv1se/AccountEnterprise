using AutoMapper;
using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
		CreateMap<Department, DepartmentDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(a => a.DepartmentId));
        CreateMap<DepartmentForCreationDto, Department>();
		CreateMap<DepartmentForUpdateDto, Department>();

		CreateMap<Employee, EmployeeDto>()
             .ForMember(x => x.Id, opt => opt.MapFrom(a => a.EmployeeId));
        CreateMap<EmployeeForCreationDto, Employee>();
		CreateMap<EmployeeForUpdateDto, Employee>();

		CreateMap<Category, CategoryDto>()
             .ForMember(x => x.Id, opt => opt.MapFrom(a => a.CategoryId));
        CreateMap<CategoryForCreationDto, Category>();
		CreateMap<CategoryForUpdateDto, Category>();

		CreateMap<Account, AccountDto>()
			.ForMember(x => x.Id, opt => opt.MapFrom(a => a.AccountId));
        CreateMap<AccountForCreationDto, Account>();
		CreateMap<AccountForUpdateDto, Account>();

		CreateMap<OperationType, OperationTypeDto>()
              .ForMember(x => x.Id, opt => opt.MapFrom(a => a.OperationTypeId));
        CreateMap<OperationTypeForCreationDto, OperationType>();
		CreateMap<OperationTypeForUpdateDto, OperationType>();

		CreateMap<Operation, OperationDto>()
             .ForMember(x => x.Id, opt => opt.MapFrom(a => a.OperationId));
        CreateMap<OperationForCreationDto, Operation>();
		CreateMap<OperationForUpdateDto, Operation>();

        CreateMap<Transaction, TransactionDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(a => a.TransactionId));
        CreateMap<TransactionForCreationDto, Transaction>();
		CreateMap<TransactionForUpdateDto, Transaction>();
    }
}

