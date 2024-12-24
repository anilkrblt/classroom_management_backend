using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class ExamConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.HasData
            (
                new Exam
                {
                    ExamId = 1,
                    Name = "Mathematics Final Exam",
                    LectureCode = "MATH101"
                },
                new Exam
                {
                    ExamId = 2,
                    Name = "Physics Midterm Exam",
                    LectureCode = "PHYS201"
                },
                new Exam
                {
                    ExamId = 3,
                    Name = "Chemistry Final Exam",
                    LectureCode = "CHEM301"
                },
                new Exam
                {
                    ExamId = 4,
                    Name = "Biology Quiz",
                    LectureCode = "BIOL102"
                },
                new Exam
                {
                    ExamId = 5,
                    Name = "Computer Science Lab Exam",
                    LectureCode = "CS202"
                },
                new Exam
                {
                    ExamId = 6,
                    Name = "English Language Test",
                    LectureCode = "ENG103"
                },
                new Exam
                {
                    ExamId = 7,
                    Name = "History Midterm Exam",
                    LectureCode = "HIST205"
                },
                new Exam
                {
                    ExamId = 8,
                    Name = "Engineering Mechanics Final",
                    LectureCode = "ENGM202"
                },
                new Exam
                {
                    ExamId = 9,
                    Name = "Economics Final Exam",
                    LectureCode = "ECON301"
                },
                new Exam
                {
                    ExamId = 10,
                    Name = "Philosophy Critical Thinking Test",
                    LectureCode = "PHIL101"
                }
            );
        }
    }
}
