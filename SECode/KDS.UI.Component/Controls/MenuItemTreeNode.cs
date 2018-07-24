using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
/* ==========================================================================
 *  基础控件
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 *==========================================================================*/
namespace KDS.UI.Component
{
    public class MenuItemTreeNode:TreeNode
    {
        public MenuItemTreeNode():base()
        {
            

        }

        public MenuItemTreeNode(string text)
            : base(text)
        {

        }


        public int AppID;
        public string AppCommand;
        public string DLLName;
        public int SubFuncID;

        public int AuxIntProp1;
        public int AuxIntProp2;

        public string AuxStrProp1;
        public string AuxStrProp2;
    }
}
