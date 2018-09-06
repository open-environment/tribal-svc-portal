using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using TribalSvcPortal.AppLogic.DataAccessLayer;
using TribalSvcPortal.AppLogic.BusinessLogicLayer;
using TribalSvcPortal.Data.Models;

namespace TribalSvcPortal.AppLogic.DataAccessLayer
{
    public static class log
    {
        private static readonly ApplicationDbContext ctx;

        public static int InsertT_PRT_SYS_LOG(string logType, string logMsg)
        {
            try
            {
                T_PRT_SYS_LOG e = new T_PRT_SYS_LOG
                {
                    LogType = logType,
                    LogMsg = logMsg,
                    LogDt = System.DateTime.Now
                };
                ctx.T_PRT_SYS_LOG.Add(e);
                ctx.SaveChanges();
                return e.SysLogId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// General purpose logging of any Entity Framework methods to database
        /// </summary>
        /// <param name="ex">Exception to log</param>
        public static void LogEFException(Exception ex)
        {

            string err = "";
            if (ex is DbEntityValidationException)
            {
                DbEntityValidationException dbex = (DbEntityValidationException)ex;
                foreach (var eve in dbex.EntityValidationErrors)
                {
                    err += "Entity error type" + eve.Entry.Entity.GetType().Name;  //maybe add eve.Entry.State too
                    foreach (var ve in eve.ValidationErrors)
                        err += " property: " + ve.PropertyName + " error: " + ve.ErrorMessage;
                }
            }
            else
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.Message == "An error occurred while updating the entries. See the inner exception for details.")
                    {
                        err = ex.InnerException.InnerException.ToString();
                    }
                }
                else
                    err = (ex.InnerException != null ? ex.InnerException.Message : "");
            }

            //string err = (ex.InnerException != null ? ex.InnerException.Message : "");
            InsertT_PRT_SYS_LOG("ERROR", err.SubStringPlus(0, 2000));
        }
    }
}
