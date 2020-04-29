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
                    SqlHelper.CallProcLogin(userName, pwd, out errorMsg);
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
                string str = string.Format("select u.userId,u.userName,u.userPwd,u.userRole from t_sysUser u");
                DataSet dataSet=SqlHelper.ExcuteSql(str, out errorMsg);
                if (errorMsg == "" && dataSet!=null &&dataSet.Tables.Count>0 && dataSet.Tables[0].Rows.Count>0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        Models.UserInfoModel userInfoModel = new Models.UserInfoModel()
                        {
                            userId = dataRow["userId"].ToString(),
                            userName = dataRow["userName"].ToString(),
                            userPwd = dataRow["userPwd"].ToString(),
                            userRole = dataRow["userRole"].ToString()
                        };
                        userInfoModels.Add(userInfoModel);
                    }
                }
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
                string str = string.Format("delete from t_sysUser where userName='{0}'", userName);
                SqlHelper.ExcuteSql(str,out errorMsg);
                if (errorMsg == "")
                {
                    errorMsg = "操作成功";
                }
            }
            catch(Exception ex)
            {
                errorMsg = ex.ToString();
            }
        }

        /// <summary>
        /// 检查用户是否已存在
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public bool CheckUserExist(string userName,out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                string str = string.Format("select userName from t_sysUser where userName='{0}'",userName);
                DataSet dataSet = SqlHelper.ExcuteSql(str,out errorMsg);
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                errorMsg = ex.ToString();
                return false;
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
                if (CheckUserExist(userName, out errorMsg))
                {
                    errorMsg = "账号已存在";
                    return;
                }
                else
                {
                    if (errorMsg.Length > 0)
                    {
                        return;
                    }
                }
                string pwd = EncryUserPwd.EncryPwd(userPwd);
                string str = string.Format("insert into t_sysUser(userName,userPwd,userRole) values('{0}','{1}','{2}')", userName, pwd, userRole);
                SqlHelper.ExcuteSql(str, out errorMsg);
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
                SqlHelper.CallProcEditUserPwd(userName, userPwdOld, userPwdNew, out errorMsg);
            }
            catch(Exception ex)
            {
                errorMsg = ex.ToString();
            }
        }
    }
}