using BusinessObjects.Models;
using BusinessObjects.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SubmitAssignmentDao
    {
        public static void SubmitAssignment (SubmitAssignmentViewModel model)
        {
            string path = AddAssFileToApiLocal(model);
            model.Path = path;

            using(var context = new Prn231ProjectContext())
            {
                try
                {
                    SubmitAssignment submit = new SubmitAssignment 
                    {
                        SubmitAssignmentName = model.SubmitAssignmentName,
                        UploaderId= model.UploaderId,
                        Path = model.Path,
                        AssignmentId= model.AssignmentId,
                        Description = model.Description
                    };
                    context.SubmitAssignments.Add(submit);
                    context.SaveChanges();

                }catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        }

        private static String AddAssFileToApiLocal(SubmitAssignmentViewModel model)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/AllFiles/SubmitAssignment");
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);
            FileInfo fileInfo= new FileInfo(model.SubmitFile.FileName);
            string fileName = model.UploaderId + "_" + model.AssignmentId + "_" + model.SubmitFile.FileName;
            string fileNameWithPath = Path.Combine(path, fileName);
            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                model.SubmitFile.CopyTo(stream);
            }

            return fileNameWithPath;
        }

        public static IEnumerable<SubmitAssignment> ListSubmitAssignmentByAssId(int assID)
        {
            List<SubmitAssignment> list = new List<SubmitAssignment>();
            try
            {
                using(var context = new Prn231ProjectContext())
                {
                    list = context.SubmitAssignments.Include(m => m.Uploader)
                        .Include(m => m.AssignmentId)
                        .Where(m => m.AssignmentId == assID).ToList(); 
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return list;
        }

        public static SubmitAssignment GetSubmitAssignmentById (int id)
        {
            SubmitAssignment submitAssignment = new SubmitAssignment(); 
            try
            {
                using(var context = new Prn231ProjectContext())
                {
                    submitAssignment = context.SubmitAssignments.Include(m => m.Uploader)
                        .Include(m => m.Assignment)
                        .Where(m => m.SubmitAssignmentId == id).SingleOrDefault();
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return submitAssignment;
        }
    }
}
