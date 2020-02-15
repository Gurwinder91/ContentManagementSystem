using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using ContentManagementSystem.Model;
using ContentManagementSystem.Repository;

namespace ContentManagementSystem.Local.CustomIdentity
{
    public class UserStore<T> : IUserRoleStore<T>, 
        IUserStore<T>,
        IUserPasswordStore<T>,
        IUserEmailStore<T>,
        IUserLockoutStore<T, string>,
        IUserTwoFactorStore<T, string> where T : IdentityUser
    {

        private readonly UserRepository<T> _userTable;
        private readonly UserRolesRepository _userRolesTable;
        private readonly ISPRepository _spRepository;

        public UserStore(CMSDBEntities databaseContext, ISPRepository spRepository)
        {
            _userTable = new UserRepository<T>(databaseContext, spRepository);
            _userRolesTable = new UserRolesRepository(databaseContext);
        }

        public Task AddToRoleAsync(T user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.Run(() => _userRolesTable.AssignRole(user.Id, roleName));
        }

        public Task CreateAsync(T user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.Run(() => _userTable.Insert(user));
        }

        public Task DeleteAsync(T user)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("Null or empty argument: userId");
            }

            return Task.Run(() => _userTable.GeTById(userId));
        }

        public Task<T> FindByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Null or empty argument: userName");
            }

            return Task.Run(() => _userTable.GeTByName(userName));
        }

        public Task<IList<string>> GetRolesAsync(T user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.Run(() => _userRolesTable.FindByUserId(user.Id));
        }

        public Task<bool> IsInRoleAsync(T user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(T user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.Run(() => _userTable.Update(user));
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UserStore() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        Task IUserPasswordStore<T, string>.SetPasswordHashAsync(T user, string passwordHash)
        {
            return Task.Run(() => user.PasswordHash = passwordHash);
        }

        Task<string> IUserPasswordStore<T, string>.GetPasswordHashAsync(T user)
        {
            return Task.Run(() => _userTable.GetPasswordHash(user.Id));
        }

        Task<bool> IUserPasswordStore<T, string>.HasPasswordAsync(T user)
        {
            throw new NotImplementedException();
        }

        Task IUserStore<T, string>.CreateAsync(T user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.Run(() => _userTable.Insert(user));
        }

        Task IUserStore<T, string>.UpdateAsync(T user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.Run(() => _userTable.Update(user));
        }

        Task IUserStore<T, string>.DeleteAsync(T user)
        {
            throw new NotImplementedException();
        }

        Task<T> IUserStore<T, string>.FindByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("Null or empty argument: userId");
            }

            return Task.Run(() => _userTable.GeTById(userId));
        }

        Task<T> IUserStore<T, string>.FindByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Null or empty argument: userName");
            }

            return Task.Run(() => _userTable.GeTByName(userName));
        }

        void IDisposable.Dispose()
        {
           // throw new NotImplementedException();
        }

        Task IUserEmailStore<T, string>.SetEmailAsync(T user, string email)
        {
            throw new NotImplementedException();
        }

        Task<string> IUserEmailStore<T, string>.GetEmailAsync(T user)
        {
            return Task.FromResult(user.Email);
        }

        Task<bool> IUserEmailStore<T, string>.GetEmailConfirmedAsync(T user)
        {
            throw new NotImplementedException();
        }

        Task IUserEmailStore<T, string>.SetEmailConfirmedAsync(T user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        Task<T> IUserEmailStore<T, string>.FindByEmailAsync(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("email");
            }

            return Task.Run(() => _userTable.GeTByEmail(email));
        }

        Task<DateTimeOffset> IUserLockoutStore<T, string>.GetLockoutEndDateAsync(T user)
        {
            return
                Task.FromResult(user.LockoutEndDateUtc.HasValue
                ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc))
                : new DateTimeOffset());
        }

        Task IUserLockoutStore<T, string>.SetLockoutEndDateAsync(T user, DateTimeOffset lockoutEnd)
        {
            throw new NotImplementedException();
        }

        Task<int> IUserLockoutStore<T, string>.IncrementAccessFailedCountAsync(T user)
        {
            throw new NotImplementedException();
        }

        Task IUserLockoutStore<T, string>.ResetAccessFailedCountAsync(T user)
        {
            throw new NotImplementedException();
        }

        Task<int> IUserLockoutStore<T, string>.GetAccessFailedCountAsync(T user)
        {
            return Task.FromResult(user.AccessFailedCount);
        }

        Task<bool> IUserLockoutStore<T, string>.GetLockoutEnabledAsync(T user)
        {
            return Task.FromResult(user.LockoutEnabled);
        }

        Task IUserLockoutStore<T, string>.SetLockoutEnabledAsync(T user, bool enabled)
        {
            user.LockoutEnabled = enabled;

            return Task.Run(() => _userTable.Update(user));
        }

        Task IUserTwoFactorStore<T, string>.SetTwoFactorEnabledAsync(T user, bool enabled)
        {
            throw new NotImplementedException();
        }

        Task<bool> IUserTwoFactorStore<T, string>.GetTwoFactorEnabledAsync(T user)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }
        #endregion

    }
}