using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.Common;

using Microsoft.Reporting.WinForms;
using System.Reflection;

using KDS.Client.Helper;
using KDS.UI.Component.Forms;
using KDS.UI.Component;

using KDS.Client.App;
using KDS.Client.BLBase;
using KDS.Model;
using KDS.Common;
using C1.Win.C1Preview;
/* ==========================================================================
 *  数据窗体基类
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 *
 *==========================================================================*/
namespace KDS.Client.BaseForms
{
    /// <summary>
    /// 数据处理窗体基类
    /// 提供数据浏览窗体、导航、取数、编辑等公共功能及权限的自动处理
    /// 最终业务类的实现要重载：CreateClientBL()里创建业务对象、在AfterCreateClientBL()绑定数据
    /// Copyrights by CSS 2001~2008, huhaiming@gmail.com
    /// </summary>
    public class DataForm: BaseForm
    {
        /// <summary>
        /// 窗体类型：1-Browse；2.DataEdit
        /// </summary>
        protected int mDataFormType = 1;

        /// <summary>
        /// 窗体打开模式：1-Browse/Edit；2-AddNew
        /// </summary>
        public int OpenFormMode = 1;

        /// <summary>
        /// 是否有子窗体
        /// </summary>
        protected bool mHasSubForm=false;

        /// <summary>
        /// 是否有过滤窗体
        /// </summary>
        protected bool mHasFilterForm = false;


        /// <summary>
        /// 是否有扩展功能
        /// </summary>
        protected bool mHasMoreFunc = false;

        /// <summary>
        /// 数据表的主键名称
        /// </summary>
        protected string mDataKeyName = "";


        /// <summary>
        /// 要使用查找功能时的列名（内存表字段名）
        /// </summary>
        protected string mDataFindFieldName = "";

        protected string mDataFindFieldName2 = "";

        private DataTable mDtData;
        /// <summary>
        /// 与窗体绑定的数据主表
        /// </summary>
        protected DataTable DtData
        {
            get
            {
                return this.mDtData;
            }
            set
            {
                this.mDtData = value;
                this.bindingSource1.DataSource = this.mDtData;
            }
        }

        private HDbParameter mDbParaData;
        /// <summary>
        /// 存放数据过滤条件
        /// </summary>
        public HDbParameter DbParaData
        {
            get
            {
                return this.mDbParaData;
            }
            set
            {
                this.mDbParaData = value;
            }
        }


        /// <summary>
        /// 关联的业务逻辑类
        /// </summary>
        protected ClientBLData mClientBLData;


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
        /// RDLC报表ReportView的窗体对象
        /// </summary>
        protected ReportForm mReportForm;


        /// <summary>
        /// DbGrid.Preview报表ReportView的窗体对象
        /// </summary>
        protected ReportForm2 mReportForm2;

        /// <summary>
        /// 当前用于打印的报表文件名（值如GRID则根据Grid动态打印，否则调用RDLC打印）
        /// </summary>
        protected string mReportFileName="";

        /// <summary>
        /// 报表显示模式
        /// </summary>
        protected DisplayMode mReportFormDisplayMode = DisplayMode.PrintLayout;

        /// <summary>
        /// 报表显示Zoom模式
        /// </summary>
        protected ZoomMode mReportFormZoomMode = ZoomMode.Percent;

        /// <summary>
        /// 报表显示缩放比例；为-1时无效，同时mReportFormZoomMode的设置也无效
        /// </summary>
        protected int mReportFormZoomPercent = -1;

        /// <summary>
        /// 报表显示Zoom模式For C1
        /// </summary>
        protected ZoomModeEnum mReportForm2ZoomMode = ZoomModeEnum.ActualSize;
        /// <summary>
        /// 报表显示缩放比例For C1；为-1时无效,同时mReportForm2ZoomMode的设置也无效
        /// </summary>
        protected int mReportForm2ZoomFactor = -1;

        /// <summary>
        /// 本窗体是否允许打印
        /// </summary>
        protected bool mPrintEnabled = true;

        /// <summary>
        /// 检索数据后是否自动预览
        /// </summary>
        protected bool mIsAutoPreview = false;

        /// <summary>
        /// 是否已执行过打印
        /// </summary>
        protected bool mHasBeenPrinted = false;

        /// <summary>
        /// 相关窗体
        /// </summary>
        protected DataForm mRefForm;

        /// <summary>
        /// 条件窗体
        /// </summary>
        protected FilterForm mFilterForm;

        protected BindingSource bindingSource1;
        protected DataEditToolbar dataEditToolbar1;
        protected ClientApp mClientApp;
        private Button btnClose;

        private System.ComponentModel.IContainer components;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataForm));
            this.dataEditToolbar1 = new KDS.UI.Component.DataEditToolbar();
            this.btnClose = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // _txtDefaultFocus
            // 
            this._txtDefaultFocus.AccessibleDescription = null;
            this._txtDefaultFocus.AccessibleName = null;
            resources.ApplyResources(this._txtDefaultFocus, "_txtDefaultFocus");
            this._txtDefaultFocus.BackgroundImage = null;
            this._txtDefaultFocus.Font = null;
            // 
            // dataEditToolbar1
            // 
            this.dataEditToolbar1.AccessibleDescription = null;
            this.dataEditToolbar1.AccessibleName = null;
            this.dataEditToolbar1.AllowDrop = true;
            resources.ApplyResources(this.dataEditToolbar1, "dataEditToolbar1");
            this.dataEditToolbar1.BackColor = System.Drawing.Color.Transparent;
            this.dataEditToolbar1.BackgroundImage = null;
            this.dataEditToolbar1.DeleteEnabled = true;
            this.dataEditToolbar1.DeleteVisible = false;
            this.dataEditToolbar1.DetailEnabled = true;
            this.dataEditToolbar1.DetailVisible = false;
            this.dataEditToolbar1.ExportEnabled = true;
            this.dataEditToolbar1.ExportVisible = false;
            this.dataEditToolbar1.FilterEnabled = true;
            this.dataEditToolbar1.FilterVisible = false;
            this.dataEditToolbar1.FindEnabled = true;
            this.dataEditToolbar1.FindVisible = false;
            this.dataEditToolbar1.FirstEnabled = true;
            this.dataEditToolbar1.FirstVisible = true;
            this.dataEditToolbar1.Font = null;
            this.dataEditToolbar1.LastEnabled = true;
            this.dataEditToolbar1.LastVisible = true;
            this.dataEditToolbar1.MoreFuncEnabled = true;
            this.dataEditToolbar1.MoreFuncVisible = false;
            this.dataEditToolbar1.Name = "dataEditToolbar1";
            this.dataEditToolbar1.NewEnabled = true;
            this.dataEditToolbar1.NewVisible = false;
            this.dataEditToolbar1.NextEnabled = true;
            this.dataEditToolbar1.NextVisible = true;
            this.dataEditToolbar1.PreviewEnabled = true;
            this.dataEditToolbar1.PreviewVisible = false;
            this.dataEditToolbar1.PrintEnabled = true;
            this.dataEditToolbar1.PrintVisible = false;
            this.dataEditToolbar1.PriorEnabled = true;
            this.dataEditToolbar1.PriorVisible = true;
            this.dataEditToolbar1.RefreshEnabled = true;
            this.dataEditToolbar1.RefreshVisible = false;
            this.dataEditToolbar1.SaveEnabled = true;
            this.dataEditToolbar1.SaveVisible = false;
            // 
            // btnClose
            // 
            this.btnClose.AccessibleDescription = null;
            this.btnClose.AccessibleName = null;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.BackgroundImage = null;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = null;
            this.btnClose.Name = "btnClose";
            this.btnClose.TabStop = false;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSourceChanged += new System.EventHandler(this.bindingSource1_DataSourceChanged);
            // 
            // DataForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.BackgroundImage = null;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dataEditToolbar1);
            this.Font = null;
            this.IsSaveWindowPos = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataForm";
            this.OnStatusMessage += new KDS.UI.Component.Forms.StatusMessageEventHandler(this.DataForm_OnStatusMessage);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataForm_FormClosing);
            this.Controls.SetChildIndex(this._txtDefaultFocus, 0);
            this.Controls.SetChildIndex(this.dataEditToolbar1, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        /// <summary>
        /// 构造函数
        /// </summary>
        public DataForm()
        {
            this.InitializeComponent();
            this.InitControl();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataForm(ClientApp clientApp)
        {
            this.mClientApp = clientApp;

            this.InitializeComponent();
            this.InitControl();
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="clientApp">clientApp</param>
        /// <param name="dataForm">关联窗体</param>
        public DataForm(ClientApp clientApp,DataForm dataForm)
        {
            this.mRefForm = dataForm;
            this.mClientApp = clientApp;

            this.InitializeComponent();
            this.InitControl();
        }

        /// <summary>
        /// 创建ClientBL：（顺序应：1.先设置本类属性；2.再执行基类；）
        /// 最终业务类的实现要设置的有：mHasSubForm、mDataFormType、mDataKeyName、实例化mClientBLData+调用GetFilterCondition()+GetData()
        /// </summary>
        protected virtual void CreateClientBL()
        {
            this.InitButtonStatus();
        }


        #region ButtonEvent

        private void InitControl()
        {
            //Event
            this.dataEditToolbar1.NewClick += new EventHandler(dataEditToolbar1_NewClick);
            this.dataEditToolbar1.DetailClick += new EventHandler(dataEditToolbar1_DetailClick);
            this.dataEditToolbar1.DeleteClick += new EventHandler(dataEditToolbar1_DeleteClick);
            this.dataEditToolbar1.SaveClick += new EventHandler(dataEditToolbar1_SaveClick);
            this.dataEditToolbar1.MoreFuncClick += new EventHandler(dataEditToolbar1_MoreFuncClick);

            this.dataEditToolbar1.FirstClick += new EventHandler(dataEditToolbar1_FirstClick);
            this.dataEditToolbar1.PriorClick += new EventHandler(dataEditToolbar1_PriorClick);
            this.dataEditToolbar1.NextClick += new EventHandler(dataEditToolbar1_NextClick);
            this.dataEditToolbar1.LastClick += new EventHandler(dataEditToolbar1_LastClick);

            this.dataEditToolbar1.FilterClick += new EventHandler(dataEditToolbar1_FilterClick);
            this.dataEditToolbar1.FindClick += new EventHandler(dataEditToolbar1_FindClick);
            this.dataEditToolbar1.RefreshClick += new EventHandler(dataEditToolbar1_RefreshClick);
            this.dataEditToolbar1.ExportClick += new EventHandler(dataEditToolbar1_ExportClick);

            this.dataEditToolbar1.PreviewClick += new EventHandler(dataEditToolbar1_PreviewClick);
            this.dataEditToolbar1.PrintClick += new EventHandler(dataEditToolbar1_PrintClick);

            this.dataEditToolbar1.CloseClick += new EventHandler(dataEditToolbar1_CloseClick);
        }

        void dataEditToolbar1_FilterClick(object sender, EventArgs e)
        {
            try
            {
                this.GetFilterData();

                if (this.mIsAutoPreview)
                {
                    this.dataEditToolbar1_PreviewClick(sender, e);
                }
            }
            catch (Exception ex)
            {
                this.CatchException(ex);
            }
        }

        void dataEditToolbar1_DeleteClick(object sender, EventArgs e)
        {
            try
            {
                this.Delete();                
            }
            catch (Exception ex)
            {
                this.CatchException(ex);
            }

        }

        void dataEditToolbar1_SaveClick(object sender, EventArgs e)
        {
            try
            {
                this.Save();
            }
            catch (Exception ex)
            {
                this.CatchException(ex);
            }
        }

        void dataEditToolbar1_CloseClick(object sender, EventArgs e)
        {
            this.Close();
        }

        void dataEditToolbar1_ExportClick(object sender, EventArgs e)
        {
            try
            {
                this.Export();
            }
            catch (Exception ex)
            {
                this.CatchException(ex);
            }
        }

        void dataEditToolbar1_RefreshClick(object sender, EventArgs e)
        {
            try
            {
                this.GetData();
            }
            catch (Exception ex)
            {
                this.CatchException(ex);
            }
        }

        void dataEditToolbar1_LastClick(object sender, EventArgs e)
        {
            try
            {
                this.ChangePosition(4);
            }
            catch (Exception ex)
            {
                this.CatchException(ex);
            }
        }

        void dataEditToolbar1_NextClick(object sender, EventArgs e)
        {
            try
            {
                this.ChangePosition(3);
            }
            catch (Exception ex)
            {
                this.CatchException(ex);
            }
        }

        void dataEditToolbar1_PriorClick(object sender, EventArgs e)
        {
            try
            {
                this.ChangePosition(2);
            }
            catch (Exception ex)
            {
                this.CatchException(ex);
            }
        }

        void dataEditToolbar1_FirstClick(object sender, EventArgs e)
        {
            try
            {
                this.ChangePosition(1);
            }
            catch (Exception ex)
            {
                this.CatchException(ex);
            }
        }

        void dataEditToolbar1_DetailClick(object sender, EventArgs e)
        {
            try
            {
                this.OpenDetailForm(1);
            }
            catch (Exception ex)
            {
                this.CatchException(ex);
            }
        }


        void dataEditToolbar1_NewClick(object sender, EventArgs e)
        {
            try
            {
                if (this.mDataFormType == 1)
                    this.OpenDetailForm(2);
                else
                    this.AddNew();
            }
            catch (Exception ex)
            {
                this.CatchException(ex);
            }
        }

        void dataEditToolbar1_PrintClick(object sender, EventArgs e)
        {
            try
            {
                this.Print(true);
                this.AfterPrint(true);
            }
            catch (Exception ex)
            {
                this.CatchException(ex);
            }
        }

        void dataEditToolbar1_PreviewClick(object sender, EventArgs e)
        {
            try
            {
                this.Print(false);
                this.AfterPrint(false);
            }
            catch (Exception ex)
            {
                this.CatchException(ex);
            }
        }

        void dataEditToolbar1_FindClick(object sender, EventArgs e)
        {
            try
            {
                this.Find();
            }
            catch (Exception ex)
            {
                this.CatchException(ex);
            }
        }

        void dataEditToolbar1_MoreFuncClick(object sender, EventArgs e)
        {
            try
            {
                this.DoMoreFunc(sender, e);
            }
            catch (Exception ex)
            {
                this.CatchException(ex);
            }
        }

        #endregion 

        #region Refresh

        /// <summary>
        /// 初始化按钮状态
        /// </summary>
        private void InitButtonStatus()
        {
            this.SuspendLayout();

            bool isExistSubForm = this.mHasSubForm; // (this.mSubForm != null);

            //Visible
            this.dataEditToolbar1.NewVisible = ((this.mClientBLData.AllowAddNew) && ((this.mDataFormType == 1 && isExistSubForm) || (this.mDataFormType==2) )) ;
            this.dataEditToolbar1.DetailVisible = (this.mDataFormType == 1 && isExistSubForm);
            this.dataEditToolbar1.DeleteVisible = ((this.mDataFormType == 2) && (this.mClientBLData.AllowDelete)) ;
            this.dataEditToolbar1.SaveVisible = ((this.mDataFormType == 2) && (this.mClientBLData.AllowAddNew || this.mClientBLData.AllowModify)) ;
            this.dataEditToolbar1.MoreFuncVisible = this.mHasMoreFunc;

            this.dataEditToolbar1.FirstVisible = ( (this.mDataFormType==1) || (this.mDataFormType == 2 && this.mRefForm!=null));
            this.dataEditToolbar1.PriorVisible = ( (this.mDataFormType==1) || (this.mDataFormType == 2 && this.mRefForm!=null));
            this.dataEditToolbar1.NextVisible = ( (this.mDataFormType==1) || (this.mDataFormType == 2 && this.mRefForm!=null));
            this.dataEditToolbar1.LastVisible = ( (this.mDataFormType==1) || (this.mDataFormType == 2 && this.mRefForm!=null));

            this.dataEditToolbar1.FilterVisible = this.mHasFilterForm;
            this.dataEditToolbar1.FindVisible = (mDataFindFieldName != string.Empty);
            this.dataEditToolbar1.RefreshVisible = true;
            this.dataEditToolbar1.ExportVisible = this.mClientBLData.AllowExport;

            this.dataEditToolbar1.PreviewVisible = this.mPrintEnabled && this.mClientBLData.AllowPrint;
            this.dataEditToolbar1.PrintVisible = this.mPrintEnabled && this.mClientBLData.AllowPrint;

            //Enabled
            this.dataEditToolbar1.NewEnabled = this.mClientBLData.ACLAddNew;
            this.dataEditToolbar1.DetailEnabled = true;
            this.dataEditToolbar1.DeleteEnabled = this.mClientBLData.ACLDelete;
            this.dataEditToolbar1.SaveEnabled = (this.mClientBLData.ACLAddNew || this.mClientBLData.ACLModify);
            this.dataEditToolbar1.MoreFuncEnabled = true;

            this.dataEditToolbar1.FirstEnabled = true;
            this.dataEditToolbar1.PriorEnabled = true;
            this.dataEditToolbar1.NextEnabled = true;
            this.dataEditToolbar1.LastEnabled = true;

            this.dataEditToolbar1.FindEnabled = true;
            this.dataEditToolbar1.RefreshEnabled = true;
            this.dataEditToolbar1.ExportEnabled = this.mClientBLData.ACLExport;

            this.dataEditToolbar1.PreviewEnabled = this.mClientBLData.ACLPrint;
            this.dataEditToolbar1.PrintEnabled = this.mClientBLData.ACLPrint;

            this.ResumeLayout(false);
        }


        /// <summary>
        /// 刷新窗体
        /// </summary>
        protected virtual void RefreshForm()
        {
            this.Refresh();
        }

        #endregion 

        #region 数据处理

        /// <summary>
        /// 检查业务对象是否创建
        /// </summary>
        /// <returns></returns>
        protected void CheckClientBLDataObject()
        {
            if (this.mClientBLData == null)
            {
                throw new Exception("数据窗体-业务对象仍未初始化。");
            }
        }



        /// <summary>
        /// 获取当前记录键值
        /// </summary>
        /// <returns></returns>
        public virtual object GetKeyValue()
        {
            if (this.mDataKeyName == "")
            {
                throw new Exception("请设置主键。");
            }

            if (this.bindingSource1.Current == null)
                return null;
            else
            {
                return ((DataRowView)this.bindingSource1.Current)[DataProcess.TransToColumnName(this.mDataKeyName)];
            }

            //if (this.mDtData == null || this.mDataKeyName == "" || this.mDtData.Rows.Count == 0)
            //    return null;
            //else
            //{
            //    return this.mDtData.Rows[this.bindingSource1.Position][this.mDataKeyName];
            //} 
        }


        /// <summary>
        /// 根据当前行值设置主键参数
        /// </summary>
        /// <param name="valueType">1-按本窗体取参数值；2按关联mRefForm窗体取参数值；</param>
        protected virtual void SetKeyPara(int valueType)
        {
            this.mDbParaData = null;

            if (this.mDataKeyName == "") 
            {
                throw new Exception("请设置主键。");
            }

            if ( (valueType == 1 && this.mClientBLData == null) || (valueType == 2 && this.mRefForm == null))
                throw new Exception("未创建业务对象或为空。");

            try
            {
                object key=null;
                if (valueType == 1)
                {
                    key = this.GetKeyValue(); 
                }
                else
                {
                    key = this.mRefForm.GetKeyValue();
                }

                if (key != null)
                {
                    this.mDbParaData = new HDbParameter();
                    this.mDbParaData.Paras.Add(this.mDataKeyName, key);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取主表的当前数据行
        /// </summary>
        protected DataRow GetCurrentDataRow()
        {
            if (this.bindingSource1.Current==null)
                return null;
            else
                return ((DataRowView)this.bindingSource1.Current).Row;
        }


        /// <summary>
        /// 打开条件对话框并获取设置条件后的数据
        /// </summary>
        protected void GetFilterData()
        {
            try
            {
                if (this.GetFilterCondition())
                {
                    if (this.mClientBLData != null)
                        this.mClientBLData.TotalPage = -1; //重新确定分页

                    this.GetData();
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 获取用户的过滤条件，设置this.DbParaData为取数据做准备
        /// </summary>
        protected virtual bool GetFilterCondition()
        {
            return true;
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        protected virtual void GetData()
        {
            try
            {
                this.CheckClientBLDataObject();

                if (this.mClientBLData != null)
                {
                    if (this.mDataFormType == 1)
                    {
                        this.SetStatusMessage(2, "正在从服务器检查数据，请稍后...", -1);

                        try
                        {
                            this.mClientBLData.GetBrowseData(this.mDbParaData);
                            this.DtData = this.mClientBLData.DtBrowseData;
                        }
                        finally
                        {
                            this.SetStatusMessage(2, "", -1);
                        }

                    }
                    else
                    {
                        this.mClientBLData.GetData(this.mDbParaData, false);
                        this.DtData = this.mClientBLData.MasterTable;
                    }

                    this.RefreshForm();
                }
            }
            catch
            {
                throw;
            }
        }



        /// <summary>
        /// 变更单据状态
        /// </summary>
        /// <param name="dbPara">参数（如单号，单据状态）</param>
        protected virtual void ChangeStatus(HDbParameter dbPara)
        {
            this.SetStatusMessage(1, "正在提交更新到服务器，请稍后...", -1);

            try
            {
                this.CheckClientBLDataObject();
                if (!this.CanLeave())
                    return;

                this.mClientBLData.ChangeStatus(dbPara);

                this.GetData();
            }
            catch
            {
                throw;
            }
            finally
            {
                this.SetStatusMessage(1, "", -1);
            }

            this.SetStatusMessage(3, "操作已成功", -1);
        }


        /// <summary>
        /// 改变位置
        /// </summary>
        /// <param name="type">1:First,2:Prior,3:Next,4:Last,5:AfterDeleteMove(Pre+Next)</param>
        public void ChangePosition(int type)
        {
            if ((this.mDataFormType == 2 && this.mRefForm==null) || this.mClientBLData == null)
                return ;

            try
            {
                BindingSource bindSource;
                if (this.mDataFormType == 2)
                {
                    if (!this.CanLeave())
                        return;

                    bindSource = this.mRefForm.bindingSource1;
                }
                else
                    bindSource = this.bindingSource1;

                switch (type)
                {
                    case 1:
                        bindSource.MoveFirst();
                        break;
                    case 2:
                        bindSource.MovePrevious();
                        break;
                    case 3:
                        bindSource.MoveNext();
                        break;
                    case 4:
                        bindSource.MoveLast();
                        break;
                    case 5:
                        if (bindSource.Position == bindSource.Count - 1)
                            bindSource.MovePrevious();
                        else
                            bindSource.MoveNext();
                        break;
                }

                if (this.mDataFormType == 2)
                {
                    //this.mRefForm.RefreshForm();

                    //object key = this.mRefForm.GetKeyValue();
                    //if (key != null)
                    //{
                    //    this.mDbParaData = new HDbParameter();
                    //    this.mDbParaData.Paras.Add(this.mDataKeyName, key);
                    //    this.GetData();
                    //}

                    this.SetKeyPara(2);
                    if (this.mDbParaData!=null)
                    {
                        this.GetData();
                    }
                }
                else
                {
                    //this.RefreshForm();
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 装载报表定义（报表类型ReportFormType = 1_RDLC的情况下）
        /// </summary>
        /// <param name="assemblyType">报表文件所在程序集的类的类型</param>
        /// <param name="reportFileName">报表文件名称：本地文件应带有文件路径如Data\Report\XXXX.rdlc；集成编译报表文件应描述报表完整路径，如：KDS.Client.KDSUI.SD.XXXXX.rdlc</param>
        protected void LoadReportDefinition(Type assemblyType, string reportFileName)
        {
            if (this.mReportForm == null)
                return;

            if (reportFileName.IndexOf("\\") < 0)
            {
                //this.mReportForm.reportViewer1.LocalReport.ReportPath = @"D:\HHM\Document\DEV\Project\KDS2\Code\Client\KDS.Client.KDSUI\SD\SEOrderRpt_STD.rdlc";
                //this.mReportForm.reportViewer1.LocalReport.ReportEmbeddedResource = this.mReportFileName;
                Assembly currentAssembly = System.Reflection.Assembly.GetAssembly(assemblyType);
                this.mReportForm.reportViewer1.LocalReport.LoadReportDefinition(currentAssembly.GetManifestResourceStream(reportFileName));
            }
            else
            {
                this.mReportForm.reportViewer1.LocalReport.ReportPath = reportFileName;
            }
        }

        /// <summary>
        /// 获取报表文件名（不用执行基类）（报表类型ReportFormType = 1_RDLC的情况下才调用此方法）
        /// </summary>
        /// <returns>报表文件名（文件名含"\"表示本地加载，否则从DLL中加载。约定：1.返回为"GRID"根据表格动态打印,2.""则放弃本次打印,3."NONE"未设置打印格式</returns>
        protected virtual string GetReportFileName(bool lPrintMode)
        {
            return "";
        }

        /// <summary>
        /// 执行打印（继承后可重载），派生类中可重写不执行此基类代码
        /// 1.窗体是RDLC的情况下，必须加载数据源，可对this.mReportForm.reportViewer1对象等进行再次处理
        /// 2.窗体是DbGrid.Preivew情况下，必须对this.mReportForm2.SetPreviewObject(this.dbGrid2)作处理
        /// </summary>
        /// <param name="lPrintMode">true:Print; false:Preview</param>
        protected virtual void DoPrint(bool lPrintMode)
        {

        }

        /// <summary>
        /// 执行打印后的处理，先执行代码再执行基类
        /// </summary>
        /// <param name="lPrintMode"></param>
        protected virtual void AfterPrint(bool lPrintMode)
        {

        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="lPrintMode">true:Print; false:Preview</param>
        protected void Print(bool lPrintMode)
        {
            this.mHasBeenPrinted = false;

            this.CheckClientBLDataObject();
            if (!this.CanLeave())
                return;

            try
            {
                if (lPrintMode && (!this.mPrintEnabled || !this.mClientBLData.AllowPrint || !this.mClientBLData.ACLPrint))
                {
                    throw new Exception("您没权限或系统不允许打印。");
                }

                string cReportFileName=this.GetReportFileName(lPrintMode);

                //放弃打印
                if (cReportFileName == string.Empty)
                    return ;

                if (cReportFileName == "NONE")
                    throw new Exception("未设置默认打印格式，请点击预览，然后设置默认打印格式。");

                this.SetStatusMessage(2, "正在准备打印数据，请稍后 ...", -1);

                this.mReportFileName = cReportFileName;

                //RDLC preview
                if (this.mReportFileName!="GRID" && this.mReportFileName.ToLower().IndexOf(".xml")==-1)
                {
                    this.mClientBLData.ProcessPrint(lPrintMode);

                    //创建报表Preview窗体
                    this.mReportForm = new ReportForm();

                    this.mReportForm.Text = this.Text + " - 打印预览";
                    this.mReportForm.EnabledExport = (this.mClientBLData.AllowExport && this.mClientBLData.ACLExport);
                    this.mReportForm.EnabledPrint = (this.mClientBLData.AllowPrint && this.mClientBLData.ACLPrint);

                    //执行报表处理（可对数据源，对ReportViewer对象等进行再次处理）
                    this.DoPrint(lPrintMode);

                    //执行报表处理后的处理
                    //检查数据
                    if (this.mReportForm.reportViewer1.LocalReport.DataSources.Count == 0 || this.mClientBLData.DsPrintData == null)
                        throw new Exception("打印失败：没有可供打印的数据");
                    this.mReportForm.reportViewer1.RefreshReport();
                    if (!lPrintMode)
                    {
                        //预览
                        this.mReportForm.reportViewer1.SetDisplayMode(this.mReportFormDisplayMode);
                        if (this.mReportFormZoomPercent != -1)
                        {
                            this.mReportForm.reportViewer1.ZoomMode = this.mReportFormZoomMode;
                            this.mReportForm.reportViewer1.ZoomPercent = this.mReportFormZoomPercent;
                        }
                        this.mReportForm.reportViewer1.ShowPrintButton = false;
                        this.mReportForm.ShowDialog(this);
                    }
                    else
                    {
                        //打印
                        //暂不可以加，会有异常this.mReportForm.reportViewer1.RefreshReport();  //暂不可以加，会有异常 解决第二次打印只看到1页的BUG
                        
                        //直接可打印，不用再次点打印按钮
                        this.mReportForm.ShowPrint(this, lPrintMode);
                    }

                    this.mHasBeenPrinted = this.mReportForm.HasBeenPrinted;
                }

                //RDLC preview
                if (this.mReportFileName.ToLower().IndexOf(".xml") > 0)
                {
                    this.mClientBLData.ProcessPrint(lPrintMode);

                    //创建报表Preview窗体
                    this.mReportForm2 = new ReportForm2();

                    this.mReportForm2.Text = this.Text + " - 打印预览";
                    this.mReportForm2.EnabledExport = (this.mClientBLData.AllowExport && this.mClientBLData.ACLExport);
                    this.mReportForm2.EnabledPrint = (this.mClientBLData.AllowPrint && this.mClientBLData.ACLPrint);

                    //执行报表处理（可对数据源，对ReportViewer对象等进行再次处理）
                    this.DoPrint(lPrintMode);

                    if (!lPrintMode)  //Print时不显示对话框
                    {
                        if (this.mReportForm2ZoomFactor != -1)
                        {
                            this.mReportForm2.c1PrintPreviewControl1.PreviewPane.ZoomMode = this.mReportForm2ZoomMode;
                            this.mReportForm2.c1PrintPreviewControl1.PreviewPane.ZoomFactor = this.mReportForm2ZoomFactor;
                        }
                        this.mReportForm2.ShowDialog(this);
                    }

                    this.mHasBeenPrinted = this.mReportForm2.HasBeenPrinted;
                }


                //DbGrid.Preview
                if (this.mReportFileName=="GRID")  
                {
                    this.mClientBLData.ProcessPrint(lPrintMode);

                    //创建报表Preview窗体
                    this.mReportForm2 = new ReportForm2();

                    this.mReportForm2.Text = this.Text + " - 打印预览";
                    this.mReportForm2.EnabledExport = (this.mClientBLData.AllowExport && this.mClientBLData.ACLExport);
                    this.mReportForm2.EnabledPrint = (this.mClientBLData.AllowPrint && this.mClientBLData.ACLPrint);

                    //执行报表处理（可对数据源，对ReportViewer对象等进行再次处理）
                    this.DoPrint(lPrintMode);
                    
                    this.mReportForm2.ShowDialog(this);

                    this.mHasBeenPrinted = this.mReportForm2.HasBeenPrinted;
                } // end of if (this.mReportFormType == ...
            }
            catch
            {
                throw;
            }
            finally
            {
                this.SetStatusMessage(2, "", -1);

                if (this.mReportForm != null)
                    this.mReportForm.Dispose();
                if (this.mReportForm2 != null)
                    this.mReportForm2.Dispose();
            }
        }


        /// <summary>
        /// 执行导出（继承后可重载），系统默认按XML格式导出。派生类中可重写不执行此基类代码，使用自己的方式导出
        /// </summary>
        protected virtual void DoExport()
        {
            if (this.mClientBLData.DtExportData != null)
            {
                SaveFileDialog fileDialog = new SaveFileDialog();
                fileDialog.DefaultExt = "XLS";
                fileDialog.Filter = "Excel files(*.xls)|*.xls|XML files(*.xml)|*.xml";
                if (fileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    string fileName = fileDialog.FileName;
                    if (fileName.Substring(fileName.Length - 4, 4).ToLower() == ".xml")
                        this.mClientBLData.DtExportData.WriteXml(fileName, XmlWriteMode.IgnoreSchema);
                    else
                        ExcelHelper.ExportToExcel(this.mClientBLData.DtExportData, fileName);
                    //KDS.Common.ExcelHelper.ExportToExcel(this.mClientBLData.DtExportData, "导出", fileDialog.FileName);
                }
            }
            else
            {
                throw new Exception("当前无可供导出的数据。");
            }

        }


        /// <summary>
        /// 导出
        /// </summary>
        protected void Export()
        {
            try
            {
                this.CheckClientBLDataObject();
                if (!this.CanLeave())
                    return;
                this.mClientBLData.ProcessExport();

                this.DoExport();
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 查找（定位）
        /// </summary>
        protected virtual void Find()
        {
            try
            {
                if (!(this.mDataFindFieldName == ""))
                {
                    this.CheckClientBLDataObject();
                    if (this.CanLeave())
                    {
                        string strCode = MyInputBox.Show("查找定位功能用于定位到相关数据行，请输入代码[" + this.mDataFindFieldName + "]：", "");
                        if (strCode != string.Empty)
                        {
                            int pos = this.bindingSource1.Find(this.mDataFindFieldName, strCode);
                            if (pos >= 0)
                            {
                                this.bindingSource1.Position = pos;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(this.mDataFindFieldName2))
                                {
                                    throw new Exception("未找到指定的资料。");
                                }
                                pos = this.bindingSource1.Find(this.mDataFindFieldName2, strCode);
                                if (pos < 0)
                                {
                                    throw new Exception("未找到指定的资料。");
                                }
                                this.bindingSource1.Position = pos;
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// DoMoreFunc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void DoMoreFunc(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 打开明细子窗体
        /// </summary>
        /// <param name="openFormMode">1-查看/修改模式；2-新增</param>
        protected virtual void OpenDetailForm(int openFormMode)
        {

        }

        /// <summary>
        /// 新增记录（主表）
        /// </summary>
        protected virtual void AddNew()
        {
            try
            {
                this.CheckClientBLDataObject();

                if (!this.CanLeave())
                    return;

                this.mClientBLData.AddNew();
                this.DtData = this.mClientBLData.MasterTable;
                this.RefreshForm();
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// 存盘
        /// </summary>
        protected virtual void Save()
        {
            this.SetStatusMessage(1, "正在检测数据是否修改，如有更新将提交到服务器，请稍后...", -1);
            try
            {
                this.CheckClientBLDataObject();

                this.SetDefaultFocus();
                this.EndEdit();

                this.mClientBLData.Save();
                this.DtData = this.mClientBLData.MasterTable;

                //object key = this.GetKeyValue();
                //if (key != null)
                //{
                //    this.mDbParaData = new HDbParameter();
                //    this.mDbParaData.Paras.Add(this.mDataKeyName, key);
                //}
                this.SetKeyPara(1);

                this.RefreshForm();
            }
            catch 
            {
                throw;
            }
            finally
            {
                this.SetStatusMessage(1, "", -1);
            }

            this.SetStatusMessage(3, "数据已保存", -1);
        }


        /// <summary>
        /// 恢复更改
        /// </summary>
        protected virtual void Undo()
        {
            if (this.mClientBLData != null)
            {
                this.CancelEdit();
                this.mClientBLData.RejectChanges();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        protected virtual bool Delete()
        {
            bool retVal = true;

            try
            {
                this.CheckClientBLDataObject();

                if (this.bindingSource1.Current != null)
                {
                    if (MessageBox.Show("确认要删除当前数据记录吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DataRow dr = this.GetCurrentDataRow();
                        this.mClientBLData.Delete(dr);
                        this.ChangePosition(5);
                        this.RefreshForm();
                    }
                    else
                    {
                        retVal = false;
                    }
                }
            }
            catch 
            {
                retVal = false;
                throw;
            }

            if (retVal)
                this.SetStatusMessage(3, "操作已成功", -1);

            return retVal;
        }


        /// <summary>
        /// 可否移动Position记录号
        /// </summary>
        /// <returns></returns>
        protected bool CanLeave()
        {
            if (this.mClientBLData == null)
                return true;

            bool retVal = true;

            try
            {
                this.SetDefaultFocus();
                this.EndEdit();

                if (this.mClientBLData.DataHasChanged())
                {
                    DialogResult result = MessageBox.Show("数据已被修改，是否提交到服务器？", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (result)
                    {
                        case DialogResult.Yes:
                            this.Save();
                            break;
                        case DialogResult.No:
                            this.Undo();
                            break;
                        default:
                            retVal = false;
                            break;
                    }
                }
            }
            catch 
            {
                retVal = false;
                throw;
            }

            return retVal;
        }


        /// <summary>
        /// 捕获异常
        /// </summary>
        /// <param name="ex"></param>
        protected virtual void CatchException(Exception ex)
        {
            MyMessageBox.Show(this, "处理数据时出现错误。", ExceptionMessageHelper.Trans(ex));
        }


        #endregion 

        private void DataForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!this.CanLeave())
                    e.Cancel = true;

                if (this.mClientBLData != null)
                    this.mIsDataChangedAfterBLCreated = this.mClientBLData.IsDataChangedAfterBLCreated;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                this.CatchException(ex);
            }

            //释放资源
            if (this.mRefForm != null && this.mDataFormType==1)
                this.mRefForm.Dispose();

            if (this.mFilterForm != null)
                this.mFilterForm.Dispose();

            //if (this.bindingSource1 != null)
            //    this.bindingSource1.Dispose();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// 结束编辑写入缓冲
        /// </summary>
        protected virtual void EndEdit()
        {
            try
            {
                //this.SetDefaultFocus(); --huhm:注释掉防止对界面层干扰
                this.bindingSource1.EndEdit();
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 取消编辑回退缓冲
        /// </summary>
        protected virtual void CancelEdit()
        {
            //this.SetDefaultFocus();  --huhm:注释掉防止对界面层干扰
            this.bindingSource1.CancelEdit();
        }

        /// <summary>
        /// 数据源发生变化时的处理
        /// </summary>
        protected virtual void DataSourceChanged()
        {
            //在派生类中重写此方法
            
        }

        private void bindingSource1_DataSourceChanged(object sender, EventArgs e)
        {
            this.DataSourceChanged();
        }

        //StatusMessage事件
        private void DataForm_OnStatusMessage(object sender, StatusMessageEventArgs e)
        {
            //如果是编辑子窗体传递事件给父窗体
            if (this.mDataFormType == 2 && this.mRefForm != null)
            {
                this.mRefForm.SetStatusMessage(e.Type, e.Message, e.Progress);
            }
        }


        #region SaveWindowPos

        /// <summary>
        /// 从配置文件中装载窗体位置大小信息到DataSet
        /// BaseForm基类没使用缓存直接读取XML文件，DataForm基类使用了缓存(重载GetWindowPosDataSet)
        /// </summary>
        /// <returns></returns>
        protected override DataSet GetWindowPosDataSetFromXML()
        {
            //由于DataForm使用缓存，不调用基类的直接每次调出XML方案；
            //return base.GetWindowPosDataSetFromXML();

            if (this.mClientApp != null)
                return this.mClientApp.GetWindowPosDataSet();
            else
                return null;
        }

        /// <summary>
        /// 将窗体位置大小信息保存到DataSet
        /// BaseForm基类没使用缓存直接读取XML文件，DataForm基类使用了缓存(重载SaveWindowPosDataSet)
        /// </summary>
        /// <param name="dsWindowPos"></param>
        /// <returns></returns>
        protected override void SaveWindowPosDataSetToXML(DataSet dsWindowPos)
        {
            //由于DataForm使用缓存，不调用基类的直接写入到XML方案：base.SaveWindowPosDataSet(dsWindowPos);
            //MainForm调用了 this.mClientApp.SaveWindowPosToXML(); //add by huhm,2011/06/02

            //nodefault
            //base.SaveWindowPosDataSetToXML(dsWindowPos);
        }

        #endregion 

    }
}
