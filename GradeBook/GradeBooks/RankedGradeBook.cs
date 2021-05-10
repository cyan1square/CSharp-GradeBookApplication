using GradeBook.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        private int minClassSize;

        public RankedGradeBook(string name):base(name)
        {
            Type = GradeBookType.Ranked;
            minClassSize = 5;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if(Students.Count < minClassSize)
            {
                throw new InvalidOperationException();
            }

            Students.Sort((s2, s1) => s1.AverageGrade.CompareTo(s2.AverageGrade));

            int A_GradeStudents = (int)((Students.Count * 1.0) - (Students.Count * 0.8));
            int B_GradeStudents_TopRange = (int)((Students.Count * 0.8) - (Students.Count * 0.6)) + A_GradeStudents;
            int C_GradeStudents_TopRange = (int)((Students.Count * 0.6) - (Students.Count * 0.4)) + B_GradeStudents_TopRange;
            int D_GradeStudents_TopRange = (int)((Students.Count * 0.4) - (Students.Count * 0.2)) + C_GradeStudents_TopRange;
            int F_GradeStudents = (int)((Students.Count * 0.2)) + D_GradeStudents_TopRange;

            if ((averageGrade <= Students[A_GradeStudents - 1].AverageGrade) &&
                 (averageGrade > Students[B_GradeStudents_TopRange - 1].AverageGrade)) return 'A';

            if ((averageGrade <= Students[B_GradeStudents_TopRange - 1].AverageGrade) &&
                 (averageGrade > Students[C_GradeStudents_TopRange - 1].AverageGrade)) return 'B';

            if ((averageGrade <= Students[C_GradeStudents_TopRange - 1].AverageGrade) &&
                 (averageGrade > Students[D_GradeStudents_TopRange - 1].AverageGrade)) return 'C';

            if ((averageGrade <= Students[D_GradeStudents_TopRange - 1].AverageGrade) &&
                 (averageGrade > Students[F_GradeStudents - 1].AverageGrade)) return 'D';

            return 'F';
        }

        public override void CalculateStatistics()
        {
            if(Students.Count < minClassSize)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

              base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < minClassSize)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStudentStatistics(name);
        }
    }
}
