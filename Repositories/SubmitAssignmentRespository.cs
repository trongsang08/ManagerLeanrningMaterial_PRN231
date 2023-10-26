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
    public class SubmitAssignmentRespository : ISubmitAssignmentRespository
    {
        public SubmitAssignment GetSubmitAssignmentsById(int id)
        => SubmitAssignmentDao.GetSubmitAssignmentById(id);

        public IEnumerable<SubmitAssignment> ListSubmitAssignmentByAssId(int assId)
        => SubmitAssignmentDao.ListSubmitAssignmentByAssId(assId);

        public void SubmitAssignment(SubmitAssignmentViewModel submitAssignmentViewModel)
        => SubmitAssignmentDao.SubmitAssignment(submitAssignmentViewModel);
    }
}
