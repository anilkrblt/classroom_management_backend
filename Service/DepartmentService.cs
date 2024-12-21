
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using NLog;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public DepartmentService(IRepositoryManager repository,ILogger logger,IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper=mapper;
        }
        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync(bool trackChanges)
        {
            var departments =await _repository.Department.GetAllDepartmentsAsync(trackChanges);
            var departmentsDto = _mapper.Map<IEnumerable<DepartmentDto>>(departments);

            return departmentsDto;
        }

        public async Task<DepartmentDto> GetDepartmentByIdAsync(int departmentId, bool trackChanges)
        {
            var department = await GetDepartmentAndCheckIfItExists(departmentId, trackChanges);

            var departmentDto = _mapper.Map<DepartmentDto>(department);
            return departmentDto;
        }

        public async Task<IEnumerable<DepartmentDto>> GetDepartmentsByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var departmentEntities = await _repository.Department.GetByIdsAsync(ids,trackChanges);
            if (ids.Count() != departmentEntities.Count())
                throw new CollectionByIdsBadRequestException();

            var departmentsToReturn = _mapper.Map<IEnumerable<DepartmentDto>>(departmentEntities);

            return departmentsToReturn;
        }

        private async Task<Department> GetDepartmentAndCheckIfItExists(int departmentId, bool trackChanges)
        {
            var department = await _repository.Department.GetDepartmentAsync(departmentId, trackChanges);
            if (department is null)
                throw new DepartmentNotFoundException(departmentId);
            return department;
        }
    }
}