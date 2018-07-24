using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.ComponentModel;
using System.Data;

/* ==========================================================================
 *  基础窗体
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 * 
 *  功能：所有窗体的基类，提供基本的： 
 *        1.一致性风格控制；
 *==========================================================================*/
namespace KDS.UI.Component.Forms
{
    /// <summary>
    /// 基础窗体
    /// huhm2008
    /// </summary>
    public class BaseForm: Form
    {
        /// <summary>
        /// 系统状态消息事件
        /// </summary>
        [Category("DataEditEvents")]
        [Description("系统状态消息事件")]
        public event StatusMessageEventHandler OnStatusMessage;

        /// <summary>
        /// 默认的焦点控件
        /// </summary>
        protected TextBox _txtDefaultFocus;

        /// <summary>
        /// WaitWindow实例
        /// </summary>
        private MyWaitWindow myWaitWindow;
        private IContainer components;

        /// <summary>
        /// 窗体子功能ID（可选项，初始-1，需要时请设置>0）（适用于一个窗体执行多个功能时的功能区分，如销量报表包括：1111-品牌分析；1112-客户渠道分析等）
        /// </summary>
        public int FormSubFuncID = -1;


        /// <summary>
        /// 窗体是否移动改变大小
        /// </summary>
        private bool lIsWindowPosChanged=false;
        private bool lIsGridPosChanged = false;
        private bool lIsSaveWindowPos=false;

        [Category("DataEditEvents")]
        [Description("是否存储窗体位置及大小到配置文件")]
        public bool IsSaveWindowPos
        {
            get { return this.lIsSaveWindowPos; }
            set { this.lIsSaveWindowPos = value; }
        }
    
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this._txtDefaultFocus = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _txtDefaultFocus
            // 
            this._txtDefaultFocus.Location = new System.Drawing.Point(-100, -100);
            this._txtDefaultFocus.Name = "_txtDefaultFocus";
            this._txtDefaultFocus.Size = new System.Drawing.Size(100, 21);
            this._txtDefaultFocus.TabIndex = 0;
            this._txtDefaultFocus.TabStop = false;
            // 
            // BaseForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(792, 527);
            this.Controls.Add(this._txtDefaultFocus);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BaseForm";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.BaseForm_Load);
            this.SizeChanged += new System.EventHandler(this.BaseForm_SizeChanged);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BaseForm_FormClosed);
            this.LocationChanged += new System.EventHandler(this.BaseForm_LocationChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void InitControl2()
        {
            //this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            //this.UpdateStyles();
            this.DoubleBuffered = true;
        }

        public BaseForm()
        {
            InitializeComponent();

            this.InitControl2();
        }


        /// <summary>
        /// 设置默认焦点
        /// </summary>
        protected virtual void SetDefaultFocus()
        {
            //SendKeys.Send("{TAB}");
            //SendKeys.Send("+{TAB}");
            this._txtDefaultFocus.Focus();
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {
            this.SetDefaultFocus();

            this.LoadWindowPos();
        }

 
        /// <summary>
        /// 设置系统处理消息
        /// </summary>
        /// <param name="type">1:-系统状态条；2-WaitWindow；3-系统状态条（2秒后自动清除）</param>
        /// <param name="message">消息文本，string.Empty清除消息框及状态条</param>
        /// <param name="progress">进度(0-100)，-1 隐藏进度条</param>
        protected void SetStatusMessage(int type, string message, int progress)
        {
            //参数
            StatusMessageEventArgs args = new StatusMessageEventArgs();
            args.Type = type;
            args.Message = message;
            if (progress >= 0 && message!=string.Empty)
            {
                args.ShowProgressBar = true;
                args.Progress = Math.Min(progress,100);
            }
            else
            {
                args.ShowProgressBar = false;
                args.Progress = -1;
            }

            //Cursor
            if (type != 3)
            {
                if (message != string.Empty)
                    this.Cursor = Cursors.WaitCursor;
                else
                    this.Cursor = Cursors.Default;
            }

            //Type=2，只出现在WaitWindow
            if (type == 2)
            {
                if (myWaitWindow == null)
                {
                    myWaitWindow = new MyWaitWindow();
                }

                if (message != string.Empty)
                {
                    myWaitWindow.ShowMsg(message, args.ShowProgressBar);
                    myWaitWindow.Progress = args.Progress;
                }
                else
                {
                    myWaitWindow.Dispose();
                    myWaitWindow = null;
                }
            }


            //触发OnStatusMessage事件
            if (this.OnStatusMessage != null)
            {
                this.OnStatusMessage(this, args);
            }
        }



        /// <summary>
        /// 显示消息操作窗口
        /// </summary>
        /// <param name="message">消息文本</param>
        /// <param name="waitMilliSeconds">等待毫秒</param>
        protected void PopMessageWindow(string message, int waitMilliSeconds)
        {
            MyMessageWindow.Show(message, waitMilliSeconds);
        }

        /// <summary>
        /// 关闭窗体，清理资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (myWaitWindow != null)
            {
                myWaitWindow.Dispose();
                myWaitWindow = null;
            }

            this.SaveWindowPos();
        }


        #region SaveWindowPos

        /// <summary>
        /// 从配置文件中装载窗体位置大小信息到DataSet
        /// BaseForm基类没使用缓存直接读取XML文件，DataForm基类使用了缓存(重载GetWindowPosDataSet)
        /// </summary>
        /// <returns></returns>
        protected virtual DataSet GetWindowPosDataSetFromXML()
        {
            //加载DS
            DataSet ds = new DataSet("WindowPos");

            DataTable dtForm = new DataTable("FormPos");
            ds.Tables.Add(dtForm);
            dtForm.Columns.Add(new DataColumn("Name", typeof(string)));
            dtForm.Columns.Add(new DataColumn("Left", typeof(int)));
            dtForm.Columns.Add(new DataColumn("Top", typeof(int)));
            dtForm.Columns.Add(new DataColumn("Width", typeof(int)));
            dtForm.Columns.Add(new DataColumn("Height", typeof(int)));
            dtForm.Columns.Add(new DataColumn("WindowState", typeof(int)));
            dtForm.PrimaryKey = new DataColumn[] { dtForm.Columns["Name"] };

            DataTable dtGrid = new DataTable("GridPos");
            ds.Tables.Add(dtGrid);
            dtGrid.Columns.Add(new DataColumn("Name", typeof(string)));
            dtGrid.Columns.Add(new DataColumn("Width", typeof(int)));
            dtGrid.Columns.Add(new DataColumn("Ordinal", typeof(int)));
            dtGrid.PrimaryKey = new DataColumn[] { dtGrid.Columns["Name"] };

            try
            {
                if (System.IO.File.Exists(System.Windows.Forms.Application.StartupPath + "\\WindowsPos_BaseForm.xml"))
                {
                    ds.ReadXml(System.Windows.Forms.Application.StartupPath + "\\WindowsPos_BaseForm.xml");
                }
            }
            catch
            {
                //no catch
            }

            return ds;
        }

        /// <summary>
        /// 将窗体位置大小信息保存到DataSet
        /// BaseForm基类没使用缓存直接读取XML文件，DataForm基类使用了缓存(重载SaveWindowPosDataSet)
        /// </summary>
        /// <param name="dsWindowPos"></param>
        /// <returns></returns>
        protected virtual void SaveWindowPosDataSetToXML(DataSet dsWindowPos)
        {
            try
            {
                if (dsWindowPos.HasChanges())
                    dsWindowPos.WriteXml(System.Windows.Forms.Application.StartupPath + "\\WindowsPos_BaseForm.xml");
            }
            catch
            {
                // no throw
            }
        }

        /// <summary>
        /// 从配置文件中装载窗体表格控件的位置及大小
        /// BaseForm基类没使用缓存直接读取XML文件，DataForm基类使用了缓存
        /// huhm2011
        /// </summary>
        protected void LoadWindowPos()
        {
            if (!this.IsSaveWindowPos)
                return;

            try
            {
                //加载DS
                DataSet dsWindowPos = this.GetWindowPosDataSetFromXML();
                if (dsWindowPos == null)
                    return;

                string formName = this.Name + "[" + this.Text + "]";
                DataTable dtForm = dsWindowPos.Tables["FormPos"];
                DataTable dtGrid = dsWindowPos.Tables["GridPos"];

                //Form大小
                DataRow row = dtForm.Rows.Find(formName);
                if (row != null)
                {
                    int WindowState = (int)row["WindowState"];
                    if (WindowState == 1 && this.WindowState==FormWindowState.Normal)  //0-normal;1-max;-1-min
                    {
                        this.WindowState = FormWindowState.Maximized;
                    }

                    if (WindowState == 0)  //0-normal;1-max;-1-min
                    {
                        this.Left = Math.Max(0, (int)row["Left"]);
                        this.Top = Math.Max(0, (int)row["Top"]);

                        if (this.FormBorderStyle == FormBorderStyle.Sizable)
                        {
                            this.Width = Math.Max(this.Width, (int)row["Width"]);
                            this.Height = Math.Max(this.Height, (int)row["Height"]);
                        }
                    }
                }

                //Grid列宽
                this.RecursiveLoadGridViewPos(formName, dtGrid, this);
            }
            catch (Exception ex)
            {

                //MessageBox.Show("载入窗体状态时发生错误：" + ex.Message, "窗体状态", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void RecursiveLoadGridViewPos(string formName, DataTable dtGrid, Control ctr)
        {
            foreach (Control cFind in ctr.Controls)
            {
                if (cFind.Controls.Count > 0)
                {
                    this.RecursiveLoadGridViewPos(formName, dtGrid, cFind);
                }

                if (cFind is DataGridView)
                {
                    DataGridView gridData = (DataGridView)cFind;
                    gridData.ColumnWidthChanged -= new DataGridViewColumnEventHandler(gridData_ColumnWidthChanged);
                    foreach (DataGridViewColumn column in gridData.Columns)
                    {
                        DataRow row = dtGrid.Rows.Find(formName + "." + gridData.Name + "." + column.Name);
                        if (row != null)
                        {
                            column.Width = Math.Max(0, (int)row["Width"]);
                            int ordinal= (int)row["Ordinal"];
                            if (gridData.AllowUserToOrderColumns && ordinal > 0 && ordinal <= gridData.Columns.Count - 1 && !column.Frozen)
                            {
                                column.DisplayIndex = ordinal;
                            }
                            if (column.Width < 10)
                                column.Visible = false;
                        }
                        else
                        {
                            break; //只要一列找不到则中断
                        }
                    }
                    gridData.ColumnWidthChanged += new DataGridViewColumnEventHandler(gridData_ColumnWidthChanged);
                }
            }
        }


        /// <summary>
        /// 存储窗体表格控件的位置及大小到配置文件中
        /// BaseForm基类没使用缓存直接读取XML文件，DataForm基类使用了缓存
        /// huhm2011
        /// </summary>
        protected void SaveWindowPos()
        {
            if (!this.IsSaveWindowPos)
                return;

            if (!(this.lIsWindowPosChanged || this.lIsGridPosChanged))
                return;

            try
            {
                //加载DS
                DataSet dsWindowPos = this.GetWindowPosDataSetFromXML();
                if (dsWindowPos == null)
                    return;

                string formName = this.Name+"["+this.Text+"]";
                DataTable dtForm = dsWindowPos.Tables["FormPos"];
                DataTable dtGrid = dsWindowPos.Tables["GridPos"];

                //窗体定位
                if (this.lIsWindowPosChanged)
                {
                    DataRow row = dtForm.Rows.Find(formName);
                    if (row == null)
                    {
                        row = dtForm.NewRow();
                        row["Name"] = formName;
                        dtForm.Rows.Add(row);
                    }

                    int WindowState = (this.WindowState == FormWindowState.Maximized ? 1 : 0);
                    row["WindowState"] = WindowState;
                    row["Left"] = this.Left;
                    row["Top"] = this.Top;
                    row["Width"] = this.Width;
                    row["Height"] = this.Height;
                }

                //Grid列宽
                if (this.lIsGridPosChanged)
                    this.RecursiveSaveGridViewPos(formName, dtGrid, this);

                //存储到XML
                this.SaveWindowPosDataSetToXML(dsWindowPos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存窗体状态时发生错误：" + ex.Message, "窗体状态", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void RecursiveSaveGridViewPos(string formName,DataTable dtGrid,Control ctr)
        {
            foreach (Control cFind in ctr.Controls)
            {
                if (cFind.Controls.Count > 0)
                {
                    this.RecursiveSaveGridViewPos(formName, dtGrid, cFind);
                }

                if (cFind is DataGridView)
                {
                    DataGridView gridData = (DataGridView)cFind;
                    foreach (DataGridViewColumn column in gridData.Columns)
                    {
                        string columnName = formName + "." + gridData.Name + "." + column.Name;
                        DataRow row = dtGrid.Rows.Find(columnName);
                        if (row == null)
                        {
                            row = dtGrid.NewRow();
                            row["Name"] = columnName;
                            dtGrid.Rows.Add(row);
                        }
                        row["Width"] = column.Width;
                        row["Ordinal"] = column.DisplayIndex;
                    }
                }
            }
        }



        private void BaseForm_LocationChanged(object sender, EventArgs e)
        {
            this.lIsWindowPosChanged = true;
        }

        private void BaseForm_SizeChanged(object sender, EventArgs e)
        {
            this.lIsWindowPosChanged = true;
        }

        private void gridData_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.lIsGridPosChanged = true;
        }

        #endregion 
    }
}
