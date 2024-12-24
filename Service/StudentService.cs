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
    public class StudentService : IStudentService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public StudentService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        // Get all students
        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync(bool trackChanges)
        {
            var students = await _repositoryManager.Student.GetAllStudentsAsync(trackChanges);
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        // Get a specific student by ID
        public async Task<StudentDto> GetStudentByIdAsync(int studentId, bool trackChanges)
        {
            var student = await _repositoryManager.Student.GetStudentAsync(studentId, trackChanges);

            if (student == null)
                throw new KeyNotFoundException($"Student with ID {studentId} not found.");

            return _mapper.Map<StudentDto>(student);
        }

        // Get lectures for a specific student
        public async Task<IEnumerable<LectureDto>> GetStudentLecturesAsync(int studentId, bool trackChanges)
        {
            var enrollments = await _repositoryManager.Enrollment.GetAllEnrollmentsAsync(trackChanges);
            var studentEnrollments = enrollments.Where(e => e.StudentId == studentId);

            if (!studentEnrollments.Any())
                throw new KeyNotFoundException($"No lectures found for Student with ID {studentId}.");

            var lectureCodes = studentEnrollments.Select(e => e.LectureCode);
            var lectures = await _repositoryManager.Lecture.GetAllLecturesAsync(trackChanges);
            var studentLectures = lectures.Where(l => lectureCodes.Contains(l.Code));

            return _mapper.Map<IEnumerable<LectureDto>>(studentLectures);
        }

        // Get clubs for a specific student
        public async Task<IEnumerable<ClubDto>> GetStudentClubsAsync(int studentId, bool trackChanges)
        {
            var memberships = await _repositoryManager.ClubMembership.GetAllClubMembershipsAsync(trackChanges);
            var studentMemberships = memberships.Where(m => m.StudentId == studentId);

            if (!studentMemberships.Any())
                throw new KeyNotFoundException($"No clubs found for Student with ID {studentId}.");

            var clubIds = studentMemberships.Select(m => m.ClubId);
            var clubs = await _repositoryManager.Club.GetAllClubsAsync(trackChanges);
            var studentClubs = clubs.Where(c => clubIds.Contains(c.ClubId));

            return _mapper.Map<IEnumerable<ClubDto>>(studentClubs);
        }

        // Get exams for a specific student
        public async Task<IEnumerable<ExamDto>> GetStudentExamsAsync(int studentId, bool trackChanges)
        {
            var enrollments = await _repositoryManager.Enrollment.GetAllEnrollmentsAsync(trackChanges);
            var studentEnrollments = enrollments.Where(e => e.StudentId == studentId);

            if (!studentEnrollments.Any())
                throw new KeyNotFoundException($"No exams found for Student with ID {studentId}.");

            var lectureCodes = studentEnrollments.Select(e => e.LectureCode);
            var exams = await _repositoryManager.Exam.GetAllExamsAsync(trackChanges);
            var studentExams = exams.Where(e => lectureCodes.Contains(e.LectureCode));

            return _mapper.Map<IEnumerable<ExamDto>>(studentExams);
        }

        // Get students by department ID
        public async Task<IEnumerable<StudentDto>> GetStudentsByDepartmentAsync(int departmentId, bool trackChanges)
        {
            var students = await _repositoryManager.Student.GetAllStudentsAsync(trackChanges);
            var departmentStudents = students.Where(s => s.DepartmentId == departmentId);

            if (!departmentStudents.Any())
                throw new KeyNotFoundException($"No students found for Department with ID {departmentId}.");

            return _mapper.Map<IEnumerable<StudentDto>>(departmentStudents);
        }

        // Create a new student
        public async Task CreateStudentAsync(StudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            _repositoryManager.Student.CreateStudent(student);
            await _repositoryManager.SaveAsync();
        }

        // Update an existing student
        public async Task UpdateStudentAsync(int studentId, StudentDto studentDto)
        {
            var student = await _repositoryManager.Student.GetStudentAsync(studentId, true);

            if (student == null)
                throw new KeyNotFoundException($"Student with ID {studentId} not found.");

            _mapper.Map(studentDto, student);
            await _repositoryManager.SaveAsync();
        }

        // Delete a student
        public async Task DeleteStudentAsync(int studentId)
        {
            var student = await _repositoryManager.Student.GetStudentAsync(studentId, true);

            if (student == null)
                throw new KeyNotFoundException($"Student with ID {studentId} not found.");

            await _repositoryManager.Student.DeleteStudentAsync(student);
            await _repositoryManager.SaveAsync();
        }
    }
}
