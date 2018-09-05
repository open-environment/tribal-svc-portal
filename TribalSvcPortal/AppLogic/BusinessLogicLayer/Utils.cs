using System;
using System.Collections.Generic;
using System.Linq;
using TribalSvcPortal.Data.Models;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using System.IO;
using System.Threading.Tasks;
using TribalSvcPortal.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace TribalSvcPortal.AppLogic.BusinessLogicLayer
{
    public static class Utils
    {  
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
