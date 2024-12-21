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
    public class InstructorService : IInstructorService
    {

        private readonly IRepositoryManager _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;


        public InstructorService(IRepositoryManager repository,ILogger logger,IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper=mapper;
        }

        public async Task<InstructorDto> CreateInstructorForDepartment(InstructorDto instructorForCreation,int departmentId, bool trackChanges)
        {
            await GetDepartmentAndCheckIfItExists(departmentId, trackChanges);

            var instructorEntity = _mapper.Map<Instructor>(instructorForCreation);

            _repository.Instructor.CreateInstructorForDepartment(instructorEntity,departmentId);
            await _repository.SaveAsync();
            var employeeToReturn = _mapper.Map<InstructorDto>(instructorEntity);

            return employeeToReturn;
        }

        public async Task DeleteInstructorForDepartmentAsync(int departmentId, int instructorId, bool trackChanges)
        {
            await GetDepartmentAndCheckIfItExists(departmentId, trackChanges);

            var instructor = await _repository.Instructor.GetInstructorAsync(instructorId,trackChanges);
            if (instructor is null)
                throw new InstructorNotFoundException(instructorId);
            _repository.Instructor.DeleteInstructor(instructor);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<InstructorDto>> GetAllInstructorsAsync(bool trackChanges)
        {
            var instructors=await _repository.Instructor.GetAllInstructorsAsync(trackChanges);
            var instructorsDto=_mapper.Map<IEnumerable<InstructorDto>>(instructors);
            return instructorsDto;
        }

        public async Task<InstructorDto> GetInstructorByIdAsync(int instructorId, bool trackChanges)
        {
            var instructor=await GetInstructorAndCheckIfItExists(instructorId,trackChanges);
            var instructorDto=_mapper.Map<InstructorDto>(instructor);
            return instructorDto;
        }

        public async Task<IEnumerable<LectureSessionDto>> GetInstructorForLectureSessionsAsync(int instructorId, bool trackChanges)
        {
            var lectureSessions = await _repository.LectureSession.GetAllLectureSessionsAsync(trackChanges);
            var instructorsSessions = lectureSessions.Where(ls => ls.InstructorId==instructorId);
            return _mapper.Map<IEnumerable<LectureSessionDto>>(instructorsSessions);

        }

        public async Task<IEnumerable<InstructorDto>> GetInstructorsByIdsAsync(IEnumerable<int> instructorId, bool trackChanges)
        {
            if (instructorId is null)
                throw new IdParametersBadRequestException();

            var instructorEntities=await _repository.Club.GetByIdsAsync(instructorId,trackChanges);
            if (instructorId.Count() != instructorEntities.Count())
                throw new CollectionByIdsBadRequestException();

            var instructorsToReturn = _mapper.Map<IEnumerable<InstructorDto>>(instructorEntities);

            return instructorsToReturn;
        }

        public async Task UpdateInstructorForDepartment(int instructorId, int departmentId, InstructorDto instructorForUpdate, bool insTrackChanges, bool depTrackChanges)
        {
            await GetDepartmentAndCheckIfItExists(departmentId,depTrackChanges);

            var instructor = await _repository.Instructor.GetInstructorAsync(instructorId,insTrackChanges);
            if (instructor is null)
                throw new InstructorNotFoundException(instructorId);
            _mapper.Map(instructorForUpdate, instructor);
            await _repository.SaveAsync();
        }

        public Task UpdateInstructorLectureSessions(int instructorId, LectureSessionDto lectureSession)
        {
            throw new NotImplementedException();
        }







        private async Task<Instructor> GetInstructorAndCheckIfItExists(int instructorId, bool trackChanges)
        {
            var instructor = await _repository.Instructor.GetInstructorAsync(instructorId, trackChanges);
            if (instructor is null)
                throw new InstructorNotFoundException(instructorId);
            return instructor;
        }


        private async Task<Department> GetDepartmentAndCheckIfItExists(int departmentId, bool trackChanges)
        {
            var department = await _repository.Department.GetDepartmentAsync(departmentId, trackChanges);
            if (department is null)
                throw new DepartmentNotFoundException(departmentId);
            return department;
        }


    }
}