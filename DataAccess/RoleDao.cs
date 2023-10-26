using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RoleDao
    {
        public static List<Role> GetAllRole()
        {
            List<Role> roles = new List<Role>();
            try
            {
                using(var context = new Prn231ProjectContext())
                {
                    roles = context.Roles.ToList();
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return roles;
        }

        public static Role FindRoleById(int roleId) 
        {
            Role role = new Role();
            try 
            {
                using(var context = new Prn231ProjectContext())
                {
                    role = context.Roles.SingleOrDefault(x => x.RoleId == roleId);
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return role;
        }
    }
}
