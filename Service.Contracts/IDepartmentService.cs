using Shared.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IDepartmentService
    {
        // Get all departments
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync(bool trackChanges);

        // Get a specific department by ID
        Task<DepartmentDto> GetDepartmentByIdAsync(int departmentId, bool trackChanges);

        // Get instructors of a specific department
        Task<IEnumerable<InstructorDto>> GetDepartmentInstructorsAsync(int departmentId, bool trackChanges);

        // Get rooms of a specific department
        Task<IEnumerable<RoomDto>> GetDepartmentRoomsAsync(int departmentId, bool trackChanges);

        // Get students of a specific department
        Task<IEnumerable<StudentDto>> GetDepartmentStudentsAsync(int departmentId, bool trackChanges);

        // Create a new department
        Task CreateDepartmentAsync(DepartmentForCreateDto departmenForCreatetDto);

        // Update an existing department
        Task UpdateDepartmentAsync(int departmentId, DepartmentForUpdateDto departmentForUpdateDto);

        // Delete a department
        Task DeleteDepartmentAsync(int departmentId);
    }
}
