using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Management;
/***********************************************************
 * 功能：加密/解密
 * 说明：
 *       1.哈希算法，请使用SHA256/512算法，不推荐 MD4 和 MD5 算法
 *       2.对称算法，推荐AES(Rijndael)，它支持128、192和 256 位，不推荐 DES 算法
 *       3.256密钥具有更高的安全性，在目前的计算机速度暴力破解难度非常大
 *       4.保护好密钥才是主要的，huhaiming,2008
 *       
 *       5. //DEBUG: AES算法还有BUG，待改进
 * 
 * 创建：胡海明,huhaiming@gmail.com
 * 时间：2008/08
 ************************************************************/
namespace KDS.SECommon
{
    public class EncryptHelper
    {
        //未提供私有密钥时的默认密钥
        private static string KEY2
        {
            get
            {
                string s1 = @"8$#^HHM@GmaIl";
                string s2 = @"*coM^&(BFW!`Wl!lJx9+07A&(3DB281D36AA45)x7%(3x&%^*()KHGT";
                return s1+s2;
            }
        }

        #region MD5算法
        public static string MD5Encrypt(string strIN)
        {
            byte[] tmpByte;
            MD5 md5 = new MD5CryptoServiceProvider();
            tmpByte = md5.ComputeHash(GetKeyByteArray(strIN));
            md5.Clear();

            return GetStringValue(tmpByte,true);
        }

        #endregion 

        #region SHA哈希

        /// <summary>
        /// SHA1Managed 哈希算法-160 位
        /// huhm2008
        /// </summary>
        /// <param name="plainText">原字符串</param>
        /// <returns>加密后字符串</returns>
        public static string SHA1Encrypt(string plainText)
        {
            byte[] tmpByte;
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            tmpByte = sha1.ComputeHash(GetKeyByteArray(plainText));
            sha1.Clear();

            return GetStringValue(tmpByte, true);
        }

        /// <summary>
        /// SHA1Managed 哈希算法-256位
        /// huhm2008
        /// </summary>
        /// <param name="plainText">原字符串</param>
        /// <returns>加密后字符串</returns>
        public static string SHA256Encrypt(string plainText)
        {
            byte[] tmpByte;
            SHA256 sha256 = new SHA256Managed();

            tmpByte = sha256.ComputeHash(GetKeyByteArray(plainText));
            sha256.Clear();

            return GetStringValue(tmpByte, true);
        }

        /// <summary>
        /// SHA1Managed 哈希算法-512 位
        /// huhm2008
        /// </summary>
        /// <param name="plainText">原字符串</param>
        /// <returns>加密后字符串</returns>
        public static string SHA512Encrypt(string plainText)
        {
            byte[] tmpByte;
            SHA512 sha512 = new SHA512Managed();

            tmpByte = sha512.ComputeHash(GetKeyByteArray(plainText));
            sha512.Clear(); 

            return GetStringValue(tmpByte, true);

        }

        #endregion  

        #region AES算法


        /// <summary>
        /// AES加密(256位),huhm2008
        /// </summary>
        /// <param name="plainText">被加密的明文</param>
        /// <param name="key">密钥</param>
        /// <returns>密文</returns>
        public static string AESEncrypt(string plainText, String key)
        {
            Rijndael aes = Rijndael.Create();
            byte[] bKey32 = UTF8Encoding.UTF8.GetBytes((key+KEY2).Substring(0,32));   
   
            try  
            {
                byte[] bData = Encoding.UTF8.GetBytes(plainText);
                aes.Key = bKey32;   
                aes.Mode = CipherMode.ECB;   
                aes.Padding = PaddingMode.PKCS7;   

                ICryptoTransform iCryptoTransform = aes.CreateEncryptor();   
                byte[] bResult =   
                    iCryptoTransform.TransformFinalBlock(bData, 0, bData.Length);   

                return Convert.ToBase64String(bResult);   
            }   
            catch  
            {   
                //throw;   
                return "";
            }   
        }


        /// <summary>
        /// AES解密(256位),huhm2008
        /// </summary>
        /// <param name="plainText">被解密的密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public static string AESDecrypt(string encryptText, String key)
        {
            Rijndael aes = Rijndael.Create();
            byte[] bKey32 = UTF8Encoding.UTF8.GetBytes((key + KEY2).Substring(0,32)); 

            try
            {
                byte[] bData = Convert.FromBase64String(encryptText);
                aes.Key = bKey32;   
                aes.Mode = CipherMode.ECB;   
                aes.Padding = PaddingMode.PKCS7;   

                ICryptoTransform iCryptoTransform = aes.CreateDecryptor();   
                byte[] bResult =   
                    iCryptoTransform.TransformFinalBlock(bData, 0, bData.Length);   
                return Encoding.UTF8.GetString(bResult);   
            }
            catch
            {
                //throw;
                return "";
            }  
        }

        #endregion 

        //#region DES算法

        ///// <summary>
        ///// DES加密(64位),huhm2008（注意DES每次加密的密文不一样）
        ///// </summary>
        ///// <param name="plainText">待加密的字符串</param>
        ///// <param name="key">密钥</param>
        ///// <returns>加密后的字符串</returns>
        //public static string DESEncrypt(string plainText, string key)
        //{
        //    DES des = new DESCryptoServiceProvider();
        //    byte[] bKey8 = UTF8Encoding.UTF8.GetBytes((key + KEY2).Substring(0, 8));

        //    try  
        //    {
        //        byte[] bData = Encoding.UTF8.GetBytes(plainText);
        //        des.Key = bKey8;   

        //        ICryptoTransform iCryptoTransform = des.CreateEncryptor();   
        //        byte[] bResult =   
        //            iCryptoTransform.TransformFinalBlock(bData, 0, bData.Length);   

        //        return Convert.ToBase64String(bResult);   
        //    }   
        //    catch  
        //    {   
        //        //throw;   
        //        return "";
        //    } 
        //}

 

        ///// <summary>
        ///// 使用DES解密(64位),huhm2008
        ///// </summary>
        ///// <param name="encryptText">待解密的字符串</param>
        ///// <param name="key">密钥(最大长度8)</param>
        ///// <returns>解密后的字符串</returns>
        //public static string DESDecrypt(string encryptText, string key)
        //{
        //    DES des = new DESCryptoServiceProvider();
        //    byte[] bKey8 = UTF8Encoding.UTF8.GetBytes((key + KEY2).Substring(0,8));

        //    try
        //    {
        //        byte[] bData = Convert.FromBase64String(encryptText);
        //        des.Key = bKey8;   

        //        ICryptoTransform iCryptoTransform = des.CreateDecryptor();   
        //        byte[] bResult =   
        //            iCryptoTransform.TransformFinalBlock(bData, 0, bData.Length);   
        //        return Encoding.UTF8.GetString(bResult); 
        //    }
        //    catch
        //    {
        //        //throw;
        //        return "";
        //    } 

        //}


        //#endregion 


        #region 硬件信息


        /// <summary>
        /// 获取机器硬件码,huhm2008
        /// </summary>
        /// <returns>硬件码，分别为：CPU/硬盘/网卡MAC</returns>
        public static string[] GetHardwareSN()
        {
            //CPU ID
            string[] textArray1 = new string[3];
            ManagementClass class1 = new ManagementClass("WIN32_Processor");
            ManagementObjectCollection collection1 = class1.GetInstances();
            using (ManagementObjectCollection.ManagementObjectEnumerator enumerator1 = collection1.GetEnumerator())
            {
                while (enumerator1.MoveNext())
                {
                    ManagementObject obj1 = (ManagementObject)enumerator1.Current;
                    textArray1[0] = obj1.Properties["ProcessorId"].Value.ToString();
                }
            }

            //硬盘ID
            ManagementClass cimobject = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc = cimobject.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                textArray1[1] = (string)mo.Properties["Model"].Value.ToString();
            }     

            //网卡MAC
            ManagementClass class2 = new ManagementClass("WIN32_NetworkAdapterConfiguration");
            ManagementObjectCollection collection2 = class2.GetInstances();
            using (ManagementObjectCollection.ManagementObjectEnumerator enumerator1 = collection2.GetEnumerator())
            {
                while (enumerator1.MoveNext())
                {
                    ManagementObject obj2 = (ManagementObject)enumerator1.Current;
                    if ((bool)obj2["IPEnabled"])
                    {
                        textArray1[2] = obj2["MacAddress"].ToString().Replace(":", "").ToString();
                    }
                }
            }
            return textArray1;
        }

        #endregion 


        #region 私有方法

        private static byte[] GetKeyByteArray(string strData)
        {
            ASCIIEncoding Asc = new ASCIIEncoding();

            int tmpStrLen = strData.Length;
            byte[] tmpByte = new byte[tmpStrLen - 1];

            tmpByte = Asc.GetBytes(strData);

            return tmpByte;
        }

        private static string GetStringValue(byte[] Byte, bool isReturnNumCode)
        {
            if (Byte == null)
                return "";

            string tmpString = "";
            if (isReturnNumCode == false)
            {
                ASCIIEncoding Asc = new ASCIIEncoding();
                tmpString = Asc.GetString(Byte);
            }
            else
            {
                int iCounter;
                for (iCounter = 0; iCounter < Byte.Length; iCounter++)
                {
                    tmpString = tmpString + Byte[iCounter].ToString();
                }
            }
            return tmpString;
        }

        #endregion 

    }
}
