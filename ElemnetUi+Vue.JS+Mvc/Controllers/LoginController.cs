using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElemnetUi_Vue.JS_Mvc.BLL;

namespace ElemnetUi_Vue.JS_Mvc.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 系统登录界面
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginSystem()
        {
            return View();
        }

        /// <summary>
        /// 登录系统
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public ActionResult Login(string userName,string pwd)
        {
            Models.ResultBaseInfo resultBaseInfo = new Models.ResultBaseInfo();
            string errorMsg = string.Empty;
            try
            {
                string userPwd = EncryUserPwd.EncryPwd(pwd);
                MvcApplication.bllMethod.CheckUserAndPwd(userName, userPwd, out errorMsg);
                errorMsg = "";
                if (errorMsg == "")
                {
                    resultBaseInfo.isSuccess = true;
                    resultBaseInfo.successMsg = "/Main/Main";
                    Session["currentUser"] = userName;
                    //resultBaseInfo.successMsg = string.Format("欢迎登录：[{0}]",userName);
                }
                else
                {
                    resultBaseInfo.isSuccess = false;
                    resultBaseInfo.errorMsg = errorMsg;
                }
            }
            catch(Exception ex)
            {
                resultBaseInfo.isSuccess = false;
                resultBaseInfo.errorMsg = ex.ToString();
            }
            return Json(resultBaseInfo);
        }


        /// <summary>
        /// 登出系统
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginOut()
        {
            Models.ResultBaseInfo resultBaseInfo = new Models.ResultBaseInfo();
            try
            {
                if (Session["currentUser"] != null)
                {
                    Session["currentUser"] = "";
                }
                resultBaseInfo.isSuccess = true;                
            }
            catch(Exception ex)
            {
                resultBaseInfo.isSuccess = false;
                resultBaseInfo.errorMsg = ex.ToString();
            }
            return View("LoginSysTem");
        }
    }
}