﻿using MediatR;
using AccountEnterprise.Application.Dtos;

namespace AccountEnterprise.Application.Requests.Commands;

public record CreateEmployeeCommand(EmployeeForCreationDto Employee) : IRequest;