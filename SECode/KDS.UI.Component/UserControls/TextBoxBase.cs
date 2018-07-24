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
    [ToolboxBitmap(typeof(TextBox))]
    public class TextBoxBase:TextBox
    {

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TextBoxBase
            // 
            //this.BackColor = UIStyle.EditBackColor;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ResumeLayout(false);

        }

        public TextBoxBase()
        {
            
            this.InitializeComponent();
        }

        //protected override void  OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);

        //    Rectangle r = e.ClipRectangle;
        //    e.Graphics.DrawRectangle(new Pen(Color.Blue, 0), r.X, r.Y, r.Width - 1, r.Height - 1);
        //}  
 
        //重写系统消息
        //警告：严禁随意修改，不小心会耗费系统大量资源
        //作者：huhaiming,2008
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);

            const int WM_PAINT = 0xF;
            
            if (m.Msg == WM_PAINT && this.BorderStyle == System.Windows.Forms.BorderStyle.FixedSingle)
            {
                Graphics g = Graphics.FromHwnd(this.Handle);
                
                g.DrawRectangle(Pens.LightSteelBlue, this.ClientRectangle.Left, this.ClientRectangle.Top, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
               
                g.Dispose();
            }
        } 


    }
}
