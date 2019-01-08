using System;
using TribalSvcPortal.AppLogic.BusinessLogicLayer;
using TribalSvcPortal.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TribalSvcPortal.AppLogic.DataAccessLayer
{
    public class log : Ilog {
        private readonly ApplicationDbContext _context;
        public log(IConfiguration config) {
            var  c_config = config ?? throw new ArgumentNullException(nameof(config));
            _context = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>(), c_config);
        }


        public int InsertT_PRT_SYS_EMAIL_LOG(string from, string to, string cc, string subj, string msg, string logType)
        {
            try
            {
                T_PRT_SYS_EMAIL_LOG e = new T_PRT_SYS_EMAIL_LOG
                {
                    LOG_DT = System.DateTime.Now,
                    LOG_FROM = from,
                    LOG_TO = to,
                    LOG_CC =cc,
                    LOG_SUBJ = subj,
                    LOG_MSG = msg,
                    EMAIL_TYPE = logType
                };
                _context.T_PRT_SYS_EMAIL_LOG.Add(e);
                _context.SaveChanges();
                return e.EMAIL_LOG_ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertT_PRT_SYS_LOG(string logType, string logMsg)
        {
            try
            {
                T_PRT_SYS_LOG e = new T_PRT_SYS_LOG
                {
                    LOG_TYPE = logType,
                    LOG_MSG = logMsg,
                    LOG_DT = System.DateTime.Now
                };
                _context.T_PRT_SYS_LOG.Add(e);
                _context.SaveChanges();
                return e.SYS_LOG_ID;
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
        public void LogEFException(Exception ex)
        {
            string err = "";
            //if (ex is DbEntityValidationException)
            //{
            //    DbEntityValidationException dbex = (DbEntityValidationException)ex;
            //    foreach (var eve in dbex.EntityValidationErrors)
            //    {
            //        err += "Entity error type" + eve.Entry.Entity.GetType().Name;  //maybe add eve.Entry.State too
            //        foreach (var ve in eve.ValidationErrors)
            //            err += " property: " + ve.PropertyName + " error: " + ve.ErrorMessage;
            //    }
            //}
            //else
            //{
                Exception realerror = ex;
                while (realerror.InnerException != null)
                    realerror = realerror.InnerException;

                err = realerror.Message ?? "";
            //}

            InsertT_PRT_SYS_LOG("ERROR", err.SubStringPlus(0, 2000));
        }


    }
}
