using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IExamService
    {
        Task<IEnumerable<ExamDto>> GetAllExamsAsync(bool trackChanges);
        Task<ExamDto> GetExamByIdAsync(int examId, bool trackChanges);
        Task<IEnumerable<ExamDto>> GetExamsByIdsAsync(IEnumerable<int> ids, bool trackChanges);

        Task<IEnumerable<ExamDto>> GetExamsByStudentIdAsync(int studentId, bool trackChanges);

        Task DeleteExamAsync(int examId, bool trackChanges);

        Task<ExamDto> CreateExam(ExamDto examForCreation, bool trackChanges);

        Task UpdateExamAsync(int examId, ExamDto examForUpdate, bool trackChanges);


        Task<IEnumerable<ExamClassDto>> GetAllExamClassesAsync(bool trackChanges);
        Task<RoomDto> GetExamClassByExamIdAsync(int examId, bool trackChanges);
        Task UpdateExamClassAsync(int examClassId, ExamClassDto examClassForUpdate, bool trackChanges);


        Task DeleteExamClassForExamAsync(int examId, int examClassId, bool trackChanges);
        Task<ExamClassDto> CreateExamClassForExam(ExamClassDto examClassForCreation, bool trackChanges);

        Task<IEnumerable<ExamDto>> GetExamsByDepartmentId(int departmentId,bool trackChanges);
    }
}