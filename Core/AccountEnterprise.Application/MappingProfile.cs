using AutoMapper;
using AccountEnterprise.Domain.Entities;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Domain.RequestFeatures;

namespace AccountEnterprise.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Department, DepartmentDto>();
        CreateMap<DepartmentForCreationDto, Department>();
		CreateMap<DepartmentForUpdateDto, Department>();

		CreateMap<Employee, EmployeeDto>()
             .ForMember(x => x.Id, opt => 
                opt.MapFrom(a => a.EmployeeId))
             .ForMember(e => e.FullName, opt => 
                opt.MapFrom(x => 
                    string.Join(' ', x.Surname, x.Name, x.Midname)));

        CreateMap<EmployeeForCreationDto, Employee>();
		CreateMap<EmployeeForUpdateDto, Employee>();

        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryForCreationDto, Category>();
		CreateMap<CategoryForUpdateDto, Category>();

		CreateMap<Account, AccountDto>()
			.ForMember(x => x.Id, opt => opt.MapFrom(a => a.AccountId));
        CreateMap<AccountForCreationDto, Account>();
		CreateMap<AccountForUpdateDto, Account>();

        CreateMap<OperationType, OperationTypeDto>();
        CreateMap<OperationTypeForCreationDto, OperationType>();
		CreateMap<OperationTypeForUpdateDto, OperationType>();

        CreateMap<Operation, OperationDto>();
        CreateMap<OperationForCreationDto, Operation>();
		CreateMap<OperationForUpdateDto, Operation>();

        CreateMap<Transaction, TransactionDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(a => a.TransactionId));
        CreateMap<TransactionForCreationDto, Transaction>();
		CreateMap<TransactionForUpdateDto, Transaction>();

        CreateMap<User, UserDto>();
        CreateMap<UserForCreationDto, User>();
        CreateMap<UserForUpdateDto, User>();
    }
}

