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
            // Burada lectureEntity.Code, Name, Grade, Term dolu gelir; DepartmentId gibi relational alanlar yok.

            // 2) Department bilgisini metne göre bul (ör. DepartmentRepository'de bir metot var: GetDepartmentByNameAsync)
            var departments = await _repositoryManager.Department.GetAllDepartmentsAsync(false);
            try
            {
                var departmentEntity = departments.Where(d => d.Name == lectureCreateDto.Department).FirstOrDefault();
                if (departmentEntity == null)
                    throw new Exception($"Department '{lectureCreateDto.Department}' not found.");
                var lectureEntity = _mapper.Map<Lecture>(lectureCreateDto);

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
            catch (System.Exception)
            {

                throw new ArgumentException("Departman Yok");
            }


        }



        public async Task UpdateLectureAsync(string code, LectureUpdateDto lectureDto)
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

            // 4) Kayıtları veritabanına yaz
            await _repositoryManager.SaveAsync();
        }

    

        public async Task CreateLectureInstructorAsync(int instructorId, string lectureCode)
        {
            // 1) Opsiyonel kontrol: zaten var mı?
            var existingRecord = await _repositoryManager.LectureInstructor
                .GetLectureInstructorAsync(instructorId, lectureCode, trackChanges: false);

            if (existingRecord != null)
                throw new InvalidOperationException(
                    $"This instructor (ID={instructorId}) is already assigned to Lecture '{lectureCode}'.");

            // 2) Gerekliyse Lecture veya Instructor var mı kontrol (KeyNotFound hatası atmak için).
            //    Örneğin: 
            // var lecture = await _repositoryManager.Lecture.GetLectureAsync(lectureCode, trackChanges:false);
            // if (lecture == null) throw new KeyNotFoundException($"Lecture '{lectureCode}' not found.");
            // var instructor = await _repositoryManager.Instructor.GetInstructorAsync(instructorId, trackChanges:false);
            // if (instructor == null) throw new KeyNotFoundException($"InstructorId '{instructorId}' not found.");

            // 3) Yeni LectureInstructor oluştur
            var lectureInstructor = new LectureInstructor
            {
                InstructorId = instructorId,
                LectureCode = lectureCode
            };

            // 4) Ekle
            _repositoryManager.LectureInstructor.CreateInstructorLecture(lectureInstructor);

            // 5) Kayıt
            await _repositoryManager.SaveAsync();
        }



        public async Task DeleteLectureInstructorAsync(int instructorId, string lectureCode)
        {
            // 1) Kayıt var mı?
            var lectureInstructor = await _repositoryManager.LectureInstructor
                .GetLectureInstructorAsync(instructorId, lectureCode, trackChanges: true);

            if (lectureInstructor == null)
                throw new KeyNotFoundException(
                    $"LectureInstructor with InstructorId={instructorId} " +
                    $"and LectureCode='{lectureCode}' not found.");

            // 2) Sil
            _repositoryManager.LectureInstructor.DeleteInstructorLecture(lectureInstructor);

            // 3) Kaydet
            await _repositoryManager.SaveAsync();
        }



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
