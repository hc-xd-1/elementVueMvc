using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace ElemnetUi_Vue.JS_Mvc.BLL
{
    public class Method
    {
        /// <summary>
        /// 验证账号密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <param name="errorMsg"></param>
        public void CheckUserAndPwd(string userName,string pwd,out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                if (userName == "" && pwd == "")
                {
                    errorMsg = "账号或密码不能为空";
                }
                else
                {
                    SqlModelHelp.CheckUserAndPwd(userName, pwd, out errorMsg);                   
                }
            }
            catch(Exception ex)
            {
                errorMsg = ex.ToString();
            }            
        }

        /// <summary>
        /// 获取所有的用户信息
        /// </summary>
        /// <param name="userInfoModels"></param>
        /// <param name="errorMsg"></param>
        public void GetAllUsers(out List<Models.UserInfoModel> userInfoModels,out string errorMsg)
        {
            errorMsg = string.Empty;
            userInfoModels = new List<Models.UserInfoModel>();               
            try
            {
                userInfoModels=SqlModelHelp.GetAllUsers(out errorMsg);                
            }
            catch(Exception ex)
            {
                errorMsg = ex.ToString();
            }
        }

        /// <summary>
        /// 根据用户名删除用户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="errorMsg"></param>
        public void DeleteUser(string userName,out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                SqlModelHelp.DeleteUser(userName, out errorMsg);
                if (errorMsg == "")
                {
                    errorMsg = "操作成功";
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.ToString();
            }
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <param name="userRole"></param>
        /// <param name="errorMsg"></param>
        public void AddUser(string userName,string userPwd,string userRole, out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {                
                string pwd = EncryUserPwd.EncryPwd(userPwd);
                SqlModelHelp.AddUser(userName, pwd, userRole, out errorMsg);                
                if (errorMsg == "")
                {
                    errorMsg = "操作成功";
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.ToString();
            }
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwdOld"></param>
        /// <param name="userPwdNew"></param>
        public void EditUser(string userName,string userPwdOld,string userPwdNew,out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                userPwdOld = EncryUserPwd.EncryPwd(userPwdOld);
                userPwdNew = EncryUserPwd.EncryPwd(userPwdNew);
                SqlModelHelp.EditUserPwd(userName, userPwdOld, userPwdNew, out errorMsg);                
            }
            catch(Exception ex)
            {
                errorMsg = ex.ToString();
            }
        }
    }
}