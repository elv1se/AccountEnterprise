using AutoMapper;
using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
		CreateMap<Department, DepartmentDto>();
		CreateMap<DepartmentForCreationDto, Department>();
		CreateMap<DepartmentForUpdateDto, Department>();

		CreateMap<Employee, EmployeeDto>();
		CreateMap<EmployeeForCreationDto, Employee>();
		CreateMap<EmployeeForUpdateDto, Employee>();

		CreateMap<Category, CategoryDto>();
		CreateMap<CategoryForCreationDto, Category>();
		CreateMap<CategoryForUpdateDto, Category>();

		CreateMap<Account, AccountDto>();
		CreateMap<AccountForCreationDto, Account>();
		CreateMap<AccountForUpdateDto, Account>();

		CreateMap<OperationType, OperationTypeDto>();
		CreateMap<OperationTypeForCreationDto, OperationType>();
		CreateMap<OperationTypeForUpdateDto, OperationType>();

		CreateMap<Operation, OperationDto>();
		CreateMap<OperationForCreationDto, Operation>();
		CreateMap<OperationForUpdateDto, Operation>();

		CreateMap<Transaction, TransactionDto>();
		CreateMap<TransactionForCreationDto, Transaction>();
		CreateMap<TransactionForUpdateDto, Transaction>();
    }
}

