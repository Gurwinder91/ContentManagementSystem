using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContentManagementSystem.Local.CustomIdentity
{
    public class IdentityRole : IRole
    {
        public IdentityRole()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public IdentityRole(string Name) : this()
        {
            this.Name = Name;
        }

        public IdentityRole(string Name, string Id)
        {
            this.Name = Name;
            this.Id = Id;
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}