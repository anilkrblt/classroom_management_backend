using Shared.DataTransferObjects;
using Service.Contracts;
using Contracts;
using AutoMapper;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public DepartmentService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        // Get all departments
        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync(bool trackChanges)
        {
            var departments = await _repositoryManager.Department.GetAllDepartmentsAsync(trackChanges);
            return _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        }

        // Get a specific department by ID
        public async Task<DepartmentDto> GetDepartmentByIdAsync(int departmentId, bool trackChanges)
        {
            var department = await _repositoryManager.Department.GetDepartmentAsync(departmentId, trackChanges);

            if (department == null)
                throw new KeyNotFoundException($"Department with ID {departmentId} not found.");

            return _mapper.Map<DepartmentDto>(department);
        }

        // Get instructors of a specific department
        public async Task<IEnumerable<InstructorDto>> GetDepartmentInstructorsAsync(int departmentId, bool trackChanges)
        {
            var department = await _repositoryManager.Department.GetDepartmentAsync(departmentId, trackChanges);

            if (department == null)
                throw new KeyNotFoundException($"Department with ID {departmentId} not found.");

            var instructors = await _repositoryManager.Instructor.GetAllInstructorsAsync(trackChanges);
            var departmentInstructors = instructors.Where(i => i.DepartmentId == departmentId);

            return _mapper.Map<IEnumerable<InstructorDto>>(departmentInstructors);
        }

        // Get rooms of a specific department
        public async Task<IEnumerable<RoomDto>> GetDepartmentRoomsAsync(int departmentId, bool trackChanges)
        {
            var department = await _repositoryManager.Department.GetDepartmentAsync(departmentId, trackChanges);

            if (department == null)
                throw new KeyNotFoundException($"Department with ID {departmentId} not found.");

            var rooms = await _repositoryManager.Room.GetAllRoomsAsync(trackChanges);
            var departmentRooms = rooms.Where(r => r.DepartmentId == departmentId);

            return _mapper.Map<IEnumerable<RoomDto>>(departmentRooms);
        }

        // Get students of a specific department
        public async Task<IEnumerable<StudentDto>> GetDepartmentStudentsAsync(int departmentId, bool trackChanges)
        {
            var department = await _repositoryManager.Department.GetDepartmentAsync(departmentId, trackChanges);

            if (department == null)
                throw new KeyNotFoundException($"Department with ID {departmentId} not found.");

            var students = await _repositoryManager.Student.GetAllStudentsAsync(trackChanges);
            var departmentStudents = students.Where(s => s.DepartmentId == departmentId);

            return _mapper.Map<IEnumerable<StudentDto>>(departmentStudents);
        }

        // Create a new department
        public async Task CreateDepartmentAsync(DepartmentDto departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);
            _repositoryManager.Department.CreateDepartment(department);
            await _repositoryManager.SaveAsync();
        }

        // Update an existing department
        public async Task UpdateDepartmentAsync(int departmentId, DepartmentDto departmentDto)
        {
            var department = await _repositoryManager.Department.GetDepartmentAsync(departmentId, true);

            if (department == null)
                throw new KeyNotFoundException($"Department with ID {departmentId} not found.");

            _mapper.Map(departmentDto, department);
            await _repositoryManager.SaveAsync();
        }

        // Delete a department
        public async Task DeleteDepartmentAsync(int departmentId)
        {
            var department = await _repositoryManager.Department.GetDepartmentAsync(departmentId, true);

            if (department == null)
                throw new KeyNotFoundException($"Department with ID {departmentId} not found.");

            await _repositoryManager.Department.DeleteDepartmentAsync(department);
            await _repositoryManager.SaveAsync();
        }
    }
}
