using System.Web;
using System.Web.Mvc;

namespace MVCEliminatoria_EdgarDuarte_1193520
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
