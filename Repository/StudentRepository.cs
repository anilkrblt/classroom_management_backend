using System;
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
                .OrderBy(s => s.Name) 
                .ToListAsync();
        }

        public async Task<Student> GetStudentAsync(int studentId, bool trackChanges)
        {
            return await FindByCondition(s => s.StudentId == studentId, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Student>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            return await FindByCondition(s => ids.Contains(s.StudentId), trackChanges)
                .ToListAsync();
        }

        public void DeleteStudent(Student student) => Delete(student);

        public void CreateStudentForDepartment(int departmentId, Student student)
        {
            student.DepartmentId = departmentId; 
            Create(student);
        }

        public async Task<IEnumerable<Student>> GetStudentsByDepartmentId(int departmentId, bool trackChanges)
        {
            return await FindByCondition(s => s.DepartmentId == departmentId, trackChanges).ToListAsync();
        }
    }
}
