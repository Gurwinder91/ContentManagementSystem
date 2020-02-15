using ContentManagementSystem.Model;
using ContentManagementSystem.Local.CustomIdentity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace ContentManagementSystem.Local.Models
{
    public class IdentityModel
    {
        public class ApplicationUser : IdentityUser //*/Microsoft.AspNet.Identity.EntityFramework.IdentityUser
        {
            public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
            {
                // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
                var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

                return userIdentity;
            }
        }

        public class ApplicationDbContext : CMSDBEntities//IdentityDbContext<ApplicationUser>
        {
            private static string _connectionName;

            public ApplicationDbContext(string connectionName)
            {
                _connectionName = connectionName;
            }

            public static ApplicationDbContext Create()
            {
                return new ApplicationDbContext("DefaultConnection");
            }
        }
    }
}