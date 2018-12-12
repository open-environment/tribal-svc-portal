using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TribalSvcPortal.HtmlHelpers
{
    public static class HtmlHelpersExtension
    {
        public static string ActivePage(this IHtmlHelper helper, string controller, string actions)
        {
            string currentController = helper.ViewContext.RouteData.Values["Controller"].ToString();
            string currentAction = helper.ViewContext.RouteData.Values["Action"].ToString();
            string[] acceptedActions = actions.Trim().Split(',').Distinct().ToArray();
            if (currentController == controller && acceptedActions.Contains(currentAction))
                return "active open";
            else
            return "";
        }
    }
}