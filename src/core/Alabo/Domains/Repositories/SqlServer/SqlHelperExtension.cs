using System.Collections.Generic;
using System.Data.SqlClient;

namespace Alabo.Domains.Repositories.SqlServer
{
    /// <summary>
    ///     SqlHelper��չ(����AutoMapper.dll)
    /// </summary>
    public sealed partial class SqlHelper
    {
        #region ʵ������

        public T ExecuteObject<T>(string commandText, params SqlParameter[] parms)
        {
            return ExecuteObject<T>(ConnectionString, commandText, parms);
        }

        public List<T> ExecuteObjects<T>(string commandText, params SqlParameter[] parms)
        {
            return ExecuteObjects<T>(ConnectionString, commandText, parms);
        }

        #endregion ʵ������

        #region ��̬����

        public static T ExecuteObject<T>(string connectionString, string commandText, params SqlParameter[] parms)
        {
            //DataTable dt = ExecuteDataTable(connectionString, commandText, parms);
            //return AutoMapper.Mapper.DynamicMap<List<T>>(dt.CreateDataReader()).FirstOrDefault();
            using (var reader = ExecuteDataReader(connectionString, commandText, parms))
            {
                // return AutoMapper.Mapper.DynamicMap<List<T>>(reader).FirstOrDefault();
            }

            return default;
        }

        public static List<T> ExecuteObjects<T>(string connectionString, string commandText,
            params SqlParameter[] parms)
        {
            //DataTable dt = ExecuteDataTable(connectionString, commandText, parms);
            //return AutoMapper.Mapper.DynamicMap<List<T>>(dt.CreateDataReader());
            using (var reader = ExecuteDataReader(connectionString, commandText, parms))
            {
                // return AutoMapper.Mapper.DynamicMap<List<T>>(reader);
                return null;
            }
        }

        #endregion ��̬����
    }
}