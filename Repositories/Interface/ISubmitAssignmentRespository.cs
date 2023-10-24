using BusinessObjects.Models;
using BusinessObjects.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface ISubmitAssignmentRespository
    {
        void SubmitAssignment(SubmitAssignmentViewModel submitAssignmentViewModel);
        IEnumerable<SubmitAssignment> ListSubmitAssignmentByAssId(int assId);
        SubmitAssignment GetSubmitAssignmentsById(int id);
    }
}
