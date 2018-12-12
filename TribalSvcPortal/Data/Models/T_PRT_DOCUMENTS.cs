using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_DOCUMENTS
    {
        public Guid DOC_IDX { get; set; }
        public string ORG_ID { get; set; }
        public byte[] DOC_CONTENT { get; set; }
        public string DOC_NAME { get; set; }
        public string DOC_TYPE { get; set; }
        public string DOC_FILE_TYPE { get; set; }
        public int? DOC_SIZE { get; set; }
        public string DOC_COMMENT { get; set; }
        public string DOC_AUTHOR { get; set; }
        public string SHARE_TYPE { get; set; }
        public string DOC_STATUS_TYPE { get; set; }
        public bool ACT_IND { get; set; }
        public string CREATE_USER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string MODIFY_USER_ID { get; set; }
        public DateTime? MODIFY_DT { get; set; }

        public T_PRT_REF_DOC_STATUS_TYPE DOC_STATUS_TYPENavigation { get; set; }
        public T_PRT_REF_DOC_TYPE DOC_TYPENavigation { get; set; }
        public T_PRT_ORGANIZATIONS ORG_ { get; set; }
        public T_PRT_REF_SHARE_TYPE SHARE_TYPENavigation { get; set; }
    }
}
