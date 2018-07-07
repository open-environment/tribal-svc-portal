using System;
using System.Collections.Generic;
using System.Linq;
using TribalSvcPortal.Data.Models;
using TribalSvcPortal.AppLogic.DataAccessLayer;

namespace TribalSvcPortal.AppLogic.BusinessLogicLayer
{
    public static class Utils
    {
        private static readonly IDbPortal _DbPortal;

        /// <summary>
        /// General purpose logging of any Entity Framework methods to database
        /// </summary>
        /// <param name="ex">Exception to log</param>
        public static void LogEFException(Exception ex)
        {
            string err = (ex.InnerException != null ? ex.InnerException.Message : "");
            _DbPortal.InsertT_OE_SYS_LOG("ERROR", err.SubStringPlus(0, 2000));
        }


        /// <summary>
        ///  Better than built-in SubString by handling cases where string is too short
        /// </summary>
        public static string SubStringPlus(this string str, int index, int length)
        {
            if (index >= str.Length)
                return String.Empty;

            if (index + length > str.Length)
                return str.Substring(index);

            return str.Substring(index, length);
        }






    }


}
