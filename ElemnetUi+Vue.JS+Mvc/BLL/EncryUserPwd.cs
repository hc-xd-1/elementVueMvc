using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace ElemnetUi_Vue.JS_Mvc.BLL
{
    public class EncryUserPwd
    {
        public static string EncryPwd(string userPwd)
        {
            string PwdEncryMd5 = string.Empty;
            try
            {
                MD5 mD5=new MD5CryptoServiceProvider();
                byte[] byteStr = Encoding.Default.GetBytes(userPwd);
                byte[] byteEncry=mD5.ComputeHash(byteStr);
                for (int i = 0; i < byteEncry.Length; i++)
                {
                    PwdEncryMd5 += byteEncry[i].ToString("x");
                }
            }
            catch(Exception ex)
            {

            }
            return PwdEncryMd5;
        }
    }
}