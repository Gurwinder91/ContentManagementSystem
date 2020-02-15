using ContentManagementSystem.Model;
using ContentManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ContentManagementSystem.Local.CustomIdentity
{
    public class UserRepository<T> where T : IdentityUser
    {
        private readonly CMSDBEntities _databaseContext;
        private readonly ISPRepository _spRepository;
        public UserRepository(CMSDBEntities databaseContext, ISPRepository spRepository)
        {
            this._databaseContext = databaseContext;
            this._spRepository = spRepository;
        }

        internal T GeTByName(string userName)
        {
            return this._spRepository.GetAll<T>("GetUser @ColumnName, @Search", new SqlParameter[] { new SqlParameter("ColumnName", "UserName"), new SqlParameter("Search", userName) }).SingleOrDefault();   
        }


        internal T GeTByEmail(string email)
        {
            return this._spRepository.GetAll<T>("GetUser @ColumnName, @Search", new SqlParameter[] { new SqlParameter("ColumnName", "Email"), new SqlParameter("Search", email) }).SingleOrDefault();
        }

        internal int Insert(T user)
        {
            _databaseContext.CMSUsers.Add(new CMSUser
            {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                SecurityStamp = user.SecurityStamp,
                Email = user.Email,
                PasswordSet = user.PasswordSet,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                LockoutEnabled = user.LockoutEnabled,
                AccessFailedCount = user.AccessFailedCount
            });

            return _databaseContext.SaveChanges();
        }


        /// <summary>
        /// Returns an T given the user’s id
        /// </summary>
        /// <param name=”userId”>The user’s id</param>
        /// <returns></returns>
        public T GeTById(string userId)
        {
            return this._spRepository.GetAll<T>("GetUser @ColumnName, @Search", new SqlParameter[] { new SqlParameter("ColumnName", "Id"), new SqlParameter("Search", userId) }).SingleOrDefault();
        }

        /// <summary>
        /// Return the user’s password hash
        /// </summary>
        /// <param name=”userId”>The user’s id</param>
        /// <returns></returns>
        public string GetPasswordHash(string userId)
        {
            return this._spRepository.GetAll<string>("GetPasswordHash @Id", new SqlParameter("Id", userId)).SingleOrDefault();
        }

        /// <summary>
        /// Updates a user in the Users table
        /// </summary>
        /// <param name=”user”></param>
        /// <returns></returns>
        public int Update(T user)
        {
            var result = _databaseContext.CMSUsers.FirstOrDefault(u => u.Id == user.Id);
            if (result != null)
            {
                result.UserName = user.UserName;
                result.PasswordHash = user.PasswordHash;
                result.SecurityStamp = user.SecurityStamp;
                result.Email = result.Email;
                result.PasswordSet = user.PasswordSet;
                result.EmailConfirmed = user.EmailConfirmed;
                result.PhoneNumber = user.PhoneNumber;
                result.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                result.LockoutEnabled = user.LockoutEnabled;
                result.AccessFailedCount = user.AccessFailedCount;
                return _databaseContext.SaveChanges();
            }
            return 0;
        }
    }
}