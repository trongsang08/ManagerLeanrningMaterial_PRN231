using BusinessObjects.Models;
using BusinessObjects.ViewModels;
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
        public Assignment GetAssignmentsByAssId(int assId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Assignment> GetAssignmentsByCourseId(int courseId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Assignment> ListAssignmentByTeacherAndCourse(int teacherId, int courseId)
        {
            throw new NotImplementedException();
        }

        public void SaveAssignment(UploadAssignmentViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
