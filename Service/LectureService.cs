using Shared.DataTransferObjects;
using Service.Contracts;
using Contracts;
using AutoMapper;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class LectureService : ILectureService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public LectureService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LectureDto>> GetAllLecturesAsync(bool trackChanges)
        {
            var lectures = await _repositoryManager.Lecture.GetAllLecturesAsync(trackChanges);
            return _mapper.Map<IEnumerable<LectureDto>>(lectures);
        }

        public async Task<LectureDto> GetLectureByCodeAsync(string code, bool trackChanges)
        {
            var lecture = await _repositoryManager.Lecture.GetLectureAsync(code, trackChanges);

            if (lecture == null)
                throw new KeyNotFoundException($"Lecture with code {code} not found.");

            return _mapper.Map<LectureDto>(lecture);
        }

        public async Task CreateLectureAsync(LectureDto lectureDto)
        {
            var lecture = _mapper.Map<Lecture>(lectureDto);

            _repositoryManager.Lecture.CreateLecture(lecture);
            await _repositoryManager.SaveAsync();
        }

        public async Task UpdateLectureAsync(string code, LectureDto lectureDto)
        {
            var lecture = await _repositoryManager.Lecture.GetLectureAsync(code, trackChanges: true);

            if (lecture == null)
                throw new KeyNotFoundException($"Lecture with code {code} not found.");

            _mapper.Map(lectureDto, lecture);
            await _repositoryManager.SaveAsync();
        }

        public async Task DeleteLectureAsync(string code)
        {
            var lecture = await _repositoryManager.Lecture.GetLectureAsync(code, trackChanges: true);

            if (lecture == null)
                throw new KeyNotFoundException($"Lecture with code {code} not found.");

            await _repositoryManager.Lecture.DeleteLectureAsync(lecture);
            await _repositoryManager.SaveAsync();
        }
    }
}
