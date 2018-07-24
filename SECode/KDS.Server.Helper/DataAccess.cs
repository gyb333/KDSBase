using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Transactions;
using System.Data.Common;

/***********************************************************
 * 功能：数据层访问2
 * 说明： 
 *       
 * 创建：胡海明,huhaiming@gmail.com
 * 时间：2008/08
  ************************************************************/

namespace KDS.Server.Helper
{
    /// <summary>
    /// 数据层访问类（与数据库交互）
    /// huhm,2008
    /// </summary>
    public sealed class DataAccess
    {
        /// <summary>
        /// 执行超时时间，秒数（默认0，即连接的默认值）
        /// </summary>
        public int CommandTimeOut = 0;

        /// <summary>
        /// 数据库连接Configuration KeyName
        /// </summary>
        public string ConnectionKey = "DefaultConn";

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataAccess()
        {

        }

        #region ExecuteScalar
        /// <summary>
        /// 执行命令返回单一查询结果,huhm2008
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>查询结果（object）</returns>
        public object ExecuteScalar(string strSql)
        {
            return ExecuteScalar(strSql, null);
        }

        /// <summary>
        /// 执行命令返回单一查询结果,huhm2008
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="cmdParms">参数列表及值。此处不支持动态SQL条件参数</param>
        /// <returns>查询结果（object）</returns>
        public object ExecuteScalar(string strSql, DbParameter[] cmdParms)
        {
            Database db = DatabaseFactory.CreateDatabase(ConnectionKey);
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            if (CommandTimeOut > 0)
                dbCommand.CommandTimeout = CommandTimeOut;

            DataAccess.BuildDbParameter(db, dbCommand, cmdParms);

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
        /// 执行命令返回IDataReader,huhm2008
        /// ( 警告：请及时关闭dataReader对像。使用using (IDataReader dataReader = db.ExecuteReader(dbCommand)) 或 显示IDataReader.Close() )
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public IDataReader ExecuteReader(string strSql)
        {
            return ExecuteReader(strSql, null);
        }        
         

        /// <summary>
        /// ( 警告：请及时关闭dataReader对像。使用using (IDataReader dataReader = db.ExecuteReader(dbCommand)) 或 显示IDataReader.Close() )
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <param name="cmdParms">参数列表及值。此处不支持动态SQL条件参数</param>
        /// <returns>SqlDataReader</returns>
        public IDataReader ExecuteReader(string strSql, DbParameter[] cmdParms)
        {        
            Database db = DatabaseFactory.CreateDatabase(ConnectionKey);
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            if (CommandTimeOut > 0)
                dbCommand.CommandTimeout = CommandTimeOut;
            
            DataAccess.BuildDbParameter(db, dbCommand, cmdParms);
            IDataReader dr = db.ExecuteReader(dbCommand);

            return dr;
        }   

        #endregion


        #region ExecuteDataSet

        /// <summary>
        /// 执行命令返回DataSet,huhm2008
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(string strSql)
        {
            return ExecuteDataSet(strSql, null);
        }

        /// <summary>
        /// 执行命令返回DataSet,huhm2008
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(string strSql, DbParameter[] cmdParms)
        {
            Database db = DatabaseFactory.CreateDatabase(ConnectionKey);
            DbCommand dbCommand = db.GetSqlStringCommand(strSql);
            if (CommandTimeOut > 0)
                dbCommand.CommandTimeout = CommandTimeOut;

            DataAccess.BuildDbParameter(db, dbCommand, cmdParms);
            
            return db.ExecuteDataSet(dbCommand);
        }

        #endregion 



        
        #region ExecuteNoQuery


        /// <summary>
        /// 执行SQL命令
        /// </summary>
        /// <param name="sqlCmd">存储过程名或SQL语句</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="transScopeOption">事务选项</param>
        /// <returns>返回受影响记录的行数或存储过程的返回值</returns>
        public int ExecuteNonQuery(string sqlCmd, CommandType commandType, TransactionScopeOption transScopeOption)
        {
            return ExecuteNonQuery(sqlCmd, commandType, transScopeOption);
        }

        /// <summary>
        /// 执行SQL命令
        /// </summary>
        /// <param name="sqlCmd">存储过程名或SQL语句</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="transScopeOption">事务选项</param>
        /// <param name="dbParas">参数列表及值：支持输入、输出参数。此处不支持动态SQL条件参数</param>
        /// <returns>返回受影响记录的行数或存储过程的返回值</returns>
        public int ExecuteNonQuery(string sqlCmd, CommandType commandType, TransactionScopeOption transScopeOption, DbParameter[] dbParas)
        {
            int retVal = -1;

            try
            {
                TransactionOptions option = new TransactionOptions();
                option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(transScopeOption, option))
                {
                    Database db = DatabaseFactory.CreateDatabase(ConnectionKey);

                    //Command属性
                    DbCommand dbCommand;
                    if (commandType == CommandType.StoredProcedure)
                        dbCommand = db.GetStoredProcCommand(sqlCmd);
                    else
                        dbCommand = db.GetSqlStringCommand(sqlCmd);
                    if (CommandTimeOut > 0)
                        dbCommand.CommandTimeout = CommandTimeOut;

                    //参数
                    if (dbParas != null)
                    {
                        foreach (DbParameter para in dbParas)
                        {
                            if (para.Direction == ParameterDirection.Input)
                                db.AddInParameter(dbCommand, para.ParameterName, para.DbType, para.Value);
                            else
                                db.AddOutParameter(dbCommand, para.ParameterName, para.DbType, para.Size);
                        }
                    }

                    retVal = db.ExecuteNonQuery(dbCommand);
                    scope.Complete();

                    //参数返回值
                    if (dbParas != null)
                    {
                        foreach (DbParameter para in dbParas)
                        {
                            if (para.Direction == ParameterDirection.Output)
                                para.Value = db.GetParameterValue(dbCommand, para.ParameterName);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

            return retVal;
        }


        /// <summary>
        /// 执行SQL命令
        /// </summary>
        /// <param name="sqlCmd">存储过程名或SQL语句</param>
        /// <param name="commandType">命令类型</param>
        /// <returns>返回受影响记录的行数或存储过程的返回值</returns>
        public int ExecuteNonQuery(string sqlCmd, CommandType commandType)
        {
            return ExecuteNonQuery(sqlCmd, commandType, null);
        }

        /// <summary>
        /// 执行SQL命令
        /// </summary>
        /// <param name="sqlCmd">存储过程名或SQL语句</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="dbParas">参数列表及值：支持输入、输出参数。此处不支持动态SQL条件参数</param>
        /// <returns>返回受影响记录的行数或存储过程的返回值</returns>
        public int ExecuteNonQuery(string sqlCmd, CommandType commandType, DbParameter[] dbParas)
        {
            int retVal = -1;

            try
            {
                Database db = DatabaseFactory.CreateDatabase(ConnectionKey);

                //Command属性
                DbCommand dbCommand;
                if (commandType == CommandType.StoredProcedure)
                    dbCommand = db.GetStoredProcCommand(sqlCmd);
                else
                    dbCommand = db.GetSqlStringCommand(sqlCmd);
                if (CommandTimeOut > 0)
                    dbCommand.CommandTimeout = CommandTimeOut;

                //参数
                if (dbParas != null)
                {
                    foreach (DbParameter para in dbParas)
                    {
                        if (para.Direction == ParameterDirection.Input)
                            db.AddInParameter(dbCommand, para.ParameterName, para.DbType, para.Value);
                        else
                            db.AddOutParameter(dbCommand, para.ParameterName, para.DbType, para.Size);
                    }
                }

                retVal = db.ExecuteNonQuery(dbCommand);

                //参数返回值
                if (dbParas != null)
                {
                    foreach (DbParameter para in dbParas)
                    {
                        if (para.Direction == ParameterDirection.Output)
                            para.Value = db.GetParameterValue(dbCommand, para.ParameterName);
                    }
                }

            }
            catch
            {
                throw;
            }

            return retVal;
        }

        #endregion


        #region OTHER

        /// <summary>
        /// 为DbCommand构建参数及设置参数值（参数名不支持表别名）,huhm2008
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbCommand"></param>
        /// <param name="dbParas"></param>
        public static void BuildDbParameter(Database db, DbCommand dbCommand, DbParameter[] dbParas)
        {
            if (dbParas != null)
            {
                foreach (DbParameter para in dbParas)
                {
                    db.AddInParameter(dbCommand, para.ParameterName, para.DbType, para.Value);
                }
            }
        }


        /// <summary>
        /// 将带有别名的字段名转换成物理表的字段名（即去掉别名）
        /// </summary>
        /// <param name="aliasFieldName"></param>
        /// <returns></returns>
        public static string TransToColumnName(string aliasFieldName)
        {
            int pos = aliasFieldName.IndexOf(".");
            if (pos >= 0)
                return aliasFieldName.Substring(pos + 1);
            else
                return aliasFieldName;
        }

        #endregion 






        //---------------------------------------------------------专用数据检索语句，支持动态参数----------------------------------------------//


        #region GetData（支持动态参数）

        /// <summary>
        /// 根据SQL语句及条件检索数据 by huhm/huhaiming@gmail.com,2001~2008
        /// </summary>
        /// <param name="selectCmd">SQL语句，可包含JOIN及WHERE（分页时必须以SELECT语句开始，统计页码时将用Count(*)替换FROM关键字前的所有语句）</param>
        /// <param name="selectCmdEx">SQL语句扩展，ORDER BY 子句（有分页时，此应为""，将排序字段填入orderByFieldName参数中）</param>
        /// <param name="dbParas">参数列表及值。参数名支持表别名；并支持动态参数：即根据参数名构造SQL条件</param>
        /// <param name="orderByFieldName">排序字段</param>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">页大小（每页记录数），若为0则不执行分页</param>
        /// <param name="totalPage">总页码，传入-1时系统会计算并返回总页码</param>
        /// <returns></returns>
        public DataTable GetDataTable(string selectCmd, string selectCmdEx, DbParameter[] dbParas, string orderByFieldName, int pageNo, int pageSize, ref int totalPage)
        {
            try
            {
                return GetDataSet(selectCmd, selectCmdEx, dbParas, orderByFieldName, pageNo, pageSize, ref totalPage).Tables[0];
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 根据SQL语句及条件检索数据 By huhm/huhaiming@gmail.com,2001~2008
        /// </summary>
        /// <param name="selectCmd">SQL语句，可包含JOIN及WHERE</param>
        /// <param name="selectCmdEx">SQL语句扩展，ORDER BY 子句</param>
        /// <param name="dbParas">参数列表及值。参数名支持表别名；并支持动态参数：即根据参数名构造SQL条件</param>
        /// <returns></returns>
        public DataTable GetDataTable(string selectCmd, string selectCmdEx, DbParameter[] dbParas)
        {
            try
            {
                int totalPage = -1;

                return GetDataSet(selectCmd, selectCmdEx, dbParas, "", 1, 0, ref totalPage).Tables[0];
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 根据SQL语句及条件检索数据 By huhm/huhaiming@gmail.com,2001~2008
        /// </summary>
        /// <param name="selectCmd">SQL语句，可包含JOIN及WHERE</param>
        /// <param name="selectCmdEx">SQL语句扩展，ORDER BY 子句</param>
        /// <param name="dbParas">参数列表及值。参数名支持表别名；并支持动态参数：即根据参数名构造SQL条件</param>
        /// <returns></returns>
        public DataSet GetDataSet(string selectCmd, string selectCmdEx, DbParameter[] dbParas)
        {
            try
            {
                int totalPage = -1;

                return GetDataSet(selectCmd, selectCmdEx, dbParas, "", 1, 0, ref totalPage);
            }
            catch
            {
                throw;
            }
        }



        /// <summary>
        /// 根据SQL语句及条件检索数据 By huhm/huhaiming@gmail.com,2001~2008
        /// </summary>
        /// <param name="selectCmd">SQL语句，可包含JOIN及WHERE（分页时必须以SELECT语句开始，统计页码时将用Count(1)替换FROM关键字前的所有语句）</param>
        /// <param name="selectCmdEx">SQL语句扩展，ORDER BY 子句（有分页时，此应为""，将排序字段填入orderByFieldName参数中）</param>
        /// <param name="dbParas">参数列表及值。参数名支持表别名；并支持动态参数：即根据参数名构造SQL条件(如果SelectCmd有占位符@@ParaWhere则在此占位符里替换并生成参数)</param>
        /// <param name="orderByFieldName">排序字段</param>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">页大小（每页记录数），若为0则不执行分页</param>
        /// <param name="totalPage">总页码，传入-1时系统会计算并返回总页码</param>
        /// <returns></returns>
        public DataSet GetDataSet(string selectCmd, string selectCmdEx, DbParameter[] dbParas, string orderByFieldName, int pageNo, int pageSize, ref int totalPage)
        {
            try
            {
                //根据参数取数据
                string cParaWhere= DataHelper.BuildParaSqlString(selectCmd, dbParas);
      
                //构建WHERE 
                string cWhereTemp = "";
                if ( cParaWhere!= "" && selectCmd.IndexOf(" WHERE ", StringComparison.InvariantCultureIgnoreCase) < 0)
                    cWhereTemp = " WHERE 1=1";

                //参数构建后的基本SQL语句/如果有占位符@@ParaWhere则在此占位符里替换并生成参数
                string sqlCommand = "";
                if (selectCmd.IndexOf("@@ParaWhere",StringComparison.InvariantCultureIgnoreCase)<0)
                    sqlCommand = selectCmd + cWhereTemp + " " + cParaWhere+ " " + selectCmdEx;
                else
                    sqlCommand = selectCmd.Replace("@@ParaWhere"," 1=1 "+cParaWhere)+ " " + selectCmdEx;


                //创建Db
                Database db = DatabaseFactory.CreateDatabase(ConnectionKey);
                DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);

                if (CommandTimeOut > 0)
                    dbCommand.CommandTimeout = CommandTimeOut;
                DataHelper.BuildDbParaAndValue(db, dbCommand, dbParas);

      
                //若要分页
                if (pageSize > 0)
                {
                    if (totalPage == -1)  //如果需要检索页码
                    {
                        //统计总页码SQL语句：注意-统计页码时将用Count(1)替换FROM关键字前的所有语句
                        string sqlCmd_TotalPage = "";
                         //分页时必须以SELECT语句开始
                        sqlCmd_TotalPage = "SELECT COUNT(1) as _TotalRecord FROM " +
                            sqlCommand.Substring(sqlCommand.IndexOf(" FROM ", StringComparison.InvariantCultureIgnoreCase) + 5);

                        dbCommand.CommandText=sqlCmd_TotalPage;
                        
                        int totalRecord = Convert.ToInt32(db.ExecuteScalar(dbCommand));
                        pageSize = Math.Max(pageSize, 1);
                        totalPage = (int)Math.Ceiling((double)totalRecord / (double)pageSize);
                    }

                    //替换为分页SQL语句：注意-分页时必须以SELECT语句开始或者使用@@SELECT占位符
                    string sqlCmd_Page = DataHelper.GetDataPageSqlCmd(sqlCommand, orderByFieldName, pageNo, pageSize);
                    dbCommand.CommandText=sqlCmd_Page;
                }

                //检索数据
                DataSet ds = db.ExecuteDataSet(dbCommand);
                return ds;
            }
            catch
            {
                throw;
            }
        }

        #endregion 
    }
}
