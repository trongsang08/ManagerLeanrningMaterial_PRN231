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
    public class AssignmentDao
    {
        public static void SaveAssignment(UploadAssignmentViewModel model)
        {
            string path = AddAssFileToApiLocal(model);
            model.Path= path;// add record to db

            //data access
            using(var context = new Prn231ProjectContext())
            {
                try
                {
                    Assignment ass = new Assignment
                    {
                        CourseId = model.CourseId,
                        UploaderId = model.UploaderId,
                        Path = model.Path,
                        AssignmentName = model.AssignmentName.Split("-")[0],
                        RequiredDate = model.RequiredDate,
                    };
                    context.Assignments.Add(ass);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        }

        private static String AddAssFileToApiLocal(UploadAssignmentViewModel model)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/AllFiles/Assigments");
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string x = model.Assignment.FileName.Split('-')[0];
            string fileName= model.UploaderId + "-" + model.CourseId+ "-" + x;
            string fileNameWithPath = Path.Combine(path, fileName);


            using(var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                model.Assignment.CopyTo(stream);
            }
            return fileNameWithPath;
        }

        public static IEnumerable<Assignment> GetAssignmentsByCourseId(int courseId)
        {
            List<Assignment> list = new List<Assignment>();
            try
            {
                using(var context = new Prn231ProjectContext())
                {
                    list = context.Assignments.Include(m => m.CourseId)
                        .Include(m => m.Uploader).Where(a => a.CourseId == courseId).ToList();
                }

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return list;
        }


        public static IEnumerable<Assignment> ListAssignmentByTeacherAndCourse(int teacherId,int courseId)
        {
            List<Assignment> list = new List<Assignment>();
            try
            {
                using (var context = new Prn231ProjectContext())
                {
                    list = context.Assignments.Include(m => m.Course)
                        .Include(m => m.Uploader).Where(a => a.UploaderId==teacherId && a.CourseId == courseId).ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return list;
        }

        public static Assignment GetAssignmentsByAssId(int assId)
        {
            Assignment assignment = new Assignment();
            try
            {
                using(var context = new Prn231ProjectContext())
                {
                    assignment = context.Assignments.Include(m => m.Course).Include(m => m.UploaderId)
                        .Where(a => a.AssignmentId==assId).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return assignment;
        }

    }
}
