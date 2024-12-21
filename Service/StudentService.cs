using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.DataTransferObjects;
using Service.Contracts;
using Contracts;
using AutoMapper;
using Entities.Models;
using NLog;

namespace Service
{
    public class StudentService : IStudentService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        public StudentService(IRepositoryManager repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<StudentDto> GetAllStudent(bool trackChanges)
        {
            var students = _repository.Student.GetAllStudentsAsync(trackChanges);

            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public StudentDto GetStudentById(int studentId, bool trackChanges)
        {
            var student = _repository.Student.GetStudentAsync(studentId, trackChanges).Result;
            return _mapper.Map<StudentDto>(student);
        }

        public IEnumerable<StudentDto> GetStudentsByDepartmentId(int departmentId, bool trackChanges)
        {
            var department = _repository.Department.GetDepartmentAsync(departmentId, trackChanges);
            if (department is null)
                throw new ArgumentException("departman gir mk");

            var students = _repository.Student.GetStudentsByDepartmentId(departmentId, trackChanges);
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync(bool trackChanges)
        {
            var enrollments = await _repository.Enrollment.GetAllEnrollmentsAsync(trackChanges);
            return _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments);
        }

        public async Task<IEnumerable<LectureDto>> GetStudentLecturesByStudentIdAsync(int studentId, bool trackChanges)
        {

            var enrollments = await _repository.Enrollment.GetEnrollmentsByStudentIdAsync(studentId, trackChanges);
            var lectureCodes = enrollments.Select(e => e.Code).ToList();
            var lectures = await _repository.Lecture.GetByIdsAsync(lectureCodes, trackChanges);
            return _mapper.Map<IEnumerable<LectureDto>>(lectures);


        }

       
    }
}
