using System;
using System.Collections.Generic;
using TribalSvcPortal.AppLogic.DataAccessLayer;

namespace TribalSvcPortal.ViewModels.OpenDumpViewModels
{
    public class ImportViewModel
    {
        public string IMPORT_BLOCK { get; set; }  //raw text imported
        public List<SiteImportType> sites { get; set; }   //in-memory storage of array of sites to import
    }
}
