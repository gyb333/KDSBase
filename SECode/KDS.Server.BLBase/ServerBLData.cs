using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Transactions;
using System.Data.Common;
using System.Data.SqlClient;

using KDS.Server.Helper;
using KDS.Model;
using KDS.Server.App;
/***********************************************************
 * 功能：服务端业务逻辑数据抽象基类
 *  
 * 创建：胡海明,huhaiming@gmail.com
 * 时间：2008/08
 * 
 * 说明：来源于CSS系统(2001-2008 huhaiming@gmail.com)的核心数据处理模型
  ************************************************************/
namespace KDS.Server.BLBase
{
    /// <summary>
    /// 服务端业务逻辑数据抽象基类
    /// 最终业务类的实现要重载：InitProperty()、DefineHTable()
    /// 
    /// huhm2008(huhaiming@gmail.com)
    /// </summary>
    public abstract class ServerBLData : ServerBLBase
    {
        #region Property

        /// <summary>
        /// BillTypeID（初始-1），用于：a.生成BillNo（仅Server端）；b.日志查询；c.ReportVer
        /// </summary>
        protected int mBillTypeID = -1;


        /// <summary>
        /// GenBillNoKeyID：用于产生单号时的键值ID（如CompanyID或BranchID可用于产生单号，默认根据CompanyID产生）；
        /// </summary>
        protected int mGenBillNoKeyID = 0;

        /// <summary>
        /// DistributorID：用于产生单号时的DistributorID
        /// </summary>
        protected int mGenBillNoDistributorID = 0;

        /// <summary>
        /// CompBranchID：用于产生单号时的CompBranchID
        /// </summary>
        protected int mGenBillNoCompBranchID = 0;

        /// <summary>
        /// 是否允许新增
        /// </summary>
        protected bool mAllowAddNew = true;

        /// <summary>
        /// 是否允许修改
        /// </summary>
        protected bool mAllowModify = true;

        /// <summary>
        /// 是否允许删除
        /// </summary>
        protected bool mAllowDelete = true;

        /// <summary>
        /// 是否对CompanyID的检查
        /// </summary>
        protected bool mEnableCheckCompanyID = true;

        /// <summary>
        /// 调用GetData()取数据时是否允许不提供参数（为空时会取全部数据，导致意外的大量数据取出影响性能）
        /// </summary>
        protected bool mEnableGetDataParasNULL = false;

        /// <summary>
        /// 数据表类型定义列表
        /// </summary>
        protected HTable[] mHTableList;


        /// <summary>
        /// 实例化服务端业务逻辑
        /// </summary>
        /// <param name="userInfo">当前会话的UserInfo实例</param>
        public ServerBLData(T_UserInfo userInfo)
            : base(userInfo)
        {

        }

        /// <summary>
        /// 实例化服务端业务逻辑
        /// </summary>
        /// <param name="userInfo">当前会话的UserInfo实例</param>
        /// <param name="appID">AppID，应用程序标识</param>
        public ServerBLData(T_UserInfo userInfo, int appID)
            : base(userInfo, appID)
        {

        }


        /// <summary>
        /// 定义HTable表结构
        /// </summary>
        protected virtual void DefineHTable()
        {



        }

        /// <summary>
        /// 初始化属性：（构造函数前调用。如属性mAppID设置，顺序应：1.先设置本类属性；2.再执行基类；）
        /// 最终业务类的实现要设置的有：mAppID、mBillTypeID、mAllowAdd、mAllowModify、mAllowDelete
        /// </summary>
        protected override void InitProperty()
        {
            if (this.mBillTypeID < 0)
                throw new Exception("服务-请设置mBillTypeID属性");

            base.InitProperty();
        }

        #endregion




        #region BillNo

        /// <summary>
        /// 根据BillTypeID、CompanyID生成业务单据编号、支持事务。如果
        /// </summary>
        /// <returns>业务单据编号,string.Empty：失败</returns>
        protected object GetBillNo()
        {
            if (this.mBillTypeID <= 0)
            {
                return null;
            }

            try
            {
                //Modified by huhm,2009/10/13

                //BranchID或ComapnyID
                int genBillNoKeyID = 0;
                if (this.mGenBillNoKeyID <= 0)
                    genBillNoKeyID = this.mUserInfo.CompanyID;
                else
                    genBillNoKeyID = this.mGenBillNoKeyID;

                //DistributorID 
                int genBillNoDistributorID = 0;
                if (this.mGenBillNoDistributorID <= 0)
                    genBillNoDistributorID = this.mUserInfo.CompanyID;
                else
                    genBillNoDistributorID = this.mGenBillNoDistributorID;

                string sqlCommand = "prGetBillNo";
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                db.AddInParameter(dbCommand, "intBillTypeID", DbType.Int32, this.mBillTypeID);
                db.AddInParameter(dbCommand, "intCompanyID", DbType.Int32, genBillNoKeyID);
                db.AddInParameter(dbCommand, "intDistributorID", DbType.Int32, genBillNoDistributorID);
                db.AddInParameter(dbCommand, "intCompBranchID ", DbType.Int32, this.mGenBillNoCompBranchID );
                db.AddOutParameter(dbCommand, "chvBillNo", DbType.String, 20);

                db.ExecuteNonQuery(dbCommand);

                return db.GetParameterValue(dbCommand, "chvBillNo");
            }
            catch
            {
                throw;
            }
        }

        #endregion


        #region DataProcessLoadData

        /// <summary>
        /// 检索数据表前的处理
        /// </summary>
        /// <returns></returns>
        protected virtual void BeforeGetData(DataSet dsData)
        {



        }

        /// <summary>
        /// 装索数据后的处理
        /// </summary>
        protected virtual void AfterGetData(DataSet dsData)
        {



        }

        /// <summary>
        /// [为更新表而检索数据]为更新数据表而检索数据结构及数据 by CSS/huhaiming@gmail.com,2001~2008
        /// 1.条件只针对主表有效，明细表根据与主表关联的主键自动取值
        /// 2.会自动判断是否有相关的：查看 权限
        /// 3.自动根据条件SelectWhere过滤数据；
        /// 
        /// [注意事项]：
        /// 1.主表有且只有一个并且是this.mHTableList中的第一个；
        /// 2.主表主键限制一个字段；
        /// 3.主表可以多行记录，但如果同时有主表、明细表，主表应只有一条记录（因为条件针对主表、明细表是根据主表的值再取数据）；
        /// 4.明细表与主表相对应的主键应位于主键列表中的第1个，主从表主键名称可不相同
        /// </summary>
        /// <param name="dbParas">参数列表及值。参数名支持表别名；并支持动态参数：即根据参数名构造SQL条件</param>
        /// <param name="isGetModel">是否为表结构而只取空表的结构（针对主、明细表）</param>
        /// <returns></returns>
        public DataSet GetData(DbParameter[] dbParas, bool isGetModel)
        {
            //若第一次调用没DefineHTable则调用
            if (this.mHTableList == null)
                this.DefineHTable();

            //若继承类没实现DefineHTable则throw exception
            if (this.mHTableList == null)
                throw new Exception("服务端-表结构未定义");

            if (dbParas == null && !isGetModel && !this.mEnableGetDataParasNULL)
                throw new Exception("服务端-除非只取表结构否则取数据时必须提供参数");

            DataSet dsData = new DataSet();

            try
            {
                this.BeforeGetData(dsData);

                Database db = DatabaseFactory.CreateDatabase();
                foreach (HTable hTable in this.mHTableList)  //确保先检索主表记录
                {
                    string sqlCommand = string.Empty;
                    DbCommand dbCommand = null;

                    if (hTable.TableType == HTableType.Master)
                    {
                        //主表根据参数取数据
                        StringBuilder cParaWhere = new StringBuilder();
                        if (!isGetModel)
                        {
                            if (dbParas != null)
                            {
                                cParaWhere.Append(DataHelper.BuildParaSqlString(hTable.SelectWhere, dbParas));
                            }
                        }
                        else
                        {
                            cParaWhere.Append(" AND 1=2");
                        }

                        //构建WHERE
                        string cWhereTemp = "";
                        if (hTable.SelectWhere != string.Empty)
                            cWhereTemp = " WHERE";

                        if (cParaWhere.ToString() != string.Empty && cWhereTemp == string.Empty)
                            cWhereTemp = " WHERE 1=1";

                        sqlCommand = hTable.SelectCmd + cWhereTemp + " " + hTable.SelectWhere + " " + cParaWhere.ToString() + " " + hTable.SelectCmdEx;

                        dbCommand = db.GetSqlStringCommand(sqlCommand);

                        if (!isGetModel)
                        {
                            DataHelper.BuildDbParaAndValue(db, dbCommand, dbParas);
                        }
                    }
                    else  // Detail/Aux ...
                    {
                        SqlParameter[] detlTableParas = null; //明细表参数临时变量；

                        //其他表根据条件取数据
                        StringBuilder cParaWhere = new StringBuilder();
                        if (hTable.TableType == HTableType.Detail)
                        {
                            //明细表根据主表的键值取数据
                            if (dsData.Tables[this.mHTableList[0].CursorTableName].Rows.Count > 0)
                            {
                                //DEBUG_HUHM:待改进，现只整形主键
                                object oMainTableKeyValue = dsData.Tables[this.mHTableList[0].CursorTableName].Rows[0][this.mHTableList[0].KeyFieldList2[0]];

                                //主表键值
                                //cParaWhere.Append(" and " + hTable.KeyFieldList[0] + " =" + oMainTableKeyValue.ToString());
                                string paraName = DataAccess.TransToColumnName(hTable.KeyFieldList[0]);
                                cParaWhere.Append(" and " + hTable.KeyFieldList[0] + " =@" + paraName);
                                detlTableParas = new SqlParameter[1];
                                detlTableParas[0] = new SqlParameter(paraName, oMainTableKeyValue);
                            }
                            else
                            {
                                cParaWhere.Append(" and 1=2");
                            }
                        }

                        //构建WHERE
                        string cWhereTemp = "";
                        if (hTable.SelectWhere != string.Empty)
                            cWhereTemp = " WHERE";

                        if (cParaWhere.ToString() != string.Empty && cWhereTemp == string.Empty)
                            cWhereTemp = " WHERE 1=1";

                        sqlCommand = hTable.SelectCmd + cWhereTemp + " " + hTable.SelectWhere + " " + cParaWhere.ToString() + " " + hTable.SelectCmdEx;

                        dbCommand = db.GetSqlStringCommand(sqlCommand);
                        if (detlTableParas != null)
                        {
                            DataHelper.BuildDbParaAndValue(db, dbCommand, detlTableParas);
                        }

                    }   // end of if (hTable.TableType == HTableType.Master)

                    if (dbCommand != null)
                    {
                        db.LoadDataSet(dbCommand, dsData, hTable.CursorTableName);
                        dsData.Tables[hTable.CursorTableName].Prefix = hTable.TableType.ToString();  //利用冗余的PreFix属性给客户端传递表的类型

                        //DataSet dataSet = new DataSet();

                        //dsData.Tables.Add(dataSet.Tables[0].Copy());
                        //if (dataSet!=null) dataSet.Dispose();
                    }
                }   // end of foreach

                this.AfterGetData(dsData);
            }
            catch
            {
                throw;
            }

            return dsData;
        }

        #endregion



        #region DataProcess-SaveData

        /// <summary>
        /// 数据存盘前的校验
        /// </summary>
        protected virtual void Validate(DataSet dsData)
        {

        }

        /// <summary>
        /// 数据存盘前的数据计算
        /// </summary>
        protected virtual void CalculateItem(DataSet dsData)
        {

        }

        /// <summary>
        /// 存盘前的处理
        /// </summary>
        protected virtual void BeforeSave(DataSet dsData)
        {
            this.Validate(dsData);
            this.CalculateItem(dsData);
        }

        /// <summary>
        /// 存盘后的处理
        /// </summary>
        protected virtual void AfterSave(DataSet dsData)
        {

        }


        /// <summary>
        /// [数据更新] by CSS/huhaiming@gmail.com,2001~2008
        /// 1.自动根据内存表生存相应的新增、修改、删除等SQL语句并自动发送更新（根据行状态判断Add\Delete\Modify）；
        /// 2.自动产生主从表的主键ID（不包装成事务）、相关的业务单据号（包装成事务）
        /// 3.支持多表多条记录同时更新、主从表、辅助更新表都自动封装成事务
        /// 4.会自动判断是否有相关的：新增、修改、删除权限
        /// 5.允许设置是否允许更新、新增、删除 资料；
        /// 6.更新的时候自动根据条件SelectWhere过滤数据（前提是过滤的条件数据字段必须存在内存表中）；
        /// 
        /// [注意事项]：
        /// 1.主表有且只有一个并且是this.mHTableList中的第一个；
        /// 2.主表主键限制一个字段；
        /// 3.主表可以多行记录同时更新，但如果同时有主表、明细表，这时主表只能有一条记录（要动态生成主表的主键值并自动更新明细表主键值）；
        /// 4.新增明细表记录时其主键值会同步于主表，但明细表对应的主键应位于主键列表中的第1个，主从表主键名称可不相同
        /// 5.更新顺序为：先辅助表/明细表、后主表
        /// </summary>
        /// <returns>成功返回更新后的DataSet，失败null</returns>
        public DataSet Save(DataSet dsData)
        {
            bool lACLNew = this.CheckUserFuncAccess("新增");
            bool lACLModify = this.CheckUserFuncAccess("修改");
            bool lACLDelete = this.CheckUserFuncAccess("删除");

            //若第一次调用没DefineHTable则调用
            if (this.mHTableList == null)
                this.DefineHTable();

            //若继承类没实现DefineHTable则throw exception
            if (dsData == null || this.mHTableList == null)
                throw new Exception("服务端-未提供更新的数据源或表结构未定义");

            if (!dsData.HasChanges() || dsData.Tables[this.mHTableList[0].CursorTableName].Rows.Count == 0)
                throw new Exception("服务端-没有需要更新的数据");

            try
            {
                this.BeforeSave(dsData);


                #region 权限

                foreach (HTable hTable in this.mHTableList)
                {
                    //DataTable dtUpdate = dsData.Tables[hTable.CursorTableName].GetChanges();
                    DataTable dtUpdate = dsData.Tables[hTable.CursorTableName];

                    if (dtUpdate.Rows.Count > 0 && hTable.SendUpdate)
                    {
                        foreach (DataRow drUpdate in dtUpdate.Rows)  //暂时禁止DEBUG_HUHM:tUpdate.Select(hTable.SelectWhere)
                        {
                            //权限
                            if (hTable.TableType == HTableType.Master && drUpdate.RowState == DataRowState.Added)
                            {
                                if (!this.mAllowAddNew)
                                    throw new Exception("服务端-不允许新增资料");
                                if (!lACLNew)
                                    throw new Exception("服务端-您没有新增资料的权限");
                            }

                            if (drUpdate.RowState == DataRowState.Modified)
                            {
                                if (!this.mAllowModify)
                                    throw new Exception("服务端-不允许修改资料");
                                if (!lACLModify)
                                    throw new Exception("服务端-您没有修改资料的权限");
                            }

                            if (hTable.TableType == HTableType.Master && drUpdate.RowState == DataRowState.Deleted)
                            {
                                if (!this.mAllowAddNew)
                                    throw new Exception("服务端-不允许删除资料");
                                if (!lACLNew)
                                    throw new Exception("服务端-您没有删除资料的权限");
                            }


                            //自动检查非法修改
                            if (this.mEnableCheckCompanyID && drUpdate.RowState != DataRowState.Unchanged)
                            {
                                if (dtUpdate.Columns.Contains("CompanyID"))
                                {
                                    object companyID;
                                    if (drUpdate.RowState == DataRowState.Deleted)
                                        companyID = drUpdate["CompanyID", DataRowVersion.Original];
                                    else
                                    {
                                        companyID = drUpdate["CompanyID"];

                                        if (drUpdate.RowState == DataRowState.Modified)
                                        {
                                            if (companyID.ToString() != drUpdate["CompanyID", DataRowVersion.Original].ToString())
                                                throw new Exception("服务端-禁止更改共用资料或其他公司资料。");
                                        }
                                    }

                                    if (this.mUserInfo.CompanyID != Convert.ToInt32(companyID))
                                        throw new Exception("服务端-禁止更改共用资料或其他公司资料。");
                                }
                            }
                        } // end of foreach 

                    } // end of if (dtUpdate.Rows.Count > 0 ...

                } //end of foreach (HTable hTable in this.mHTableList)


                #endregion

                #region 主键

                //轮循主表
                //Notes:主表只能有一个并且是Dataset中的第一个
                foreach (DataRow drMain in dsData.Tables[this.mHTableList[0].CursorTableName].Rows)
                {
                    //新增主表主键
                    //Notes:主表主键限制一个字段
                    object billID = null;
                    if (drMain.RowState == DataRowState.Added)
                    {
                        billID = drMain[this.mHTableList[0].KeyFieldList2[0]];
                        if (billID == null || billID.ToString().Trim() == "0" || billID.ToString() == string.Empty)
                        {
                            billID = this.GetIdentity(this.mHTableList[0].SqlTableName);
                            if (billID == null || billID.ToString() == "-1")
                            {
                                throw new Exception("主键ID产生失败。");
                            }
                            drMain[this.mHTableList[0].KeyFieldList2[0]] = billID;
                        }
                    }
                    else //Modify&Delete&UnChanged
                    {
                        if (drMain.RowState != DataRowState.Deleted)
                            billID = drMain[this.mHTableList[0].KeyFieldList2[0]];
                        else
                            billID = drMain[this.mHTableList[0].KeyFieldList2[0], DataRowVersion.Original];
                    }

                    //明细表主键，明细表的主键可以多个，但列表第一个必须与主表相同
                    //Notes:主表可以多行记录同时更新，但如果有明细表时主表只能有一条记录
                    if (billID != null)
                    {
                        foreach (HTable hTable in this.mHTableList)
                        {
                            if (hTable.TableType == HTableType.Detail)
                            {
                                foreach (DataRow drDetail in dsData.Tables[hTable.CursorTableName].Rows)
                                {
                                    //主表删除记录，同时删除明细表
                                    if (drMain.RowState == DataRowState.Deleted)              //主表删除
                                        drDetail.Delete();   //自动删除明细表相关记录（不判断条件、全部删除明细表）
                                    else if (drDetail.RowState == DataRowState.Added)
                                    {
                                        object dBillID = drDetail[hTable.KeyFieldList2[0]];
                                        if (dBillID == null || dBillID.ToString().Trim() == "0" || dBillID.ToString() == string.Empty)
                                            drDetail[hTable.KeyFieldList2[0]] = billID;            //Notes:明细表的主键值来源于主表，且必须是第1个
                                    }
                                }
                            }
                        }
                    } //end of if (billID!=null)

                }   // end of foreach主表

                #endregion

                #region Update数据

                Database db = DatabaseFactory.CreateDatabase();

                TransactionOptions option = new TransactionOptions();
                option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    //Notes: 先明细后主
                    for (int i = this.mHTableList.Length - 1; i >= 0; i--)  //倒排序，保证主表最后一个更新
                    {
                        HTable hTable = this.mHTableList[i];
                        //DataTable dtUpdate = dsData.Tables[hTable.CursorTableName].GetChanges();
                        DataTable dtUpdate = dsData.Tables[hTable.CursorTableName];

                        //准备更新数据库
                        if (dtUpdate.Rows.Count > 0 && hTable.SendUpdate)
                        {
                            //发送更新  //Notes: 更新的时候自动根据条件SelectWhere过滤数据（前提是过滤的条件数据字段必须存在内存表中）；
                            foreach (DataRow drUpdate in dtUpdate.Rows)  //暂时禁止DEBUG_HUHM:tUpdate.Select(hTable.SelectWhere)
                            {
                                //产生BillNo单据号
                                if (drUpdate.RowState == DataRowState.Added)  //remark by huhm,2011/9/29
                                {
                                    if (hTable.BillNoFieldName != "" && string.IsNullOrEmpty(drUpdate[hTable.BillNoFieldName].ToString()))
                                    {
                                        object cBillNo = this.GetBillNo();
                                        if (cBillNo == null || string.IsNullOrEmpty(cBillNo.ToString().Trim()))
                                        {
                                            throw new Exception("单据号产生失败。");
                                        }

                                        drUpdate[hTable.BillNoFieldName] = cBillNo;
                                    }
                                }

                                //自动产生SQL语句
                                //HUHM_DEBUG-后期考虑Cache dbCommand
                                if (drUpdate.RowState == DataRowState.Added)
                                {
                                    if (hTable.InsertDbCommand == null)
                                        hTable.InsertDbCommand = DataHelper.GetSqlInsDbCommand(db, dtUpdate,
                                            hTable.SqlTableName, hTable.NoUpdateFieldList);

                                    DataHelper.SetDbParameterValue(hTable.InsertDbCommand, drUpdate);
                                    db.ExecuteNonQuery(hTable.InsertDbCommand);
                                }

                                if (drUpdate.RowState == DataRowState.Modified)
                                {
                                    if (hTable.UpdateDbCommand == null)
                                        hTable.UpdateDbCommand = DataHelper.GetSqlUpdDbCommand(db, dtUpdate,
                                            hTable.SqlTableName, hTable.KeyFieldList2, hTable.NoUpdateFieldList);

                                    DataHelper.SetDbParameterValue(hTable.UpdateDbCommand, drUpdate);
                                    db.ExecuteNonQuery(hTable.UpdateDbCommand);
                                }

                                if (drUpdate.RowState == DataRowState.Deleted)
                                {
                                    if (hTable.DeleteDbCommand == null)
                                        hTable.DeleteDbCommand = DataHelper.GetSqlDelDbCommand(db, dtUpdate,
                                            hTable.SqlTableName, hTable.KeyFieldList2);

                                    DataHelper.SetDbParameterValue(hTable.DeleteDbCommand, drUpdate);
                                    db.ExecuteNonQuery(hTable.DeleteDbCommand);
                                }
                            } //end of foreach (DataRow drUpdate in dtUpdate.Rows)
                        } // end of if (dtUpdate.Rows.Count > 0 && hTable.TableType != HTableType.Aux)
                    } //end of for (int i = this.mHTableList.Count - 1; i >= 0; i--) 

                    scope.Complete();

                } // end of using(scope)

                dsData.AcceptChanges();

                #endregion

                this.AfterSave(dsData);

            }   // end of try
            catch
            {
                throw;
            }

            return dsData;
        }

        #endregion

    }
}
