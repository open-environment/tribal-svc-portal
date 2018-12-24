using System;

namespace TribalSvcPortal.AppLogic.DataAccessLayer {
    public interface Ilog {
        int InsertT_PRT_SYS_LOG(string logType, string logMsg);
        void LogEFException(Exception ex);
    }
}