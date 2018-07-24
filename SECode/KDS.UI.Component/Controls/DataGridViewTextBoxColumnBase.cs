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
    public class DataGridViewTextBoxColumnBase : DataGridViewColumn
    {
        public DataGridViewTextBoxColumnBase()
            : base(new DataGridViewTextBoxCellBase())
        {
          
        }

        public DataGridViewTextBoxColumnBase(DataGridViewTextBoxCellBase cellTemplate)
            : base(cellTemplate)
        {

        }

        /// <summary>
        /// Cell模版
        /// </summary>
        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }

            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewTextBoxCellBase)))
                {
                    throw new InvalidCastException("类型不属于DataGridViewTextBoxCellBase");
                }

                base.CellTemplate = value;
            }
        }

    }
}