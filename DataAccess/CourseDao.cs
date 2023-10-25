using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CourseDao
    {
        public static List<Course> GetAllCourse()
        {
            List<Course> list = new List<Course>();
            try
            {

                using (var context = new Prn231ProjectContext())
                {
                    var courses = context.Courses.Include(a => a.Assignments).Include(u => u.Users).ToList();
                    foreach( var c in courses)
                    {
                        list.Add(c);
                    }
                }

            }
            catch(Exception ex)
            {
                 throw new Exception(ex.Message);
            }
            return list;
        }

        public static Course GetCourseById(int id)
        {
            Course course = new Course();
            try{
                using(var context = new Prn231ProjectContext()) 
                {
                    course = context.Courses
                        .Include(c => c.Assignments)
                        .Include(u => u.Users)
                        .Include(c => c.Materials)
                        .Where(c => c.CourseId== id).FirstOrDefault();
                    
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return course;
        }


        public static void SaveCourse(Course course) 
        {
            try
            {
                using(var context = new Prn231ProjectContext())
                {
                    context.Courses.Add(course);
                    context.SaveChanges();
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateCourse(Course course)
        {
            try
            {
                using (var context = new Prn231ProjectContext())
                {
                    context.Entry<Course>(course).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static void DeleteCourse(Course course)
        {
            try
            {
                using (var context = new Prn231ProjectContext())
                {
                    using(var transaction = context.Database.BeginTransaction())
                    {
                        var c = context.Courses.Include(c => c.Users).Include(c =>c.Assignments)
                            .Include(m => m.Materials).SingleOrDefault( c=> c.CourseId == course.CourseId );

                        var userCourse = c.Users.ToList();
                        foreach( var user in userCourse )
                        {
                            c.Users.Remove(user);
                        }

                        var assignmentCourse = context.Assignments.Where(a => a.CourseId == course.CourseId).ToList();
                        foreach ( var assignment in assignmentCourse )
                        {
                            context.Assignments.Remove(assignment);
                        } 
                        
                        var materialCouse = c.Materials.Where(a => a.CourseId == course.CourseId).ToList();
                        foreach(var mc  in materialCouse)
                        {
                            context.Materials.Remove(mc);
                        }

                        context.Courses.Remove(c);


                        if(context.SaveChanges() > 0)
                        {
                            transaction.Commit();
                        }

                    }
                } 

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static IEnumerable<Course> GetAllCourseByTeacherId (int teacherId)
        {
            List<Course> list = new List<Course>();
            try
            {
                using(var context = new Prn231ProjectContext())
                {
                    var user = context.Users.Where(u => u.UserId == teacherId).FirstOrDefault();
                    var courses = context.Courses.Include(a => a.Assignments).Include(u => u.Users).Where(u => u.Users.Contains(user)).ToList();
                    foreach(var c in courses) 
                    {
                        list.Add(c);
                    }
                }
                
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }


        public static IEnumerable<Course> GetAllCourseByStudentId(int teacherId)
        {
            List<Course> list = new List<Course>();
            try
            {
                using (var context = new Prn231ProjectContext())
                {
                    var user = context.Users.Where(u => u.UserId == teacherId).FirstOrDefault();
                    var courses = context.Courses.Include(a => a.Assignments).Include(u => u.Users).Where(u => u.Users.Contains(user)).ToList();
                    foreach (var c in courses)
                    {
                        list.Add(c);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

    }
}
