using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ILectureService
    {
        Task<IEnumerable<LectureDto>> GetAllLecturesAsync(bool trackChanges);
        Task<LectureDto> GetLectureByCodeAsync(string lectureCode, bool trackChanges);
        IEnumerable<LectureDto> GetLecturesByCodesAsync(IEnumerable<string> lectureCode, bool trackChanges);

        Task DeleteLectureForDepartmentAsync(string lectureCode, int departmentId, bool trackChanges);

        Task UpdateLectureForDepartment(int departmentId, string lectureCode,
        LectureDto lectureForUpdate, bool lectureTrackChanges, bool departmentTrackChanges);
        Task<LectureDto> CreateLectureForDepartment(int departmentId, LectureDto lectureForCreation, bool trackChanges, bool departmentTrackChanges);
    }
}