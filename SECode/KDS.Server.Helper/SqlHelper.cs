using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;

/***********************************************************
 * 功能：数据层访问
 * 说明： 
 *       1.整合了ADO 2.0及微软的SqlHelper
 *       
 * 创建：胡海明,huhaiming@gmail.com
 * 时间：2008/08
  ************************************************************/

namespace KDS.Server.Helper
{
    public class SqlHelper
    {        
        public SqlHelper()
        {

        }


        #region  ExecuteNonQuery
        /// <summary>
        /// 执行命令返回影响的记录数,huhm2008
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteNonQuery(string strSql)
        {    
            return ExecuteNonQuery(strSql,-1);
            
        }

        /// <summary>
        /// 执行命令返回影响的记录数(设置等待时间避免查询超时),huhm2008
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="intTimeout">超时秒数</param>
        /// <returns>影响记录条数</returns>
         public static int ExecuteNonQuery(string strSql,int intTimeout)
         {           
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            
            if (intTimeout>0)
                dbCommand.CommandTimeout = intTimeout;

            return db.ExecuteNonQuery(dbCommand);
         }

         /// <summary>
         /// 执行命令返回影响的记录数
         /// </summary>
         /// <param name="strSql">SQL语句</param>
         /// <returns>影响的记录数</returns>
         public static int ExecuteNonQuery(string strSql, params SqlParameter[]  cmdParms)
         {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            BuildDBParameter(db, dbCommand, cmdParms);    
            return db.ExecuteNonQuery(dbCommand);
         }
         
         /// <summary>
         /// 带事务的执行查询,huhm2008
         /// </summary>
         /// <param name="SQLStringList">多条SQL语句</param>     
         public static void ExecuteNonQueryWithTran(ArrayList SQLStringList)
         {
 
            Database db = DatabaseFactory.CreateDatabase();
            using (DbConnection dbconn = db.CreateConnection())
            { 
                dbconn.Open();
                DbTransaction dbtran = dbconn.BeginTransaction();
                try
                {
                    //执行语句
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            DbCommand dbCommand = db.GetSqlStringCommand(strsql);
                            db.ExecuteNonQuery(dbCommand);
                        }
                    }
                    dbtran.Commit();
                }
                catch (Exception ex)
                {
                    dbtran.Rollback();
                    throw ex;
                }
                finally
                {
                    dbconn.Close();
                }
            }
        }
 

         /// <summary>
         /// 带事务的执行多条SQL语句,huhm2008
         /// </summary>
         /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的DbParameter[]）</param>
         public static void ExecuteNonQueryWithTran(Hashtable SQLStringList)
         {
            Database db = DatabaseFactory.CreateDatabase();
            using (DbConnection dbconn = db.CreateConnection())
            {
                dbconn.Open();
                DbTransaction dbtran = dbconn.BeginTransaction();
                try
                {
                    //执行语句
                    foreach (DictionaryEntry myDE in SQLStringList)
                    {
                        string strsql = myDE.Key.ToString();
                        SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;                        
                        if (strsql.Trim().Length > 1)
                        {
                            DbCommand dbCommand = db.GetSqlStringCommand(strsql);
                            BuildDBParameter(db, dbCommand, cmdParms);    
                            db.ExecuteNonQuery(dbCommand);
                        }
                    }
                    dbtran.Commit();
                }
                catch
                {
                    dbtran.Rollback();
                }
                finally
                {
                    dbconn.Close();
                }
            }
        }

        #endregion 


        #region ExecuteScalar
        /// <summary>
        /// 执行命令返回单一查询结果,huhm2008
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>查询结果（object）</returns>
        public static object ExecuteScalar(string strSql)
        {            
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            object obj = db.ExecuteScalar(dbCommand);
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                return null;
            }
            else
            {
                return obj;
            }      
        }
         
         /// <summary>
         /// 执行命令返回单一查询结果,huhm2008
         /// </summary>
         /// <param name="strSql">SQL语句</param>
         /// <returns>查询结果（object）</returns>
         public static object ExecuteScalar(string strSql,params SqlParameter[]  cmdParms)
         {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            BuildDBParameter(db, dbCommand, cmdParms);    
            object obj = db.ExecuteScalar(dbCommand);
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                return null;
            }
            else
            {
                return obj;
            }      
         }
        
        #endregion

        #region ExecuteReader

        /// <summary>
        /// 执行命令返回SqlDataReader( 注意：使用后必须显示SqlDataReader.Close() ),huhm2008
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string strSql)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            SqlDataReader dr = (SqlDataReader)db.ExecuteReader(dbCommand);
            return dr;      
        }        
         

        /// <summary>
        /// 执行命令返回SqlDataReader( 注意：使用后必须显示SqlDataReader.Close() ),huhm2008
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string strSql,params SqlParameter[]  cmdParms)
        {        
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            BuildDBParameter(db, dbCommand, cmdParms);
            SqlDataReader dr = (SqlDataReader)db.ExecuteReader(dbCommand);
            return dr;
        }   

        #endregion


        #region ExecuteDataSet
        /// <summary>
        /// 执行命令返回DataSet,huhm2008
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <returns>DataSet</returns>
         public static DataSet ExecuteDataSet(string strSql)
         {            
            return ExecuteDataSet(strSql,-1);
         }

         /// <summary>
         /// 执行命令返回DataSet,huhm2008
         /// </summary>
         /// <param name="strSql">命令</param>
         /// <param name="intTimeOut">超时秒数</param>
         /// <returns>DataSet</returns>
         public static DataSet ExecuteDataSet(string strSql,int intTimeOut)
         {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);

            if (intTimeOut>0)
                dbCommand.CommandTimeout = intTimeOut;

            return db.ExecuteDataSet(dbCommand);
         }
 
         /// <summary>
         /// 执行命令返回DataSet,huhm2008
         /// </summary>
         /// <param name="strSql">查询语句</param>
         /// <returns>DataSet</returns>
         public static DataSet ExecuteDataSet(string strSql,params SqlParameter[]  cmdParms)
         {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            BuildDBParameter(db, dbCommand, cmdParms);   
            return db.ExecuteDataSet(dbCommand);
         }

        #endregion
 
        
        #region ExecuteByProc
 

        /// <summary>
        /// 执行存储过程返回影响的行数,huhm2008      
        /// </summary>  
        /// <param name="storedProcName">存储过程名</param>
         public static int ExecuteNonQueryByProc(string storedProcName)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(storedProcName);
            return db.ExecuteNonQuery(dbCommand);
        }
 
        /// <summary>
        /// 执行存储过程返回输出参数的值和影响的行数,huhm2008    
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="OutParameter">输出参数名称</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns>Object</returns>
         public static object ExecuteNonQueryByProc(string storedProcName, SqlParameter[] InParameters, SqlParameter OutParameter, int rowsAffected)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(storedProcName);
            BuildDBParameter(db, dbCommand, (SqlParameter[])InParameters);
            db.AddOutParameter(dbCommand, OutParameter.ParameterName, OutParameter.DbType, OutParameter.Size);
            rowsAffected = db.ExecuteNonQuery(dbCommand);
            return db.GetParameterValue(dbCommand,"@" + OutParameter.ParameterName);  //得到输出参数的值
        }
 

         /// <summary>
         /// 执行存储过程返回SqlDataReader( 注意：使用后必须SqlDataReader.Close()),huhm2008
         /// </summary>
         /// <param name="storedProcName">存储过程名</param>
         /// <param name="parameters">存储过程参数</param>
         /// <returns>SqlDataReader</returns>
         public static SqlDataReader ExecuteReaderByProc(string storedProcName, SqlParameter[]  parameters)
         {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(storedProcName, parameters);            
            //BuildDBParameter(db, dbCommand, parameters);
            return (SqlDataReader)db.ExecuteReader(dbCommand);           
         }
                   

         /// <summary>
         /// 执行存储过程返回DataSet,huhm2008
         /// </summary>
         /// <param name="storedProcName">存储过程名</param>
         /// <param name="parameters">存储过程参数</param>
         /// <param name="tableName">DataSet结果中的表名</param>
         /// <returns>DataSet</returns>
         public static DataSet ExecuteDataSetByProc(string storedProcName, SqlParameter[]  parameters)
         {           
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(storedProcName, parameters);
            //BuildDBParameter(db, dbCommand, parameters);
            return db.ExecuteDataSet(dbCommand); 
         }

         /// <summary>
         /// 执行存储过程返回DataSet,huhm2008
         /// </summary>
         /// <param name="storedProcName">存储过程名</param>
         /// <param name="parameters">输入参数</param>
         /// <param name="intTimeOut">以秒计的超时时间</param>
         /// <returns></returns>
         public static DataSet ExecuteDataSetByProc(string storedProcName, SqlParameter[]  parameters, int intTimeOut)
         {           
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(storedProcName, parameters);
            dbCommand.CommandTimeout = intTimeOut;
            //BuildDBParameter(db, dbCommand, parameters);
            return db.ExecuteDataSet(dbCommand); 
         }
 
        #endregion    
 
        #region UpdateDataSet
        

        #endregion 

        #region 常用方法

         /// <summary>
        /// 获取表字段的最大值,huhm2008
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="FieldName">字段名（应是整型）</param>
        /// <returns>true/false</returns>
        public static int GetTableMaxID(string TableName,string FieldName)
        {
            string strSql = "select max(" + FieldName + ")+1 from " + TableName;
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);           
            object obj = db.ExecuteScalar(dbCommand);
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }           
        }

        //// <summary>
        /// 检测一个记录是否存在,huhm2008
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
         public static bool ExistsData(string strSql)
         {
            return ExistsData(strSql,null);
         }

        /// <summary>
        ///  检测一个记录是否存在,huhm2008
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="cmdParms"></param>
        /// <returns>true/false</returns>
        public static bool ExistsData(string strSql, params SqlParameter[]  cmdParms)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);

            if ((cmdParms!=null) && (cmdParms.Length>0))
                BuildDBParameter(db, dbCommand, cmdParms); 
           
            object obj = db.ExecuteScalar(dbCommand);           
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        /// <summary>
        /// 加载参数
        /// </summary>
        public static void BuildDBParameter(Database db, DbCommand dbCommand, params SqlParameter[]  cmdParms)
        {
            foreach (DbParameter sp in cmdParms)
            {
                db.AddInParameter(dbCommand, sp.ParameterName, sp.DbType,sp.Value);
            }
        }

        #endregion

        #region 私有方法内部使用

        private static void PrepareCommand(SqlCommand cmd,SqlConnection conn,SqlTransaction trans, string cmdText, SqlParameter[]  cmdParms) 
        {
              if (conn.State != ConnectionState.Open)
                   conn.Open();
              cmd.Connection = conn;
              cmd.CommandText = cmdText;
              if (trans != null)
                   cmd.Transaction = trans;
              cmd.CommandType = CommandType.Text;//cmdType;
              if (cmdParms != null) 
              {
                foreach (DbParameter parameter in cmdParms)
                   {
                       if ( ( parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input ) && 
                            (parameter.Value == null))
                       {
                            parameter.Value = DBNull.Value;
                       }
                       cmd.Parameters.Add(parameter);
                   }
              }
        }

        #endregion

     }
}
