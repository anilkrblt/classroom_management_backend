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


            var lecturesDto = _mapper.Map<IEnumerable<LectureDto>>(lectures);
            return lecturesDto;
        }

        public async Task<LectureDto> GetLectureByCodeAsync(string code, bool trackChanges)
        {
            var lecture = await _repositoryManager.Lecture.GetLectureAsync(code, trackChanges);

            if (lecture == null)
                throw new KeyNotFoundException($"Lecture with code {code} not found.");

            return _mapper.Map<LectureDto>(lecture);
        }

        public async Task<LectureDto> CreateLectureAsync(LectureCreateDto lectureCreateDto)
        {
            // 1) LectureCreateDto -> Lecture (map)
            var lectureEntity = _mapper.Map<Lecture>(lectureCreateDto);
            // Burada lectureEntity.Code, Name, Grade, Term dolu gelir; DepartmentId gibi relational alanlar yok.

            // 2) Department bilgisini metne göre bul (ör. DepartmentRepository'de bir metot var: GetDepartmentByNameAsync)
            var departments = await _repositoryManager.Department.GetAllDepartmentsAsync(false);
            var departmentEntity = departments.Where(d => d.Name == lectureCreateDto.Department).FirstOrDefault();

            if (departmentEntity == null)
                throw new Exception($"Department '{lectureCreateDto.Department}' not found.");

            // Bulduğumuz departmanın Id'sini Lecture entity'sine setliyoruz
            lectureEntity.DepartmentId = departmentEntity.DepartmentId;

            // 3) Lecture kaydını ekle
            _repositoryManager.Lecture.CreateLecture(lectureEntity);
            // 4) Instructor listesi -> LectureInstructor kayıtları oluştur
            if (lectureCreateDto.Instructors != null)
            {
                foreach (var instructorDto in lectureCreateDto.Instructors)
                {
                    var lectureInstructor = new LectureInstructor
                    {
                        LectureCode = lectureEntity.Code,
                        InstructorId = instructorDto.InstructorId
                    };

                    _repositoryManager.LectureInstructor.CreateInstructorLecture(lectureInstructor);
                }
            }

            // 5) SaveChanges
            await _repositoryManager.SaveAsync();

            // 6) Yeni eklenen veriyi tekrar çekip (Include'larla) LectureDto'ya dönüştür
            // (Varsayıyorum ki GetLectureWithDetailsByCodeAsync benzeri bir metot var)
            var createdLecture = await _repositoryManager.Lecture.GetLectureAsync(lectureEntity.Code, false);

            var lectureDto = _mapper.Map<LectureDto>(createdLecture);
            return lectureDto;
        }



        public async Task UpdateLectureAsync(string code, LectureDto lectureDto)
        {
            // 1) Mevcut Lecture + LectureInstructor kayıtlarını getir
            var lecture = await _repositoryManager.Lecture.GetLectureAsync(code, trackChanges: true);
            // GetLectureWithInstructorsAsync => 
            //   Lecture + .Include(l => l.LectureInstructors)
            //     .ThenInclude(li => li.Instructor)

            if (lecture == null)
                throw new KeyNotFoundException($"Lecture with code {code} not found.");

            // 2) Lecture alanlarını güncelle (Name, Grade, Term, Department vs.)
            //    (Department name’den bulma işiniz varsa ekleyebilirsiniz)
            _mapper.Map(lectureDto, lecture);

            // 3) LectureInstructors güncelle
            //    (a) Mevcut instructor Id’leri
            var existingInstructorIds = lecture.LectureInstructors
                .Select(li => li.InstructorId)
                .ToList();

            //    (b) Yeni gelen instructor Id’leri
            var newInstructorIds = lectureDto.Instructors
                .Select(i => i.InstructorId)
                .ToList();

            //    (c) Silinmesi gereken instructorId’ler
            var toRemove = existingInstructorIds.Except(newInstructorIds).ToList();

            //    (d) Eklenmesi gereken instructorId’ler
            var toAdd = newInstructorIds.Except(existingInstructorIds).ToList();

            // (c) Sil -> LectureInstructor tablosundan bu instructor’ları ayıkla
            foreach (var instructorId in toRemove)
            {
                var lectureInstructor = lecture.LectureInstructors
                    .FirstOrDefault(li => li.InstructorId == instructorId);
                if (lectureInstructor != null)
                {
                    _repositoryManager.LectureInstructor.DeleteInstructorLecture(lectureInstructor);
                }
            }

            // (d) Ekle -> yeni LectureInstructor kayıtları
            foreach (var instructorId in toAdd)
            {
                // Dilersek instructor var mı yok mu kontrol edebiliriz
                var lectureInstructor = new LectureInstructor
                {
                    LectureCode = lecture.Code,
                    InstructorId = instructorId
                };
                _repositoryManager.LectureInstructor.CreateInstructorLecture(lectureInstructor);
            }

            // 4) Kayıtları veritabanına yaz
            await _repositoryManager.SaveAsync();
        }







        /*  public async Task UpdateLectureAsync(string code, LectureDto lectureDto)
          {
              var lecture = await _repositoryManager.Lecture.GetLectureAsync(code, trackChanges: true);

              if (lecture == null)
                  throw new KeyNotFoundException($"Lecture with code {code} not found.");

              _mapper.Map(lectureDto, lecture);
              await _repositoryManager.SaveAsync();
          }*/
        /*
                public async Task DeleteLectureAsync(string code)
                {
                    var lecture = await _repositoryManager.Lecture.GetLectureAsync(code, trackChanges: true);

                    if (lecture == null)
                        throw new KeyNotFoundException($"Lecture with code {code} not found.");

                    await _repositoryManager.Lecture.DeleteLectureAsync(lecture);
                    await _repositoryManager.SaveAsync();
                }
        */

        public async Task DeleteLectureAsync(string code)
        {
            var lecture = await _repositoryManager.Lecture.GetLectureAsync(code, trackChanges: true);

            if (lecture == null)
                throw new KeyNotFoundException($"Lecture with code {code} not found.");

            // 1) LectureInstructor tablosundaki ilişkili kayıtları çek
            var lectureInstructors =
                await _repositoryManager.LectureInstructor.GetAllInstructorLecturesAsync(trackChanges: true);
            lectureInstructors = lectureInstructors.Where(li => li.LectureCode == code);

            // 2) Hepsini tek tek sil
            foreach (var li in lectureInstructors)
            {
                _repositoryManager.LectureInstructor.DeleteInstructorLecture(li);
            }

            // 3) Sonra dersi sil
            await _repositoryManager.Lecture.DeleteLectureAsync(lecture);

            // 4) Save
            await _repositoryManager.SaveAsync();
        }



    }
}
