using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using NLog;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    public class ExamService : IExamService
    {


        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;


        public ExamService(IRepositoryManager repository, ILogger logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ExamDto> CreateExam(ExamDto examForCreation, bool trackChanges)
        {
            var examEntity = _mapper.Map<Exam>(examForCreation);
            _repository.Exam.CreateExamForLecture(examEntity, examEntity.Code);
            await _repository.SaveAsync();
            var examToReturn = _mapper.Map<ExamDto>(examEntity);
            return examToReturn;
        }
        public async Task<ExamClassDto> CreateExamClassForExam(ExamClassDto examClassForCreation, bool trackChanges)
        {
            var examClassEntity = _mapper.Map<ExamClass>(examClassForCreation);
            _repository.ExamClass.CreateExamClass(examClassEntity);
            await _repository.SaveAsync();
            var examClassToReturn = _mapper.Map<ExamClassDto>(examClassEntity);
            return examClassToReturn;
        }
        public async Task DeleteExamAsync(int examId, bool trackChanges)
        {
            var exam = await GetExamAndCheckIfItExists(examId, trackChanges);
            _repository.Exam.DeleteExam(exam);
            await _repository.SaveAsync();
        }
        public async Task DeleteExamClassForExamAsync(int examId, int examClassId, bool trackChanges)
        {
            await GetExamAndCheckIfItExists(examId, trackChanges);
            var examClass = await GetExamClassAndCheckIfItExists(examClassId, trackChanges);
            _repository.ExamClass.DeleteExamClass(examClass);
            await _repository.SaveAsync();
        }
        public async Task<IEnumerable<ExamClassDto>> GetAllExamClassesAsync(bool trackChanges)
        {
            var examClasses = await _repository.ExamClass.GetAllExamClasssAsync(trackChanges);

            var examClassesDto = _mapper.Map<IEnumerable<ExamClassDto>>(examClasses);

            return examClassesDto;
        }
        public async Task<IEnumerable<ExamDto>> GetAllExamsAsync(bool trackChanges)
        {
            var exams = await _repository.Exam.GetAllExamsAsync(trackChanges);
            var examsDto = _mapper.Map<IEnumerable<ExamDto>>(exams);
            return examsDto;
        }
        public async Task<ExamDto> GetExamByIdAsync(int examId, bool trackChanges)
        {
            var exam = await GetExamAndCheckIfItExists(examId, trackChanges);
            var examDto = _mapper.Map<ExamDto>(exam);
            return examDto;
        }
        public async Task<IEnumerable<ExamDto>> GetExamsByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var examEntities = await _repository.Exam.GetByIdsAsync(ids, trackChanges);
            if (ids.Count() != examEntities.Count())
                throw new CollectionByIdsBadRequestException();

            var examsToReturn = _mapper.Map<IEnumerable<ExamDto>>(examEntities);

            return examsToReturn;
        }
        public async Task UpdateExamAsync(int examId, ExamDto examForUpdate, bool trackChanges)
        {
            var exam = await GetExamAndCheckIfItExists(examId, trackChanges);
            _mapper.Map(examForUpdate, exam);
            await _repository.SaveAsync();
        }
        public async Task UpdateExamClassAsync(int examClassId, ExamClassDto examClassForUpdate, bool trackChanges)
        {
            var examClass = await GetExamClassAndCheckIfItExists(examClassId, trackChanges);
            _mapper.Map(examClassForUpdate, examClass);
            await _repository.SaveAsync();
        }
        public async Task<RoomDto> GetExamClassByExamIdAsync(int examId, bool trackChanges)
        {
            var exam = await GetExamAndCheckIfItExists(examId, trackChanges);
            var examClass = exam.ExamClasses.FirstOrDefault();
            if (examClass is null)
                throw new ExamClassNotFoundException(examClass.ExamId);
            var examRoom = examClass.Room;
            return _mapper.Map<RoomDto>(examRoom);
        }

//GetExamsByDepartmentId


/*
        public async IEnumerable<ExamDto> GetExamsByStudentIdAsync(int studentId, bool trackChanges)
        {
            var student=await _repository.Student.GetStudentAsync(studentId,trackChanges);
            if(student is null)
                throw new StudentNotFoundException(studentId);
            var exams= await _repository.Exam.GetAllExamsAsync(trackChanges);
            var studentEnrollments = await _repository.Enrollment.GetEnrollmentByStudentIdsAsync(trackChanges);
            exams.Where(e =>e.Code.Contains());
        }
*/





        private async Task<Exam> GetExamAndCheckIfItExists(int examId, bool trackChanges)
        {
            var exam = await _repository.Exam.GetExamAsync(examId, trackChanges);
            if (exam is null)
                throw new ExamNotFoundException(examId);
            return exam;
        }

        private async Task<ExamClass> GetExamClassAndCheckIfItExists(int examClassId, bool trackChanges)
        {
            var examClass = await _repository.ExamClass.GetExamClassAsync(examClassId, trackChanges);
            if (examClass is null)
                throw new ExamClassNotFoundException(examClassId);
            return examClass;
        }

        public IEnumerable<ExamDto> GetExamsByStudentIdAsync(int studentId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ExamDto>> GetExamsByDepartmentId(int departmentId, bool trackChanges)
        {
            var department = await _repository.Department.GetDepartmentAsync(departmentId,trackChanges);
            if (department is null)
                throw new DepartmentNotFoundException(departmentId);
            var exams=await _repository.Exam.GetAllExamsAsync(trackChanges);
            exams=exams.Where(e=>e.Lecture.DepartmentId==departmentId);
            return _mapper.Map<IEnumerable<ExamDto>>(exams);
        }
    }
}