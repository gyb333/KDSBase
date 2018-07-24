using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Common;

using KDS.Common;
using KDS.Client.App;
using KDS.Model;
/***********************************************************
 * 客户端业务逻辑数据基类
 * 
 * 创建：胡海明,huhaiming@gmail.com
 * 时间：2008/08
  ************************************************************/
namespace KDS.Client.BLBase
{
    /// <summary>
    /// 客户端BL数据基类
    /// 最终业务类的实现要重载：InitProperty()、CallGetDataService()、CallSaveService()、CallGetDataBrowseService()
    /// 
    /// huhm2008
    /// </summary>
    public abstract class ClientBLData : ClientBLBase
    {

        #region 属性

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageNo = 1;

        /// <summary>
        /// 总页码，获取数据分页时总页码
        /// </summary>
        public int TotalPage = -1;

        /// <summary>
        /// 分页大小，默认0（不分页）
        /// </summary>
        public int PageSize = 0;

        /// <summary>
        /// 用于数据更新数据集
        /// </summary>
        protected DataSet mDsData = null;
        public DataSet DsData
        {
            get
            {
                return this.mDsData;
            }
        }

        private string mMasterTableName = "";
        /// <summary>
        /// 主表名。如果设置了主表名的话则返回设置的名称，否则返回数据集中的第一个表的表名
        /// </summary>
        protected string MasterTableName
        {
            get
            {
                if (this.mMasterTableName == "")
                {
                    if (this.mDsData == null)
                        return "";
                    else
                        return this.mDsData.Tables[0].TableName;
                }
                else
                {
                    return this.mMasterTableName;
                }
            }
            set
            {
                this.mMasterTableName = value;
            }
        }


        /// <summary>
        /// 数据集中的主表
        /// </summary>
        public DataTable MasterTable
        {
            get
            {
                return this.mDsData.Tables[this.MasterTableName];
            }
        }

        /// <summary>
        /// 当前Export的数据表
        /// </summary>
        protected DataTable mDtExportData;
        public DataTable DtExportData
        {
            get
            {
                return this.mDtExportData;
            }
        }
        /// <summary>
        /// 为打印定义的数据表
        /// </summary>
        protected DataSet mDsPrintData;
        public DataSet DsPrintData
        {
            get
            {
                return this.mDsPrintData;
            }
            set
            {
                this.mDsPrintData = value;
            }
        }


        /// <summary>
        /// 当前用于Browse的数据表
        /// </summary>
        protected DataTable mDtBrowseData = null;
        public DataTable DtBrowseData
        {
            get
            {
                return this.mDtBrowseData;
            }
        }



        private bool mIsDataChangedAfterBLCreated = false;
        /// <summary>
        /// 数据在BL创建后是否已变更
        /// </summary>
        public bool IsDataChangedAfterBLCreated
        {
            get
            {
                return this.mIsDataChangedAfterBLCreated;
            }
        }

        /// <summary>
        /// BillTypeID（初始-1），用于：a.生成BillNo（仅Server端）；b.日志查询；c.ReportVer
        /// </summary>
        protected int mBillTypeID = -1;
        /// <summary>
        /// BillTypeID（初始-1），用于：a.生成BillNo（仅Server端）；b.日志查询；c.ReportVer
        /// </summary>
        public int BillTypeID
        {
            get
            {
                return this.mBillTypeID;
            }
        }

        #endregion 


        #region 报表属性

        /// <summary>
        /// 报表过滤条件的描述
        /// </summary>
        public string ReportFilterDesc;

        /// <summary>
        /// 报表分类汇总字段描述列表
        /// </summary>
        public string[] ReportGroupFieldCaptionList;

        /// <summary>
        /// 报表合计字段描述列表
        /// </summary>
        public string[] ReportSumFieldCaptionList;

        #endregion 


        #region 可用功能

        /// <summary>
        /// 是否允许新增
        /// </summary>
        protected bool mAllowAddNew = true;
        public bool AllowAddNew
        {
            get
            {
                return this.mAllowAddNew;
            }
        }
        /// <summary>
        /// 是否允许修改
        /// </summary>
        protected bool mAllowModify = true;
        public bool AllowModify
        {
            get
            {
                return this.mAllowModify;
            }
        }

        /// <summary>
        /// 是否允许删除
        /// </summary>
        protected bool mAllowDelete = true;
        public bool AllowDelete
        {
            get
            {
                return this.mAllowDelete;
            }
        }


        /// <summary>
        /// 是否允许导出
        /// </summary>
        protected bool mAllowExport = true;
        public bool AllowExport
        {
            get
            {
                return this.mAllowExport;
            }
        }

        /// <summary>
        /// 是否允许打印
        /// </summary>
        protected bool mAllowPrint = false;
        public bool AllowPrint
        {
            get
            {
                return this.mAllowPrint;
            }
        }



        #endregion



        #region 可用权限

        /// <summary>
        /// 新增权限
        /// </summary>
        protected bool mACLAddNew = false;
        public bool ACLAddNew
        {
            get
            {
                return this.mACLAddNew;
            }
        }

        /// <summary>
        /// 修改权限
        /// </summary>
        protected bool mACLModify = false;
        public bool ACLModify
        {
            get
            {
                return this.mACLModify;
            }
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        protected bool mACLDelete = false;
        public bool ACLDelete
        {
            get
            {
                return this.mACLDelete;
            }
        }

        /// <summary>
        /// 打印权限
        /// </summary>
        protected bool mACLPrint = false;
        public bool ACLPrint
        {
            get
            {
                return this.mACLPrint;
            }
        }

        /// <summary>
        /// 导出权限
        /// </summary>
        protected bool mACLExport = false;
        public bool ACLExport
        {
            get
            {
                return this.mACLExport;
            }
        }

        #endregion




        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="clientApp"></param>
        public ClientBLData(ClientApp clientApp)
            : base(clientApp)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="clientApp"></param>
        /// <param name="appID">AppID，应用程序标识</param>
        public ClientBLData(ClientApp clientApp, int appID)
            : base(clientApp, appID)
        {

        }



        /// <summary>
        /// 初始化属性后的处理（构造函数后调用。如权限检查等，顺序应：1.先设置本类属性；2.再执行基类；）
        /// </summary>
        protected override void AfterInitProperty()
        {
            this.mACLAddNew = this.mClientApp.CheckUserFuncAccess(this.mAppID, "新增");
            this.mACLModify = this.mClientApp.CheckUserFuncAccess(this.mAppID, "修改");
            this.mACLDelete = this.mClientApp.CheckUserFuncAccess(this.mAppID, "删除");
            this.mACLPrint = this.mClientApp.CheckUserFuncAccess(this.mAppID, "打印");
            this.mACLExport = this.mClientApp.CheckUserFuncAccess(this.mAppID, "导出");

            base.AfterInitProperty();
        }


        #region 数据处理

        /// <summary>
        /// 数据是否改变
        /// </summary>
        /// <returns></returns>
        public bool DataHasChanged()
        {
            if (this.mDsData != null)
                return this.mDsData.HasChanges();
            else
                return false;
        }

        /// <summary>
        /// 放弃数据
        /// </summary>
        public void RejectChanges()
        {
            if (this.mDsData != null)
                this.mDsData.RejectChanges();
        }




        /// <summary>
        /// 获取Browse数据
        /// </summary>
        public void GetBrowseData(HDbParameter dbPara)
        {
            if (!this.mACLView)
                throw new Exception("您没有查看数据的权限");

            try
            {
                this.BeforeGetBrowseData();

                this.mDtBrowseData = this.CallGetDataBrowseService(dbPara);
                this.mDtExportData = this.mDtBrowseData;

                this.AfterGetBrowseData();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 为获取Browse数据调用WCF服务
        /// </summary>
        /// <returns></returns>
        protected abstract DataTable CallGetDataBrowseService(HDbParameter dbPara);


        /// <summary>
        /// 获取Browse数据前的处理
        /// </summary>
        protected virtual void BeforeGetBrowseData()
        {


        }

        /// <summary>
        /// 获取Browse数据后的处理
        /// </summary>
        protected virtual void AfterGetBrowseData()
        {


        }




        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="isGetModel"></param>
        public void GetData(HDbParameter dbPara, bool isGetModel)
        {
            if (!this.mACLView)
                throw new Exception("您没有查看数据的权限");

            try
            {
                this.BeforeGetData();

                this.mDsData = this.CallGetDataService(dbPara, isGetModel);
                this.mDtExportData = this.mDsData.Tables[this.MasterTableName];

                this.AfterGetData();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取编辑数据需要调用的WCF服务
        /// </summary>
        /// <param name="isGetModel"></param>
        protected abstract DataSet CallGetDataService(HDbParameter dbPara, bool isGetModel);
        

        /// <summary>
        /// 获取数据前的处理
        /// </summary>
        protected virtual void BeforeGetData()
        {


        }

        /// <summary>
        /// 获取数据后的处理
        /// </summary>
        protected virtual void AfterGetData()
        {


        }






        /// <summary>
        /// 数据存盘
        /// </summary>
        public void Save()
        {
            try
            {
                if (DataHasChanged())
                {
                    //foreach (DataRow drMain in this.mDsData.Tables[0].Rows)
                    //{
                    //    //已检测过
                    //    //if (drMain.RowState == DataRowState.Added)
                    //    //{
                    //    //    if (!this.mAllowAddNew)
                    //    //        throw new Exception("不允许新增资料");
                    //    //    if (!this.mACLAddNew)
                    //    //        throw new Exception("您没有新增资料的权限");
                    //    //}

                    //    //已检测过
                    //    //if (drMain.RowState == DataRowState.Deleted)
                    //    //{
                    //    //    if (!this.mAllowAddNew)
                    //    //        throw new Exception("不允许删除资料");
                    //    //    if (!this.mACLDelete)
                    //    //        throw new Exception("您没有删除资料的权限");
                    //    //}
                    //}

                    foreach (DataTable dtUpdate in this.mDsData.Tables)
                    {
                        foreach (DataRow dr in dtUpdate.Rows)
                        {
                            if (dr.RowState == DataRowState.Modified)
                            {
                                if (!this.mAllowModify)
                                    throw new Exception("不允许修改数据");
                                if (!this.mACLModify)
                                    throw new Exception("您没有权限修改数据");
                            }
                        }
                    }

                    this.BeforeSave();

                    this.mDsData = this.CallSaveService();
                    this.mDtExportData = this.MasterTable;

                    this.AfterSave();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 数据存盘需要调用的WCF服务
        /// </summary>
        protected abstract DataSet CallSaveService();

        /// <summary>
        /// 存盘前的处理
        /// </summary>
        protected virtual void BeforeSave()
        {
            this.Validate();
            this.CalculateItem();
        }

        /// <summary>
        /// 存盘后的处理
        /// </summary>
        protected virtual void AfterSave()
        {
            this.mDsData.AcceptChanges();
            this.mIsDataChangedAfterBLCreated = true;
        }





        /// <summary>
        /// 变更状态
        /// </summary>
        /// <param name="dbPara">参数（如单号，单据状态）</param>
        public void ChangeStatus(HDbParameter dbPara)
        {
            //派生类应在BeforeChangeStatus里检查审核等权限，基类不检查审核等权限
            try
            {
                this.BeforeChangeStatus(dbPara);
                if (dbPara != null)
                {
                    this.CallChangeStatusService(dbPara);
                    this.AfterChangeStatus(dbPara);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 状态变更WCF服务
        /// </summary>
        /// <param name="dbPara">参数（如单号，单据状态）</param>
        protected virtual void CallChangeStatusService(HDbParameter dbPara)
        {
        }


        /// <summary>
        ///  变更状态前的处理，请在这里检查扩展功能的权限（基类不检查审核，关闭，作废等权限）
        /// </summary>
        /// <param name="dbPara">参数（如单号，单据状态）</param>
        protected virtual void BeforeChangeStatus(HDbParameter dbPara)
        {
        }

        /// <summary>
        /// 变更状态后的处理
        /// </summary>
        /// <param name="dbPara">参数（如单号，单据状态）</param>
        protected virtual void AfterChangeStatus(HDbParameter dbPara)
        {
            this.mIsDataChangedAfterBLCreated = true;
        }







        /// <summary>
        /// 存盘前计算数据
        /// </summary>
        protected virtual void CalculateItem()
        {


        }

        /// <summary>
        /// 存盘前校验数据
        /// </summary>
        protected virtual void Validate()
        {



        }





        /// <summary>
        /// 新增数据（自动增加：主表行，清空：主表(Table.Prefix:Master)/明细表(Detail)，不清除其他表(Other)）
        /// </summary>
        public void AddNew()
        {
            if (!this.mAllowAddNew)
                throw new Exception("不允许新增数据");
            if (!this.mAllowAddNew)
                throw new Exception("您没有权限新增数据");

            try
            {
                this.BeforeAddNew();

                if (this.mDsData == null)
                {
                    this.GetData(null,true);
                }
                else
                {
                    foreach (DataTable dt in this.mDsData.Tables)
                    {
                        if (dt.Prefix!="Other")
                            dt.Clear();
                    }
                }



                DataRow dr = this.MasterTable.NewRow();
                this.MasterTable.Rows.Add(dr);

                DataProcess.SetDataRowValueToBlank(this.MasterTable, dr);

                this.AfterAddNew();
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 新增数据前的处理
        /// </summary>
        protected virtual void BeforeAddNew()
        {


        }

        /// <summary>
        /// 新增数据后的处理
        /// </summary>
        protected virtual void AfterAddNew()
        {
            
        }



        /// <summary>
        /// 添加明细表数据行（不带明细表名参数，默认第一个明细表）
        /// </summary>
        /// <returns>新增加的数据行的引用</returns>
        public DataRow AddNew_Detail()
        {
            return this.AddNew_Detail(this.mDsData.Tables[1]);
        }

        /// <summary>
        /// 添加明细表数据行
        /// </summary>
        /// <param name="tableName">明细表</param>
        /// <returns>新增加的数据行的引用</returns>
        public DataRow AddNew_Detail(DataTable dtDetail)
        {
            //明细表的添加需要主表的修改权
            if (!this.mAllowModify)
                throw new Exception("不允许修改数据");
            if (!this.mACLModify)
                throw new Exception("您没有权限修改数据");

            DataRow dr = null;

            try
            {
                this.BeforeAddNew_Detail(dtDetail);

                if (this.mDsData == null)
                {
                    throw new Exception("主表数据不存在，请先检索或添加主表数据");
                }
                else
                {
                    dr = dtDetail.NewRow();
                    dtDetail.Rows.Add(dr);

                    DataProcess.SetDataRowValueToBlank(dtDetail, dr);
                }

                this.AfterAddNew_Detail(dtDetail);
            }
            catch
            {
                throw;
            }

            return dr;
        }


        /// <summary>
        /// 新增明细表数据前的处理
        /// </summary>
        /// <param name="tableName">明细表</param>
        protected virtual void BeforeAddNew_Detail(DataTable dtDetail)
        {


        }

        /// <summary>
        /// 新增明细表数据后的处理
        /// </summary>
        /// <param name="tableName">明细表</param>
        protected virtual void AfterAddNew_Detail(DataTable dtDetail)
        {

        }


        /// <summary>
        /// 删除明细表数据行
        /// </summary>
        /// <param name="dr">数据行</param>
        public void Remove_Detail(DataRow dr)
        {
            //明细表的删除需要主表的修改权
            if (!this.mAllowModify)
                throw new Exception("不允许修改数据");
            if (!this.mACLModify)
                throw new Exception("您没有权限修改数据");

            try
            {
                if (dr != null)
                {
                    if (dr.RowState == DataRowState.Added)
                        dr.RejectChanges();
                    else
                        dr.Delete();
                }
            }
            catch
            {
                throw;
            }
        }



        /// <summary>
        /// 移除（移除后不自动调用Save）
        /// </summary>
        /// <param name="dr">数据行</param>
        public void Remove(DataRow dr)
        {
            if (!this.mAllowDelete)
                throw new Exception("不允许删除数据");
            if (!this.mACLDelete)
                throw new Exception("您没有权限删除数据");

            try
            {
                if (dr != null)
                {
                    if (dr.RowState == DataRowState.Added)
                        dr.RejectChanges();
                    else
                        dr.Delete();
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 删除（调用移除并发送Save）
        /// </summary>
        /// <param name="dr">数据行</param>
        public void Delete(DataRow dr)
        {
            try
            {
                if (dr!=null)
                {
                    this.Remove(dr);

                    if (this.DataHasChanged())
                        this.Save();
                }
            }
            catch
            {
                throw;
            }
        }





        /// <summary>
        /// 导出（先执行基类）
        /// </summary>
        public virtual void ProcessExport()
        {
            if (!this.mAllowExport)
                throw new Exception("不允许导出数据");
            if (!this.mACLExport)
                throw new Exception("您没有导出的权限");
        }




        /// <summary>
        /// 打印（先执行基类）
        /// </summary>
        /// <param name="printMode">true-打印；false-预览</param>
        public virtual void ProcessPrint(bool printMode)
        {
            if (this.DsPrintData != null)
                this.DsPrintData.Dispose();

            if (!this.mAllowPrint)
                throw new Exception("不允许打印数据");

            if (printMode)
            {
                if (!this.mACLPrint)
                    throw new Exception("您没有打印的权限");
            }
        }

        #endregion
    }
}
