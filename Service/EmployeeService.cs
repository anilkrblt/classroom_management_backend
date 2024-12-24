using Shared.DataTransferObjects;
using Service.Contracts;
using Contracts;
using AutoMapper;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public EmployeeService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        // Get all employees
        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(bool trackChanges)
        {
            var employees = await _repositoryManager.Employee.GetAllEmployeesAsync(trackChanges);
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        // Get a specific employee by ID
        public async Task<EmployeeDto> GetEmployeeByIdAsync(int employeeId, bool trackChanges)
        {
            var employee = await _repositoryManager.Employee.GetEmployeeAsync(employeeId, trackChanges);

            if (employee == null)
                throw new KeyNotFoundException($"Employee with ID {employeeId} not found.");

            return _mapper.Map<EmployeeDto>(employee);
        }

        // Create a new employee
        public async Task CreateEmployeeAsync(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);

            _repositoryManager.Employee.CreateEmployee(employee);
            await _repositoryManager.SaveAsync();
        }

        // Update an existing employee
        public async Task UpdateEmployeeAsync(int employeeId, EmployeeDto employeeDto)
        {
            var employee = await _repositoryManager.Employee.GetEmployeeAsync(employeeId, true);

            if (employee == null)
                throw new KeyNotFoundException($"Employee with ID {employeeId} not found.");

            _mapper.Map(employeeDto, employee);
            await _repositoryManager.SaveAsync();
        }

        // Delete an employee
        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var employee = await _repositoryManager.Employee.GetEmployeeAsync(employeeId, true);

            if (employee == null)
                throw new KeyNotFoundException($"Employee with ID {employeeId} not found.");

            await _repositoryManager.Employee.DeleteEmployeeAsync(employee);
            await _repositoryManager.SaveAsync();
        }
    }
}
