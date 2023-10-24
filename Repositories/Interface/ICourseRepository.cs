using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface ICourseRepository
    {
        public IEnumerable<Course> GetAllCourseByTeacherId(int teacherId);
        public IEnumerable<Course> GetAllCourseByStudentId(int studentId);

        List<Course> GetAllCourse();
        Course GetCourseById(int courseId);
        void InsertCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(Course course);
    }
}
