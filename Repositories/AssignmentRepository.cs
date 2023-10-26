using BusinessObjects.Models;
using BusinessObjects.ViewModels;
using DataAccess;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AssignmentRepository : IAssignmentRespository
    {
        public Assignment GetAssignmentsByAssId(int assId) => AssignmentDao.GetAssignmentsByAssId(assId);
        

        public IEnumerable<Assignment> GetAssignmentsByCourseId(int courseId)=> AssignmentDao.GetAssignmentsByCourseId(courseId);
        

        public IEnumerable<Assignment> ListAssignmentByTeacherAndCourse(int teacherId, int courseId)
         => AssignmentDao.ListAssignmentByTeacherAndCourse(teacherId, courseId);

        public void SaveAssignment(UploadAssignmentViewModel model) => AssignmentDao.SaveAssignment(model);
        
    }
}
