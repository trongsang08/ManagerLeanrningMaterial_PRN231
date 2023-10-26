using BusinessObjects.Models;
using DataAccess;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        public User checkLogin(string email, string password) => UserDao.CheckLogin(email, password);


        public void DeleteUser(User u) => UserDao.DeleteUser(u);
        

        public List<Role> GetAllRoles() => RoleDao.GetAllRole();
       

        public List<User> GetAllUsers() => UserDao.GetAllUser();
       
        public string GetRoleByEmail(string email) => UserDao.GetRoleByEmail(email);
        

        public Role GetRoleById(int rid) => RoleDao.FindRoleById(rid);
        

        public User GetUserByEmail(string email) => UserDao.GetUserByEmail(email);
        

        public User GetUserById(int uId) => UserDao.FindUserById(uId);
        

        public void InsertUser(User u) => UserDao.InsertUser(u);
        

        public void UpdateProduct(User u) => UserDao.UpdateUser(u);
        
    }
}
