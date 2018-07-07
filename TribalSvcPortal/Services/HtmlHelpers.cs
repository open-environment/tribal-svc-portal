using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TribalSvcPortal.HtmlHelpers
{
    public static class HtmlHelpersExtension
    {
        public static string ActivePage(this IHtmlHelper helper, string controller, string action)
        {
            string classValue = "";

            string currentController = helper.ViewContext.RouteData.Values["Controller"].ToString();
            string currentAction = helper.ViewContext.RouteData.Values["Action"].ToString();
            if (currentController == controller && currentAction == action)
                classValue = "active open";

            return classValue;
        }

    }

}