using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElemnetUi_Vue.JS_Mvc.Models;

namespace ElemnetUi_Vue.JS_Mvc.BLL
{
    public class SqlModelHelp
    {
       static ElementVueMvcEntities db = new ElementVueMvcEntities();


        /// <summary>
        /// 检查用户名密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <param name="errorMsg"></param>
        public static void CheckUserAndPwd(string userName,string userPwd,out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                var users= from u in db.t_sysUser
                                 where u.userName==userName && u.userPwd==userPwd
                                 select u;
                if (users.Count()==0)
                {
                    errorMsg = "账号或密码不存在，请检查";
                }
            }
            catch(Exception ex)
            {
                errorMsg = ex.ToString();
            }
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static List<UserInfoModel> GetAllUsers(out string errorMsg)
        {
            errorMsg = string.Empty;
            List<UserInfoModel> listUsers = new List<UserInfoModel>();
            try
            {
                var users = from user in db.t_sysUser
                            select user;
                foreach (t_sysUser t_SysUser in users)
                {
                    UserInfoModel userInfoModel = new UserInfoModel()
                    {
                        userId = t_SysUser.userId.ToString(),
                        userName = t_SysUser.userName,
                        userPwd = t_SysUser.userPwd,
                        userRole = t_SysUser.userRole
                    };
                    listUsers.Add(userInfoModel);
                }
            }
            catch(Exception ex)
            {
                errorMsg = ex.ToString();
            }
            return listUsers;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="errorMsg"></param>
        public static void DeleteUser(string userName,out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                var user = from u in db.t_sysUser
                           where u.userName == userName
                           select u;
                if (user.Count() > 0)
                {
                    foreach (t_sysUser u in user)
                    {
                        db.t_sysUser.Remove(u);
                    }
                    int changeRows=db.SaveChanges();
                    if (changeRows == 0)
                    {
                        errorMsg = "删除用户失败";
                    }
                }
            }
            catch(Exception ex)
            {
                errorMsg = ex.ToString();
            }
        }


        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwdOld"></param>
        /// <param name="userPwdNew"></param>
        /// <param name="errorMsg"></param>
        public static void EditUserPwd(string userName,string userPwdOld,string userPwdNew,out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                var user = from u in db.t_sysUser
                           where u.userName == userName && u.userPwd == userPwdOld
                           select u;
                if (user.Count() == 0)
                {
                    errorMsg = "旧密码不正确，请重新输入";
                }
                else
                {
                    foreach (t_sysUser _SysUser in user)
                    {
                        _SysUser.userPwd = userPwdNew;
                    }
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                errorMsg = ex.ToString();
            }
        }

        /// <summary>
        /// 新增账户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <param name="userRole"></param>
        /// <param name="errorMsg"></param>
        public static void AddUser(string userName,string userPwd,string userRole,out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                var userExist = from u in db.t_sysUser
                                where u.userName == userName
                                select u;
                if (userExist.Count() > 0)
                {
                    errorMsg = "账号已存在，请勿重复创建";
                }
                else
                {
                    t_sysUser t_SysUser = new t_sysUser()
                    {
                        userName = userName,
                        userPwd = userPwd,
                        userRole = userRole
                    };
                    db.t_sysUser.Add(t_SysUser);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                errorMsg = ex.ToString();
            }
        }
    }
}