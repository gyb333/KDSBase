using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/* ==========================================================================
 *  基础窗体_基类
 *  
 *  作者：胡海明 (huhaiming@gmail.com)
 *  日期：2008/08
 * 
 *  功能：所有窗体的基类，提供基本的： 
 *        1.一致性风格控制；
 *        2.权限基类；
 *==========================================================================*/
namespace KDS.UI.Component
{
    public class UIStyle
    {
        /// <summary>
        /// 窗体背景顔色-浅绿,huhm2008
        /// </summary>
        public static System.Drawing.Color FormBackColor = System.Drawing.Color.FromArgb(227, 241, 254);            //(234, 241, 250); // 浅绿  (191,219,255)   //浅蓝

        /// <summary>
        /// 标题栏深背景顔色-深蓝,huhm2008
        /// </summary>
        public static System.Drawing.Color TitleDeepBackColor = System.Drawing.Color.FromArgb(176, 206, 240); //深蓝
        
        /// <summary>
        /// 标题栏浅背景顔色-浅蓝,huhm2008
        /// </summary>
        public static System.Drawing.Color TitleShallowBackColor = System.Drawing.Color.FromArgb(207, 221, 240); //浅蓝

        /// <summary>
        /// 主窗体背景基色-中蓝,huhm2008
        /// </summary>
        public static System.Drawing.Color MainformBackColor = System.Drawing.Color.FromArgb(204, 216, 253); //中蓝


        /// <summary>
        /// 编辑控件底色-White，huhm2008
        /// </summary>
        public static System.Drawing.Color EditBackColor = System.Drawing.Color.White;


        /// <summary>
        /// 编辑控件Disable底色
        /// </summary>
        public static System.Drawing.Color EditDisableBackColor = System.Drawing.Color.FromArgb(227, 241, 254);


        /// <summary>
        /// 工作区背景色-WhiteSmoke，huhm2008
        /// </summary>
        public static System.Drawing.Color WorkspaceBackColor = System.Drawing.Color.WhiteSmoke;


        /// <summary>
        /// Grid背景色-浅绿，huhm2008
        /// </summary>
        public static System.Drawing.Color GridBackColor = System.Drawing.Color.FromArgb(227, 241, 254);   //System.Drawing.Color.WhiteSmoke;

        /// <summary>
        /// Grid-Readonly时的顔色-浅黄，huhm2008
        /// </summary>
        public static System.Drawing.Color GridDisableCellStyleBackColor = System.Drawing.Color.FromArgb(255, 255, 228);   

    }
}
