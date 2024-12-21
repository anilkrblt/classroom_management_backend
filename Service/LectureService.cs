using Shared.DataTransferObjects;
using Service.Contracts;
using Contracts;
using AutoMapper;
using Entities.Models;
using NLog;
using Entities.Exceptions;

namespace Service
{
    public class LectureService : ILectureService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public LectureService(IRepositoryManager repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<LectureDto>> GetAllLecturesAsync(bool trackChanges)
        {
            var lectures = await _repository.Lecture.GetAllLecturesAsync(trackChanges);
            return _mapper.Map<IEnumerable<LectureDto>>(lectures);
        }

        public async Task<LectureDto> GetLectureByCodeAsync(string lectureCode, bool trackChanges)
        {
            var lecture = await _repository.Lecture.GetLectureAsync(lectureCode, trackChanges);
            if (lecture is null)
                throw new LectureNotFoundException(lectureCode);
            return _mapper.Map<LectureDto>(lecture);
        }
        public IEnumerable<LectureDto> GetLecturesByCodesAsync(IEnumerable<string> lectureCodes, bool trackChanges)
        {
            if (lectureCodes is null)
                throw new IdParametersBadRequestException();
            var lectures = _repository.Lecture.GetByIdsAsync(lectureCodes, trackChanges).Result;
            if (lectureCodes.Count() != lectures.Count())
                throw new CollectionByIdsBadRequestException();

            return _mapper.Map<IEnumerable<LectureDto>>(lectures);
        }

        public async Task DeleteLectureForDepartmentAsync(string lectureCode, int departmentId, bool trackChanges)
        {
            var department = await _repository.Department.GetDepartmentAsync(departmentId, trackChanges);
            if (department is null)
                throw new DepartmentNotFoundException(departmentId);

            var lecture = await _repository.Lecture.GetLectureAsync(lectureCode, trackChanges);
            if (lecture == null)
                throw new ArgumentException("Lecture not found.");
            _repository.Lecture.DeleteLecture(lecture);
            await _repository.SaveAsync();
        }

        public async Task UpdateLectureForDepartment(int departmentId, string lectureCode, LectureDto lectureForUpdate, bool lectureTrackChanges, bool departmentTrackChanges)
        {
            var department = await _repository.Department.GetDepartmentAsync(departmentId, departmentTrackChanges);
            if (department is null)
                throw new DepartmentNotFoundException(departmentId);

            var lecture = await _repository.Lecture.GetLectureAsync(lectureCode, lectureTrackChanges);
            if (lecture == null)
                throw new LectureNotFoundException(lectureCode);
            _mapper.Map(lectureForUpdate, lecture);
            await _repository.SaveAsync();
        }

        public async Task<LectureDto> CreateLectureForDepartment(int departmentId, LectureDto lectureForCreation, bool lectureTrackChanges, bool departmentTrackChanges)
        {

            var department = await _repository.Department.GetDepartmentAsync(departmentId, departmentTrackChanges);
            if (department is null)
                throw new DepartmentNotFoundException(departmentId);

            var lectureEntity = _mapper.Map<Lecture>(lectureForCreation);

            _repository.Lecture.CreateLecture(departmentId, lectureEntity);
            await _repository.SaveAsync();

            return _mapper.Map<LectureDto>(lectureEntity);



        }
    }
}
