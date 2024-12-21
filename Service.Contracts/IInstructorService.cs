using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IInstructorService
    {
        Task<IEnumerable<InstructorDto>> GetAllInstructorsAsync(bool trackChanges);
        Task<InstructorDto> GetInstructorByIdAsync(int instructorId, bool trackChanges);
        Task<IEnumerable<InstructorDto>> GetInstructorsByIdsAsync(IEnumerable<int> instructorId, bool trackChanges);

        Task DeleteInstructorForDepartmentAsync(int departmentId, int instructorId, bool trackChanges);

        Task UpdateInstructorForDepartment(int instructorId, int departmentId,
        InstructorDto instructorForUpdate, bool insTrackChanges, bool depTrackChanges);

        Task<IEnumerable<LectureSessionDto>> GetInstructorForLectureSessionsAsync(int instructorId, bool trackChanges);

        Task UpdateInstructorLectureSessions(int instructorId, LectureSessionDto lectureSession);

        Task<InstructorDto> CreateInstructorForDepartment(InstructorDto instructorForCreation,int departmentId, bool trackChanges);
    }
}