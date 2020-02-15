using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagementSystem.Repository
{
   public interface ISPRepository
    {
        /// <summary>
        /// Get Collection of data by Store procedure name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <returns></returns>
        IQueryable<TElement> GetAll<TElement>(string spName) where TElement : class;

        /// <summary>
        /// Get data by Store procedure name and array of parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="sqlParametes"></param>
        /// <returns></returns>
        IQueryable<TElement> GetAll<TElement>(string spName, SqlParameter[] sqlParametes) where TElement : class;

        /// <summary>
        /// Get data by Store procedure name and parameter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="sqlParametes"></param>
        /// <returns></returns>
        IQueryable<TElement> GetAll<TElement>(string spName, SqlParameter sqlParametes) where TElement : class;

        /// <summary>
        /// Get Response as a int by Store procedure name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <returns></returns>
        TElement GetResponseAsInt<TElement>(string spName) where TElement : struct;

        /// <summary>
        /// Get Response as a int by Store procedure name and array of parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="sqlParametes"></param>
        /// <returns></returns>
        TElement GetResponseAsInt<TElement>(string spName, SqlParameter[] sqlParametes) where TElement : struct;

        /// <summary>
        /// Get Response as a int by Store procedure name and parameter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="sqlParametes"></param>
        /// <returns></returns>
        TElement GetResponseAsInt<TElement>(string spName, SqlParameter sqlParametes) where TElement : struct;
    }
}
