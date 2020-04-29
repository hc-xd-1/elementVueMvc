using System.Web;
using System.Web.Mvc;

namespace ElemnetUi_Vue.JS_Mvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
