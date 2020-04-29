using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElemnetUi_Vue.JS_Mvc.Models
{
    public class ResultBaseInfo
    {
        public bool isSuccess { get; set; }
        public string errorMsg { get; set; }
        public string successMsg { get; set; }
    }

    public class UserInfoModel
    {
        public string userId { get; set; }
        public string userName { get; set; }
        public string userPwd { get; set; }     
        public string userRole { get; set; }
    }
}