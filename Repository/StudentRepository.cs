using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Lecture)
                        .ThenInclude(l => l.LectureSessions)
                            .ThenInclude(ls => ls.Room)
                .Include(s => s.Department)
                .OrderBy(s => s.FullName)
                .ToListAsync();
        }

        // Get a specific student by ID
        public async Task<Student> GetStudentAsync(int studentId, bool trackChanges)
        {
            return await FindByCondition(s => s.StudentId == studentId, trackChanges)
             .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Lecture)
                        .ThenInclude(l => l.LectureSessions)
                            .ThenInclude(ls => ls.Room)
                .Include(s => s.Department)
                .Include(s => s.ClubMemberships)
                    .ThenInclude(cm => cm.Club)
                .SingleOrDefaultAsync();
        }

        // Get students by department ID
        public async Task<IEnumerable<Student>> GetStudentsByDepartmentId(int departmentId, bool trackChanges)
        {
            return await FindByCondition(s => s.DepartmentId == departmentId, trackChanges)
             .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Lecture)
                        .ThenInclude(l => l.LectureSessions)
                            .ThenInclude(ls => ls.Room)
                .Include(s => s.Department)
                .OrderBy(s => s.FullName)
                .ToListAsync();
        }

        // Create a new student
        public void CreateStudent(Student student)
        {
            Create(student);
        }

        // Update an existing student
        public async Task UpdateStudentAsync(Student student)
        {
            var existingStudent = await GetStudentAsync(student.StudentId, true);
            if (existingStudent != null)
            {
                existingStudent.FullName = student.FullName;
                existingStudent.Email = student.Email;
                existingStudent.GradeLevel = student.GradeLevel;
                existingStudent.DepartmentId = student.DepartmentId;

                Update(existingStudent);
            }
        }

        // Delete a student
        public async Task DeleteStudentAsync(Student student)
        {
            var existingStudent = await GetStudentAsync(student.StudentId, true);
            if (existingStudent != null)
            {
                Delete(existingStudent);
            }
        }

        public Student GetStudentByEmail(string email)
        {
            var student = FindByCondition(s => s.Email == email, false)
                        .Include(s => s.Enrollments)
                            .ThenInclude(e => e.Lecture)
                                .ThenInclude(l => l.LectureSessions)
                                    .ThenInclude(ls => ls.Room)
                        .Include(s => s.Department).SingleOrDefault();

            return student;
        }
    }
}
