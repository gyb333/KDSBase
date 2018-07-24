using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using KDS.Server.App;
using KDS.SECommon;


using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Management;

/***********************************************************
 * 功能：计算注册码
 * 说明： 
 *       
 * 创建：胡海明,huhaiming@gmail.com
 * 时间：2001~2008
  ************************************************************/
namespace KDS.Server.RegKey
{
    public partial class Form1 : Form
    {
        private string OperationPwd
        {
            get
            {
                //分拆，为了应付反编译软件
                string x = "1xdpqJutsX09f74Px".Substring(3, 5);     //pqJut
                string y = "sfsX09f74Pxdsf4fxs".Substring(2, 9);  //sX09f74Px

                return x + y;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGetServerRegKey_Click(object sender, EventArgs e)
        {
            if (this.txtPassword.Text != OperationPwd)
            {
                this.txtServerRegCode.Text = "操作密码错误！";
            }
            else
            {
                this.txtServerRegCode.Text = GetServerRegKey(this.txtServerSN.Text);
            }
        }

        private void btnGetDbEncryptPwd_Click(object sender, EventArgs e)
        {
            if (this.txtPassword.Text != OperationPwd)
            {
                this.txtDbEncryptPwd.Text = "操作密码错误！";
            }
            else
            {
                this.txtDbEncryptPwd.Text = this.GetDbEncryptPwd(this.txtDbOrgPwd.Text);
            }

        }

        /// <summary>
        /// 获取服务注册码
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        private string GetServerRegKey(string sn)
        {
            //计算序列号，huhaiming,2008
            if (sn != "")
                return EncryptHelper.AESEncrypt(sn, "xxK9stU"+ServerGlobalData.RegEncryptKey);
            else
                return "";
        }



        /// <summary>
        /// 获取Db密码
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        private string GetDbEncryptPwd(string orgPwd)
        {
            //计算序列号，huhaiming,2008
            if (orgPwd != "")
                return EncryptHelper.AESEncrypt(orgPwd, ServerGlobalData.DBConnEncryptKey);
            else
                return "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //test
        private void button2_Click(object sender, EventArgs e)
        {
            //string txt=this.GetDbEncryptPwd(this.txtDbOrgPwd.Text);
            //this.txtDbEncryptPwd.Text = txt;

            string orgCode="HuHaiMing test";
            string key="12345678913424234234";

            //MessageBox.Show(EncryptHelper.AESDecrypt(txt, ServerGlobalData.DBConnEncryptKey));
            MessageBox.Show(EncryptHelper.AESDecrypt(EncryptHelper.AESEncrypt(orgCode, key), key));
            //MessageBox.Show(EncryptHelper.DESDecrypt(EncryptHelper.DESEncrypt(orgCode, key), key));
            MessageBox.Show(EncryptHelper.SHA256Encrypt(orgCode));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
#if (DEBUG)
            this.btnTest.Visible=true;
#endif 
        }

    }
}
