using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContentManagementSystem.Local.CustomIdentity
{
    public class IdentityUser : IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, IUser, IUser<string>
    {
        public IdentityUser()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public IdentityUser(string UserName) : this()
        {
            this.UserName = UserName;
        }

        public Boolean PasswordSet { get; set; }
        public Boolean Block { get; set; }
    }
}