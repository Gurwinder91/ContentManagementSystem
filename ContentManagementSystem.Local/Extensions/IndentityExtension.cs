using ContentManagementSystem.Model;
using ContentManagementSystem.Repository;
using ContentManagementSystem.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ContentManagementSystem.Local.Extensions
{
    public static class IndentityExtension
    {
        private static ISPRepository _spRepository;

        public static void Init(ISPRepository spRepository)
        {
            _spRepository = spRepository;
        }
        public static Author_DTO GetUserProfile(this IIdentity identity)
        {
            string userId = identity.GetUserId();
            return _spRepository.GetAll<Author_DTO>("GetAuthorByUserID @UserID", new SqlParameter("UserID", userId)).FirstOrDefault();
        }
    }
}