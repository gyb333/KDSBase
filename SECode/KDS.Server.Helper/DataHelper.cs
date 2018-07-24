using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.Collections;
using KDS.Common;
using System.Text.RegularExpressions;

/***********************************************************
 * 功能：数据层访问
 * 说明： 
 *       1.此类库整合了CSS系统经典的按需更新、多表自动更新的机制；最初创建日期：2001/01/01
 *       2.整合了ADO 2.0及微软的SqlHelper
 *       3.多年模型的积累，非KOGC企业使用前请通知作者，请勿侵权
 *       
 * 创建：胡海明,huhaiming@gmail.com
 * 时间：2008/08
  ************************************************************/

namespace KDS.Server.Helper
{
    #region HTable

    /// <summary>
    /// 表类型
    /// </summary>
    public enum HTableType
    {
        /// <summary>
        /// 可更新主表
        /// </summary>
        Master,

        /// <summary>
        /// 可更新明细表
        /// </summary>
        Detail,

        /// <summary>
        /// 其他表
        /// </summary>
        Other
    }

    /// <summary>
    /// 表缓冲模式
    /// </summary>
    public enum BufferModeOverrideMode
    {
        /// <summary>
        /// 行缓冲
        /// </summary>
        Row,

        /// <summary>
        /// 表缓冲
        /// </summary>
        Table
    }

    /// <summary>
    /// HTable-核心表结构用于自动的数据集处理，
    /// 最初来源于CSS系统，Copyrights by huhaiming,2001~2008
    /// </summary>
    public sealed class HTable
    {
        /// <summary>
        /// 表类型,huhm2008
        /// </summary>
        public HTableType TableType=HTableType.Master;

        /// <summary>
        /// 是否发送更新默认true(Aux类型的表始终不会更新)
        /// </summary>
        public bool SendUpdate=true;

        /// <summary>
        /// DataTable本地表名
        /// </summary>
        public string CursorTableName="";

        /// <summary>
        /// SQL物理表名
        /// </summary>
        public string SqlTableName="";

        /// <summary>
        /// 查询SQL语句
        /// </summary>
        public string SelectCmd="";

        /// <summary>
        /// 查询条件，WHERE语句
        /// </summary>
        public string SelectWhere="";

        /// <summary>
        /// 临时的SQL条件WHERE语句，会随时改变请勿存放数据
        /// </summary>
        public string SelectWhereTemp="";

        /// <summary>
        /// 扩展SQL语句，如ORDER BY
        /// </summary>
        public string SelectCmdEx="";

        /// <summary>
        /// 表缓冲模式
        /// </summary>
        public BufferModeOverrideMode BufferModeOverride=BufferModeOverrideMode.Row;

        private string[] mKeyFieldList= { "ID" };
        /// <summary>
        /// 表关键字段名列表，支持多列并可带表的别名
        /// </summary>
        public string[] KeyFieldList
        {
            get
            {
                return this.mKeyFieldList;
            }
            set
            {
                this.mKeyFieldList = value;

                //转换成不带别名的字段名
                if (this.mKeyFieldList.Length > 0)
                {
                    this.mKeyFieldList2 = new string[this.mKeyFieldList.Length];
                    for (int i = 0; i < this.mKeyFieldList.Length; i++)
                    {
                        this.mKeyFieldList2[i] = DataHelper.TransToColumnName(this.mKeyFieldList[i]);
                    }
                }
            }
        }


        private string[] mKeyFieldList2 = { "ID" };
        /// <summary>
        /// 表关键字段名列表，存放不带表别名的关键字段列表
        /// </summary>
        public string[] KeyFieldList2
        {
            get
            {
                return this.mKeyFieldList2;
            }
        }


        /// <summary>
        /// 业务单据号字段，为空时不产生单号
        /// </summary>
        public string BillNoFieldName="";

        /// <summary>
        /// 不更新字段，应为字段的别名
        /// </summary>
        public string[] NoUpdateFieldList ={};


        public DbCommand InsertDbCommand;

        public DbCommand DeleteDbCommand;

        public DbCommand UpdateDbCommand;


        /// <summary>
        /// HTable表对象
        /// </summary>
        public HTable()
        {

        }
    }

    #endregion 

    /// <summary>
    /// 数据层访问类（不与数据库交互，仅构造SQL命令或参数）
    /// huhm2008 (huhaiming@gmail.com)
    /// </summary>
    public sealed class DataHelper
    {

        #region GetSqlCmd

        /// <summary>
        /// 获取SQL插入语句,huhm2008
        /// </summary>
        /// <param name="cursorTable">数据表</param>
        /// <param name="sqlTableName">SQL物理表名</param>
        /// <param name="noUpdateFieldList">不更新字段列表</param>
        /// <returns></returns>
        public static string GetSqlInsCmd(DataTable cursorTable,string sqlTableName,string[] noUpdateFieldList)
        {
            //计算SqlInsCmd
            //huhaiming@gmail.com
            string lcSqlCmd="";

            if (cursorTable != null)
            {
                string lcUpdFieldNameList ="";
                string lcUpdFieldValList="";

                foreach (string noUpdateFieldListItem in noUpdateFieldList)
                {
                    if (!cursorTable.Columns.Contains(noUpdateFieldListItem))
                        throw new Exception("获取SQL插入语句失败，不更新字段：" + noUpdateFieldListItem + "在表" + cursorTable.TableName + "中不存在。");
                }

                for (int i = 0; i < cursorTable.Columns.Count; i++)
                {
                    if (!noUpdateFieldList.Contains(cursorTable.Columns[i].ColumnName, StringComparer.InvariantCultureIgnoreCase))
                    {
                        lcUpdFieldNameList += "[" + cursorTable.Columns[i].ColumnName + "],";
                        lcUpdFieldValList += "@" + cursorTable.Columns[i].ColumnName + ",";
                    }
                }

                lcUpdFieldNameList = lcUpdFieldNameList.Substring(0, lcUpdFieldNameList.Length - 1);
                lcUpdFieldValList = lcUpdFieldValList.Substring(0, lcUpdFieldValList.Length - 1);
                lcSqlCmd = "INSERT INTO [" + sqlTableName + "] (" + lcUpdFieldNameList + ") VALUES (" + lcUpdFieldValList + ")";
            }


            return lcSqlCmd;
        }

        /// <summary>
        /// 获取SQL更新语句,huhm2008
        /// </summary>
        /// <param name="cursorTable">数据表</param>
        /// <param name="sqlTableName">SQL物理表名</param>
        /// <param name="keyFieldList">关键字段列表</param>
        /// <param name="noUpdateFieldList">不更新字段列表</param>
        /// <returns></returns>
        public static string GetSqlUpdCmd(DataTable cursorTable, string sqlTableName,string[] keyFieldList,string[] noUpdateFieldList)
        {
            //计算UpdInsCmd
            //huhaiming@gmail.com
            string lcSqlCmd = "";

            if (cursorTable != null)
            {
                string lcUpdFieldNameList = "";
                string lcKeyFieldList = "";

                foreach (string noUpdateFieldListItem in noUpdateFieldList)
                {
                    if (!cursorTable.Columns.Contains(noUpdateFieldListItem))
                        throw new Exception("获取SQL更新语句失败，不更新字段：" + noUpdateFieldListItem + "在表" + cursorTable.TableName + "中不存在。");
                }

                foreach (string keyFieldListItem in keyFieldList)
                {
                    if (!cursorTable.Columns.Contains(keyFieldListItem))
                        throw new Exception("获取SQL更新语句失败，关键字段：" + keyFieldListItem + "在表" + cursorTable.TableName + "中不存在。");
                }

                for (int i = 0; i < cursorTable.Columns.Count; i++)
                {
                    if (!noUpdateFieldList.Contains(cursorTable.Columns[i].ColumnName, StringComparer.InvariantCultureIgnoreCase) && !keyFieldList.Contains(cursorTable.Columns[i].ColumnName, StringComparer.InvariantCultureIgnoreCase))
                    {
                        lcUpdFieldNameList += "[" + cursorTable.Columns[i].ColumnName + "]=@" +
                            cursorTable.Columns[i].ColumnName + ",";
                    }
                }

                foreach (string keyFieldListItem in keyFieldList)
                {
                    lcKeyFieldList += "[" + keyFieldListItem + "]=@" + keyFieldListItem + " AND ";
                }

                lcUpdFieldNameList = lcUpdFieldNameList.Substring(0, lcUpdFieldNameList.Length - 1);
                lcKeyFieldList = lcKeyFieldList.Substring(0, Math.Max(lcKeyFieldList.Length - 4, 0));
                lcSqlCmd = "UPDATE [" + sqlTableName + "] SET " + lcUpdFieldNameList + " WHERE " + lcKeyFieldList ;
            }


            return lcSqlCmd;
        }

        /// <summary>
        /// 获取SQL删除语句,huhm2008
        /// </summary>
        /// <param name="cursorTable">数据表</param>
        /// <param name="sqlTableName">SQL物理表名</param>
        /// <param name="keyCondition">条件</param>
        /// <returns></returns>
        public static string GetSqlDelCmd(DataTable cursorTable, string sqlTableName, string keyCondition)
        {
            //计算SqlInsCmd
            //huhaiming@gmail.com
            string lcSqlCmd = "";

            if (cursorTable != null)
            {
                lcSqlCmd = "DELETE [" + sqlTableName + "] WHERE " + keyCondition;
            }

            return lcSqlCmd;
        }

        #endregion 


        #region DataTrans

        /// <summary>
        /// 转换type类型到DbType,huhm2008
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DbType GetDbType(Type type)
        {

            DbType val = DbType.String; // default value

            try
            {
                if(type.Name.ToLower()=="byte[]")
                {
                    val = DbType.Binary;
                }
                else
                {
                    val = (DbType)Enum.Parse(typeof(DbType), type.Name, true);
                }
            }
            catch
            {
                // add error handling to suit your taste
                throw;
            }

            return val;

        }

        /// <summary>
        /// 将带有别名的字段名转换成物理表的字段名（即去掉别名）,huhm2008
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


        /// <summary>
        /// 根据参数构建SQL条件语句（返回SQL条件语句）,huhm2008
        /// </summary>
        /// <param name="sqlCmd">源sql语句：如果sqlCmd已存在参数则不追加此参数</param>
        /// <param name="dbParas">参数</param>
        /// <returns>返回SQL条件语句，无参数时返回string.empty，否则返回形如： AND UserCode=@UserCode and Password=@Password </returns>
        public static string BuildParaSqlString(string sqlCmd,DbParameter[] dbParas)
        {
            //根据参数取数据
            StringBuilder cParaWhere = new StringBuilder();
            if (dbParas != null)
            {
                foreach (DbParameter para in dbParas)
                {
                    string paraValue = para.Value==null?"":para.Value.ToString();

                    if (paraValue.IndexOf(',') < 0)
                    {
                        //构建SQL参数
                        //if (sqlCmd.IndexOf("@" + TransToColumnName(para.ParameterName) + " ", StringComparison.InvariantCultureIgnoreCase) < 0)
                        if (!Regex.IsMatch(sqlCmd+" ", "@" + TransToColumnName(para.ParameterName) + "[^0-9a-zA-Z]+", RegexOptions.IgnoreCase))
                            cParaWhere.Append(" AND " + para.ParameterName + "=@" + TransToColumnName(para.ParameterName));
                    }
                    else
                    {
                        //参数值中带有“,”则不构造参数,用SQL字符串构建
                        //检测是否为Int数组，非Int数组字符串不能添加（有安全漏洞）
                        if (FuncHelper.IsIntArrayString(paraValue))
                        {
                            cParaWhere.Append(" AND "+para.ParameterName+" IN("+paraValue+")");
                        }
                    }
                }
            }

            return cParaWhere.ToString();
        }


        /// <summary>
        /// 为DbCommand构建参数及设置参数值（参数名支持表别名）,huhm2008
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbCommand"></param>
        /// <param name="dbParas"></param>
        public static void BuildDbParaAndValue(Database db, DbCommand dbCommand, DbParameter[] dbParas)
        {
            if (dbParas != null)
            {
                foreach (DbParameter para in dbParas)
                {
                    string paraValue = para.Value == null ? "" : para.Value.ToString();
                    if (paraValue.IndexOf(',') < 0)
                    {
                        db.AddInParameter(dbCommand, TransToColumnName(para.ParameterName), para.DbType, para.Value);
                    }
                }
            }
        }


        /// <summary>
        /// 根据当前数据表的数据行设置参数值,huhm2008
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <param name="dr"></param>
        public static void SetDbParameterValue(DbCommand dbCommand, DataRow dr)
        {
            foreach (DbParameter para in dbCommand.Parameters)
            {
                if (dr.RowState!=DataRowState.Deleted)
                    para.Value = dr[para.ParameterName.Replace("@","")];
                else
                    para.Value = dr[para.ParameterName.Replace("@", ""),DataRowVersion.Original];
            }
        }

        #endregion 



        #region GetSqlCommand

        /// <summary>
        /// 获取SQL插入语句DbCommand,huhm2008
        /// </summary>
        /// <param name="db">Database实例</param>
        /// <param name="cursorTable">数据表</param>
        /// <param name="sqlTableName">SQL物理表名</param>
        /// <param name="noUpdateFieldList">不更新字段列表</param>
        /// <returns></returns>
        public static DbCommand GetSqlInsDbCommand(Database db,DataTable cursorTable, string sqlTableName, string[] noUpdateFieldList)
        {
            //计算SqlInsCmd
            //huhaiming@gmail.com

            DbCommand dbCommand = null;

            if (cursorTable != null)
            {
                StringBuilder sbUpdFieldNameList = new StringBuilder();
                StringBuilder sbUpdFieldValList = new StringBuilder();

                //foreach (string noUpdateFieldListItem in noUpdateFieldList)
                //{
                //    if (!cursorTable.Columns.Contains(noUpdateFieldListItem))
                //        throw new Exception("获取SQL插入语句失败，不更新字段：" + noUpdateFieldListItem + "在表" + cursorTable.TableName + "中不存在。");
                //}

                for (int i = 0; i < cursorTable.Columns.Count; i++)
                {
                    string columnName = cursorTable.Columns[i].ColumnName;
                    if (!noUpdateFieldList.Contains(columnName, StringComparer.InvariantCultureIgnoreCase))
                    {
                        sbUpdFieldNameList.Append("[" + columnName + "],");
                        sbUpdFieldValList.Append("@" + columnName + ",");
                    }
                }

                sbUpdFieldNameList.Remove(sbUpdFieldNameList.Length - 1,1);
                sbUpdFieldValList.Remove(sbUpdFieldValList.Length - 1,1);
                string lcSqlCmd="INSERT INTO [" + sqlTableName + "] (" + sbUpdFieldNameList.ToString() + ") VALUES (" + sbUpdFieldValList.ToString() + ")";
                dbCommand = db.GetSqlStringCommand(lcSqlCmd);

                for (int i = 0; i < cursorTable.Columns.Count; i++)
                {
                    string columnName = cursorTable.Columns[i].ColumnName;
                    if (!noUpdateFieldList.Contains(columnName, StringComparer.InvariantCultureIgnoreCase))
                    {
                        db.AddInParameter(dbCommand, columnName, GetDbType(cursorTable.Columns[i].DataType));
                    }
                }

            }

            return dbCommand;
        }

        /// <summary>
        /// 获取SQL更新语句DbCommand,huhm2008
        /// </summary>
        /// <param name="db">Database实例</param>
        /// <param name="cursorTable">数据表</param>
        /// <param name="sqlTableName">SQL物理表名</param>
        /// <param name="keyFieldList">关键字段列表</param>
        /// <param name="noUpdateFieldList">不更新字段列表</param>
        /// <returns></returns>
        public static DbCommand GetSqlUpdDbCommand(Database db, DataTable cursorTable, string sqlTableName, string[] keyFieldList, string[] noUpdateFieldList)
        {
            //计算UpdInsCmd
            //huhaiming@gmail.com

            DbCommand dbCommand = null;

            if (cursorTable != null)
            {
                StringBuilder sbUpdFieldNameList = new StringBuilder();
                StringBuilder sbKeyFieldList = new StringBuilder();

                 //foreach (string noUpdateFieldListItem in noUpdateFieldList)
                //{
                //    if (!cursorTable.Columns.Contains(noUpdateFieldListItem))
                //        throw new Exception("获取SQL更新语句失败，不更新字段：" + noUpdateFieldListItem + "在表" + cursorTable.TableName + "中不存在。");
                //}

                //foreach (string keyFieldListItem in keyFieldList)
                //{
                //    if (!cursorTable.Columns.Contains(DataHelper.TransToColumnName(keyFieldListItem)))
                //        throw new Exception("获取SQL更新语句失败，关键字段：" + keyFieldListItem + "在表" + cursorTable.TableName + "中不存在。");
                //}

                for (int i = 0; i < cursorTable.Columns.Count; i++)
                {
                    string columnName = cursorTable.Columns[i].ColumnName;
                    if (!noUpdateFieldList.Contains(columnName, StringComparer.InvariantCultureIgnoreCase) && !keyFieldList.Contains(columnName, StringComparer.InvariantCultureIgnoreCase))
                    {
                        sbUpdFieldNameList.Append("[" + columnName + "]=@" + columnName + ",");
                    }
                }

                foreach (string keyFieldListItem in keyFieldList)
                {
                    sbKeyFieldList.Append("[" + keyFieldListItem + "]=@" + keyFieldListItem + " AND ");
                }

                sbUpdFieldNameList.Remove(sbUpdFieldNameList.Length - 1,1);
                if (sbKeyFieldList.Length>=5)
                    sbKeyFieldList.Remove(sbKeyFieldList.Length - 5, 5);

                string lcSqlCmd="UPDATE [" + sqlTableName + "] SET " + sbUpdFieldNameList.ToString() + " WHERE " + sbKeyFieldList.ToString();

                dbCommand = db.GetSqlStringCommand(lcSqlCmd);

                for (int i = 0; i < cursorTable.Columns.Count; i++)
                {
                    string columnName = cursorTable.Columns[i].ColumnName;
                    if (!noUpdateFieldList.Contains(columnName, StringComparer.InvariantCultureIgnoreCase) && !keyFieldList.Contains(columnName, StringComparer.InvariantCultureIgnoreCase))
                    {
                        db.AddInParameter(dbCommand, columnName, GetDbType(cursorTable.Columns[i].DataType));
                    }
                }

                foreach (string keyFieldListItem in keyFieldList)
                {
                    db.AddInParameter(dbCommand, keyFieldListItem, GetDbType(cursorTable.Columns[keyFieldListItem].DataType));
                }
            }
            
            return dbCommand;
        }

        /// <summary>
        /// 获取SQL删除语句DbCommand,huhm2008
        /// </summary>
        /// <param name="db">Database实例</param>
        /// <param name="cursorTable">数据表</param>
        /// <param name="sqlTableName">SQL物理表名</param>
        /// <param name="keyCondition">条件</param>
        /// <returns></returns>
        public static DbCommand GetSqlDelDbCommand(Database db,DataTable cursorTable, string sqlTableName, string keyCondition)
        {
            //计算SqlInsCmd
            //huhaiming@gmail.com

            DbCommand dbCommand = null;

            if (cursorTable != null)
            {
                string lcSqlCmd = "DELETE [" + sqlTableName + "] WHERE " + keyCondition;
                dbCommand = db.GetSqlStringCommand(lcSqlCmd);
            }

            return dbCommand;
        }

        /// <summary>
        /// 获取SQL删除语句DbCommand,huhm2008
        /// </summary>
        /// <param name="db">Database实例</param>
        /// <param name="cursorTable">数据表</param>
        /// <param name="sqlTableName">SQL物理表名</param>
        /// <param name="keyCondition">条件</param>
        /// <returns></returns>
        public static DbCommand GetSqlDelDbCommand(Database db, DataTable cursorTable, string sqlTableName, string[] keyFieldList)
        {
            //计算SqlInsCmd
            //huhaiming@gmail.com

            DbCommand dbCommand = null;

            if (cursorTable != null)
            {
                StringBuilder sbKeyFieldList = new StringBuilder();

                foreach (string keyFieldListItem in keyFieldList)
                {
                    if (!cursorTable.Columns.Contains(keyFieldListItem))
                        throw new Exception("获取SQL更新语句失败，关键字段：" + keyFieldListItem + "在表" + cursorTable.TableName + "中不存在。");
                }

                foreach (string keyFieldListItem in keyFieldList)
                {
                    sbKeyFieldList.Append("[" + keyFieldListItem + "]=@" + keyFieldListItem + " AND ");
                }
                
                if (sbKeyFieldList.Length >= 5)
                    sbKeyFieldList.Remove(sbKeyFieldList.Length - 5, 5);

                string lcSqlCmd = "DELETE [" + sqlTableName + "] WHERE " + sbKeyFieldList.ToString();

                dbCommand = db.GetSqlStringCommand(lcSqlCmd);

                foreach (string keyFieldListItem in keyFieldList)
                {
                    db.AddInParameter(dbCommand, keyFieldListItem, GetDbType(cursorTable.Columns[keyFieldListItem].DataType));
                }
            }

            return dbCommand;
        }

        #endregion 

        #region Page&Others

        /// <summary>
        /// 获取数据分页的SQL语句（限制：必须以SELECT开头）
        /// </summary>
        /// <param name="selectSqlCmd">原始SQL语句</param>
        /// <param name="orderbyFieldName">排序字段</param>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>分页的SQL语句</returns>
        public static string GetDataPageSqlCmd(string selectSqlCmd, string orderbyFieldName, int pageNo, int pageSize)
        {            
            string sqlCmd="";             
            //将常规的SELECT检索语句转换成能分页的SELECT语句            
            //sqlCmd = "SELECT top(" + pageSize.ToString() + ") * FROM ( " +
            //    "SELECT ROW_NUMBER() OVER (ORDER BY " + orderbyFieldName + ") AS _RowNum," + selectSqlCmd.Substring(7, selectSqlCmd.Length - 7) +
            //    ") as t WHERE _RowNum>((" + pageNo.ToString() + "-1)*" + pageSize.ToString() + ")";

            sqlCmd = "SELECT * FROM ( " +
                "SELECT ROW_NUMBER() OVER (ORDER BY " + orderbyFieldName + ") AS _RowNum," + selectSqlCmd.Substring(7, selectSqlCmd.Length - 7) +
                ") as t WHERE _RowNum>((" + pageNo.ToString() + "-1)*" + pageSize.ToString() + ") AND _RowNum<=(" + pageNo.ToString() + "*" + pageSize.ToString() + ") ";

            return sqlCmd;
        }


        /// <summary>
        /// 为分级多维汇总得到Grouping字段列表,GroupBy子句,Sum字段列表。[0]:GroupingSql-字段列表;[1]:GroupBy子句;[2]:Sum字段列表
        /// </summary>
        /// <param name="avGroupFieldList">可用的分类汇总Groupby字段(描述/物理字段名)</param>
        /// <param name="groupFieldCaptionList">要分类汇总的Groupby字段描述</param>
        /// <param name="avSumFieldList">可用的汇总字段(Sum/Avg)(描述/物理字段名)</param>
        /// <param name="sumFieldCaptionList">要汇总的字段描述(Sum/Avg)</param>
        /// <returns>string[]数值，[0]:GroupingSql-字段列表;[1]:GroupBy子句;[2]:Sum字段列表</returns>
        public static string[] GetRollupSqlCmd(Dictionary<string, string> avGroupFieldList, string[] groupFieldCaptionList, Dictionary<string, string> avSumFieldList, string[] sumFieldCaptionList)
        {
            return GetRollupSqlCmd(avGroupFieldList, groupFieldCaptionList, avSumFieldList, sumFieldCaptionList, true);
        }


        /// <summary>
        /// 为分级多维汇总得到Grouping字段列表,GroupBy子句,Sum字段列表。[0]:GroupingSql-字段列表;[1]:GroupBy子句;[2]:Sum字段列表
        /// </summary>
        /// <param name="avGroupFieldList">可用的分类汇总Groupby字段(描述/物理字段名)</param>
        /// <param name="groupFieldCaptionList">要分类汇总的Groupby字段描述</param>
        /// <param name="avSumFieldList">可用的汇总字段(Sum/Avg)(描述/物理字段名)</param>
        /// <param name="sumFieldCaptionList">要汇总的字段描述(Sum/Avg)</param>
        /// <param name="isSubTotal">是否产生小计</param>
        /// <returns>string[]数值，[0]:GroupingSql-字段列表;[1]:GroupBy子句;[2]:Sum字段列表</returns>
        public static string[] GetRollupSqlCmd(Dictionary<string, string> avGroupFieldList, string[] groupFieldCaptionList, Dictionary<string, string> avSumFieldList, string[] sumFieldCaptionList, bool isSubTotal)
        {
            string [] retVal=new string[3];
            string groupingSql = "";
            string groupBySql = "";
            string sumSql = "";

            //Groupby字段
            foreach (string field in groupFieldCaptionList)
            {
                string fieldValue = "";
                if (!avGroupFieldList.TryGetValue(field, out fieldValue))
                {
                    throw new Exception("无效的GroupBy分类汇总字段。");
                }

                if (isSubTotal)
                {
                    //CASE WHEN (GROUPING(tp.BrandName) = 1) THEN '合计' ELSE ISNULL(tp.BrandName, '未知') END AS 品牌,
                    groupingSql += string.Format("CASE WHEN (GROUPING({0}) = 1) THEN '合计' ELSE ISNULL({0}, '未知') END AS {1},", fieldValue, field);
                }
                else
                {
                    groupingSql += string.Format("{0} AS {1},", fieldValue, field);
                }
                groupBySql += fieldValue + ",";
            }
            if (groupingSql.LastIndexOf(",") == groupingSql.Length - 1)
                groupingSql = groupingSql.Substring(0, groupingSql.Length - 1);

            if (groupBySql.LastIndexOf(",") == groupBySql.Length - 1)
                groupBySql = groupBySql.Substring(0, groupBySql.Length - 1);

            retVal[0] = groupingSql;

            if (isSubTotal)
            {
                retVal[1] = "GROUP BY " + groupBySql + " WITH ROLLUP";
            }
            else
            {
                retVal[1] = "GROUP BY " + groupBySql;
            }


            //Sum字段
            foreach (string field in sumFieldCaptionList)
            {
                string fieldValue = "";
                if (!avSumFieldList.TryGetValue(field, out fieldValue))
                {
                    throw new Exception("无效的分类Sum汇总字段。");
                }

                sumSql += fieldValue + ",";
            }
            if (sumSql.LastIndexOf(",") == sumSql.Length - 1)
                sumSql = sumSql.Substring(0, sumSql.Length - 1);

            retVal[2] = sumSql;

            return retVal;
        }

        #endregion 
    }

}
