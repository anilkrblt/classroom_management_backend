using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ILectureInstructor
    {
        Task<IEnumerable<LectureInstructor>> GetAllInstructorLecturesAsync(bool trackChanges);
        Task<IEnumerable<LectureInstructor>> GetInstructorLecturesAsync(int instructorId, bool trackChanges);

        void CreateInstructorLecture(LectureInstructor LectureInstructor);

        Task UpdateInstructorLectureAsync(LectureInstructor LectureInstructor);
        void DeleteInstructorLecture(LectureInstructor LectureInstructor);

    }
}