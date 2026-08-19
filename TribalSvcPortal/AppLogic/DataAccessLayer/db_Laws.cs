using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TribalSvcPortal.Data.Models;
using TribalSvcPortal.AppLogic.BusinessLogicLayer;
using Microsoft.EntityFrameworkCore;
using static TribalSvcPortal.AppLogic.BusinessLogicLayer.Utils;

namespace TribalSvcPortal.AppLogic.DataAccessLayer
{
  

    public interface IDbLaws
    {
        //************** LAWS **********************************
        List<T_LAW_LAWS> getT_LAWS_LAWS(string q);



    }

    public class DbLaws : IDbLaws
    {
        private readonly ApplicationDbContext ctx;
        private readonly Ilog _log;
        
        public DbLaws(ApplicationDbContext _context, Ilog log)
        {
            ctx = _context;
            _log = log ?? throw new ArgumentNullException(nameof(log));        
        }


        //************** T_LAWS_LAWS **********************************
        public List<T_LAW_LAWS> getT_LAWS_LAWS(string q)
        {
            try
            {
                var xxx = (from a in ctx.T_LAW_LAWS
                           where a.citation.Contains(q) || a.title.Contains(q)                           
                        select a).ToList();

                return xxx;
            }
            catch (Exception ex)
            {
                _log.LogEFException(ex);
                return null;
            }
        }


    }

}
