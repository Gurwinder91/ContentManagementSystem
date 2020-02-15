using ContentManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContentManagementSystem.Local.CustomIdentity
{
    public class UserRolesRepository
    {

        private readonly CMSDBEntities _databaseContext;

        public UserRolesRepository(CMSDBEntities databaseContext)
        {
            _databaseContext = databaseContext;
        }

        /// <summary>
        /// Return List of User Roles
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<String> FindByUserId(string userId)
        {
            return _databaseContext.CMSUsers.Where(w => w.Id == userId).SelectMany(s => s.CMSRoles).Select(r => r.Name).ToList();
        }


        /// <summary>
        /// Add User Role
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        public int AssignRole(string userId, string RoleName)
        {
            CMSRole role = _databaseContext.CMSRoles.Where(x => x.Name == RoleName).First();
            var CMSUser = _databaseContext.CMSUsers.Where(x => x.Id == userId).First();
            CMSUser.CMSRoles.Add(role);
            return _databaseContext.SaveChanges();
        }
    }
}