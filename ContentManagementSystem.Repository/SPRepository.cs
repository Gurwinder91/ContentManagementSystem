using ContentManagementSystem.Model;
using ContentManagementSystem.Shared;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagementSystem.Repository
{
   public class SPRepository: ISPRepository
    {
        private CMSDBEntities _entities;
        public SPRepository(CMSDBEntities entities)
        {
            this._entities = entities;
        }

        protected CMSDBEntities Context
        {
            get { return _entities; }
            set { _entities = value; }
        }

        /// <summary>
        /// Get Collection of data by Store procedure name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <returns></returns>
        public IQueryable<TElement> GetAll<TElement>(string spName) where TElement : class
        {
            return _entities.Database.SqlQuery<TElement>(spName.GetSPCmd()).AsQueryable();
        }

        /// <summary>
        /// Get data by Store procedure name and array of parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="sqlParametes"></param>
        /// <returns></returns>
        public IQueryable<TElement> GetAll<TElement>(string spName, SqlParameter[] sqlParametes) where TElement : class
        {
            return _entities.Database.SqlQuery<TElement>(spName.GetSPCmd(), sqlParametes).AsQueryable();
        }

        /// <summary>
        /// Get data by Store procedure name and parameter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="sqlParametes"></param>
        /// <returns></returns>
        public IQueryable<TElement> GetAll<TElement>(string spName, SqlParameter sqlParametes) where TElement : class
        {
            return _entities.Database.SqlQuery<TElement>(spName.GetSPCmd(), sqlParametes).AsQueryable();
        }


        /// <summary>
        /// Get Response as a int by Store procedure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <returns></returns>
        public TElement GetResponseAsInt<TElement>(string spName) where TElement : struct
        {
            return _entities.Database.SqlQuery<TElement>(spName).SingleOrDefault();
        }
        /// <summary>
        /// Get Response as a int by Store procedure name and array of parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="sqlParametes"></param>
        /// <returns></returns>
        public TElement GetResponseAsInt<TElement>(string spName, SqlParameter[] sqlParametes) where TElement : struct
        {
            return _entities.Database.SqlQuery<TElement>(spName.GetSPCmd(), sqlParametes).SingleOrDefault();
        }

        /// <summary>
        /// Get Response as a int by Store procedure name and parameter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="sqlParametes"></param>
        /// <returns></returns>
        public TElement GetResponseAsInt<TElement>(string spName, SqlParameter sqlParametes) where TElement : struct
        {
            return _entities.Database.SqlQuery<TElement>(spName.GetSPCmd(), sqlParametes).SingleOrDefault();
        }

    }
}
