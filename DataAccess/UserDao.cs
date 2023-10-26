using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserDao
    {
        public static List<User> GetAllUser()
        {
            List<User> list = new List<User>();
            try
            {
                using(var context = new Prn231ProjectContext())
                {
                    list = context.Users.Include(x => x.Role).ToList();
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return list;
        }


        public static User FindUserById (int id)
        {
            User user = new User();
            try
            {
                using(var context = new Prn231ProjectContext())
                {
                    user = context.Users.Include(x => x.Role)
                        .FirstOrDefault(x => x.UserId==id);
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return user;
        }

        public static void InsertUser(User user)
        {
            try
            {
                using(var context= new Prn231ProjectContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateUser(User user)
        {
            try
            {
                using(var context = new Prn231ProjectContext())
                {
                    context.Entry<User>(user).State= EntityState.Modified;
                    context.SaveChanges();
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteUser(User user)
        {
            try
            {
                using(var context = new Prn231ProjectContext())
                {
                    //Todo: delete materials --> submitAssignment --> Assignments
                    var p1 = context.Users.SingleOrDefault(c=>c.UserId==user.UserId);
                    var materials = context.Materials.Where(c => c.Uploader.UserId == p1.UserId).ToList();
                    var assignment = context.Assignments.Where(c => c.Uploader.UserId == p1.UserId).ToList();
                    //1. delete materials
                    context.Materials.RemoveRange(materials);
                    //2. delete submitAssignments of Ass
                    foreach (var ass in assignment)
                    {
                        //delete assignments file
                        if (File.Exists(ass.Path))
                        {
                            File.Delete(ass.Path);
                        }
                        var subAss = context.SubmitAssignments.Where(a => a.AssignmentId == ass.AssignmentId).ToList();
                        foreach(var sub in subAss)
                        {
                            //delete SubmitAssingment file
                            if(File.Exists(sub.Path))
                            {
                                File.Delete(sub.Path);
                            }
                        }
                        context.SubmitAssignments.RemoveRange(subAss);
                    }
                    //3. delete ass
                    context.Assignments.RemoveRange(assignment);
                    //4.delete user course
                    var c = context.Users.Include(c => c.Courses).SingleOrDefault(a => a.UserId == user.UserId);
                    var userCoure = context.Courses.ToList();
                    foreach(var uc in userCoure)
                    {
                        c.Courses.Remove(uc);
                    }
                    //5.Delete submitAss of User
                    var subAssignments = context.SubmitAssignments.Where(c => c.Uploader.UserId == p1.UserId).ToList();
                    context.SubmitAssignments.RemoveRange(subAssignments);

                    //6. delete User
                    context.Users.Remove(p1);
                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public static User CheckLogin(string email, string password)
        {
            User user = new User();
            try
            {
                using(var context = new Prn231ProjectContext())
                {
                    user = context.Users.Where(u => u.Password.Equals(password) && u.Email.Equals(email)).FirstOrDefault();
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return user;
        }

        public static string GetRoleByEmail(string email)
        {
            string role = "";

            try
            {
                using(var context = new Prn231ProjectContext())
                {
                    role = context.Users.Include(u => u.Role)
                        .Where(u => u.Email.Equals(email)).Select(u => u.Role.RoleName).FirstOrDefault();
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return role;
        }

        public static User GetUserByEmail(string email)
        {
            User user = new User();
            try
            {
                using(var context = new Prn231ProjectContext())
                {
                    user = context.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return user;
        }
    }
}
