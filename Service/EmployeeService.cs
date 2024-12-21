using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using NLog;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public EmployeeService(IRepositoryManager repository,ILogger logger,IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper=mapper;
        }
        public async Task<EmployeeDto> CreateEmployee(EmployeeDto employeeForCreation, bool trackChanges)
        {
            var employeeEntity=_mapper.Map<Employee>(employeeForCreation);
            _repository.Employee.CreateEmployee(employeeEntity);
            await _repository.SaveAsync();
            var employeeToReturn=_mapper.Map<EmployeeDto>(employeeEntity);
            return employeeToReturn;
        }

        public async Task DeleteEmployeeAsync(int employeeId, bool trackChanges)
        {
            var employee=await GetEmployeeAndCheckIfItExists(employeeId,trackChanges);
            _repository.Employee.DeleteEmployee(employee);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(bool trackChanges)
        {
            var employees=await _repository.Employee.GetAllEmployeesAsync(trackChanges);
            var employeesDto=_mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return employeesDto;
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int employeeId, bool trackChanges)
        {
            var employee=await GetEmployeeAndCheckIfItExists(employeeId,trackChanges);
            var employeeDto=_mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var employeeEntities=await _repository.Employee.GetByIdsAsync(ids,trackChanges);
            if (ids.Count() != employeeEntities.Count())
                throw new CollectionByIdsBadRequestException();

            var employeesToReturn = _mapper.Map<IEnumerable<EmployeeDto>>(employeeEntities);

            return employeesToReturn;
        }

        public async Task UpdateEmployeeAsync(int employeeId, EmployeeDto employeeForUpdate, bool trackChanges)
        {
            var employee=await GetEmployeeAndCheckIfItExists(employeeId,trackChanges);
            _mapper.Map(employeeForUpdate, employee);
            await _repository.SaveAsync();
        }

        private async Task<Employee> GetEmployeeAndCheckIfItExists(int employeeId, bool trackChanges)
        {
            var employee = await _repository.Employee.GetEmployeeAsync(employeeId, trackChanges);
            if (employee is null)
                throw new EmployeeNotFoundException(employeeId);
            return employee;
        }
    }
}