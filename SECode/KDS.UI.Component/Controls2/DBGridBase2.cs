using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using C1.Win.C1TrueDBGrid;
using System.Windows.Forms;
using System.Drawing;
/* ==========================================================================
 *  基础控件
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 *==========================================================================*/
namespace KDS.UI.Component
{
    /// <summary>
    /// 数据分析DataGrid
    /// huhm2008
    /// </summary>
    [ToolboxBitmap(typeof(DataGrid))]
    public class DBGridBase2 : C1TrueDBGrid
    {

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBGridBase2));
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // DBGridBase2
            // 
            this.AlternatingRows = true;
            this.Images.Add(((System.Drawing.Image)(resources.GetObject("$this.Images"))));
            this.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow;
            this.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.PreviewInfo.ZoomFactor = 75;
            this.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("DBGridBase2.PrintInfo.PageSettings")));
            this.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Blue;
            this.PropBag = resources.GetString("$this.PropBag");
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        public DBGridBase2()
        {
            this.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow;
            this.PrintInfo.UseGridColors = false;


            this.InitializeComponent();

            this.ShowFilterBar();
        }

        public void ShowFilterBar()
        {
            //Filter
            foreach (C1DataColumn item in this.Columns)
            {
                item.FilterApplyText = "应用";
                item.FilterCancelText = "取消";
                item.FilterClearText = "清除";
                item.FilterDropdown = true;
                item.FilterMultiSelect = true;
            }
        }


        /// <summary>
        /// 自动调整列宽,huhm2008
        /// </summary>
        public void AutoResizeAllColumns()
        {
            foreach (C1DisplayColumn dc in this.Splits[0].DisplayColumns)
            {
                dc.AutoSize();
            }
        }

        /// <summary>
        /// 合并单元格,huhm2008
        /// </summary>
        /// <param name="fromColumnIndex">要合并的列从第几列开始</param>
        /// <param name="toColumnIndex">合并到哪一列结束</param>
        public void MergeCells(int fromColumnIndex,int toColumnIndex)
        {
            int i = 0;
            foreach (C1DisplayColumn dc in this.Splits[0].DisplayColumns)
            {
                if (i >= fromColumnIndex && i <= toColumnIndex)
                {
                    dc.AutoSize();
                    dc.Merge = ColumnMergeEnum.Restricted;
                }
                i++;
            }
        }

        //public ColumnMergeEnum Merge
        //{
        //    get
        //    {
        //        C1DisplayColumnCollection cols = this.Splits[0].DisplayColumns;
        //        return cols[0].Merge;
        //    }
        //    set
        //    {
        //        C1DisplayColumnCollection cols = this.Splits[0].DisplayColumns;
        //        foreach (C1DisplayColumn dc in cols)
        //        {
        //            dc.Merge = value;
        //        }
        //    }
        //}

    }
}
