using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    [ToolboxBitmap(typeof(CheckedListBox))]
    public class CheckedListBoxBase:CheckedListBox
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CheckedListBoxBase
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CheckOnClick = true;
            this.ResumeLayout(false);

        }

        public CheckedListBoxBase()
        {
            this.InitializeComponent();

            if (!this.Enabled)
                this.BackColor = UIStyle.EditDisableBackColor;
        }

        /// <summary>
        /// 调整选择条目的顺序
        /// </summary>
        /// <param name="type">1-Up;2-Down</param>
        public void AdjustSelectedItemPos(int type)
        {
            if (this.SelectedItem == null)
                return;

            object selectItem = this.SelectedItem;
            int selectItemIndex = this.SelectedIndex;
            CheckState checkState = this.GetItemCheckState(selectItemIndex);

            if (type == 1)
            {
                if (selectItemIndex > 0)
                {
                    this.Items.RemoveAt(selectItemIndex);
                    this.Items.Insert(selectItemIndex - 1, selectItem);
                    this.SelectedItem = selectItem;
                    this.SetItemCheckState(selectItemIndex - 1, checkState);
                }
            }
            else
            {
                if (selectItemIndex < this.Items.Count - 1)
                {
                    this.Items.RemoveAt(selectItemIndex);
                    this.Items.Insert(selectItemIndex + 1, selectItem);
                    this.SelectedItem = selectItem;
                    this.SetItemCheckState(selectItemIndex + 1, checkState);
                }
            }
        }


        //重写系统消息
        //警告：严禁随意修改，不小心会耗费系统大量资源
        //作者：huhaiming,2008
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);

            const int WM_PAINT = 0xF;

            if (m.Msg == WM_PAINT && this.BorderStyle == System.Windows.Forms.BorderStyle.None)
            {
                Graphics g = Graphics.FromHwnd(this.Handle);

                g.DrawRectangle(Pens.LightSteelBlue, this.ClientRectangle.Left, this.ClientRectangle.Top, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);

                g.Dispose();
            }
        } 
    }
}
