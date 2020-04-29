using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElemnetUi_Vue.JS_Mvc.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 管理系统主界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Main()
        {
            return View();
        }


        /// <summary>
        /// 用户管理界面
        /// </summary>
        /// <returns></returns>
        public ActionResult UserManage()
        {
            return View();
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllUsers()
        {
            Models.ResultBaseInfo resultBaseInfo = new Models.ResultBaseInfo();
            List<Models.UserInfoModel> userInfoModels = new List<Models.UserInfoModel>();
            string errorMsg = string.Empty;
            try
            {
                MvcApplication.bllMethod.GetAllUsers(out userInfoModels,out errorMsg);
            }
            catch(Exception ex)
            {

            }
            return Json(userInfoModels);
        }

        public ActionResult EditUser(string userName,string userPwdOld,string userPwdNew)
        {
            string errorMsg = string.Empty;
            try
            {
                MvcApplication.bllMethod.EditUser(userName, userPwdOld, userPwdNew, out errorMsg);
            }
            catch(Exception ex)
            {
                errorMsg = ex.ToString();
            }
            return Json(errorMsg);
        }

        public ActionResult DeleteUser(string userName)
        {
            string errorMsg = string.Empty;
            try
            {
                MvcApplication.bllMethod.DeleteUser(userName, out errorMsg);
            }
            catch
            {

            }
            return Json(errorMsg);
        }

        public ActionResult AddUser(string userName,string userPwd,string userRole)
        {
            string errorMsg = string.Empty;
            try
            {
                MvcApplication.bllMethod.AddUser(userName,userPwd,userRole, out errorMsg);
            }
            catch
            {

            }
            return Json(errorMsg);
        }
    }
}