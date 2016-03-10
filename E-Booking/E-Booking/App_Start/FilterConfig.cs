using EBooking.Utilities;
using System.Web;
using System.Web.Mvc;

namespace EBooking
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LogExceptionFilterAttribute());
        }
    }
}