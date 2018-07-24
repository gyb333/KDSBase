using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;
/* ==========================================================================
 *  �����ؼ�
 *  
 *  ���ߣ������� (huhaiming@gmail.com)
 *  ���ڣ�2008/08
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
        /// Cellģ��
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
                    throw new InvalidCastException("���Ͳ�����DataGridViewTextBoxCellBase");
                }

                base.CellTemplate = value;
            }
        }

    }
}