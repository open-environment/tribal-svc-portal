using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_DOCUMENTS
    {
        public Guid DocIdx { get; set; }
        public string OrgId { get; set; }
        public byte[] DocContent { get; set; }
        public string DocName { get; set; }
        public string DocType { get; set; }
        public string DocFileType { get; set; }
        public int? DocSize { get; set; }
        public string DocComment { get; set; }
        public string DocAuthor { get; set; }
        public string ShareType { get; set; }
        public string DocStatusType { get; set; }
        public bool ActInd { get; set; }
        public int? CreateUseridx { get; set; }
        public DateTime? CreateDt { get; set; }
        public int? ModifyUseridx { get; set; }
        public DateTime? ModifyDt { get; set; }

        public T_PRT_REF_DOC_STATUS_TYPE DocStatusTypeNavigation { get; set; }
        public T_PRT_REF_DOC_TYPE DocTypeNavigation { get; set; }
        public T_PRT_ORGANIZATIONS Org { get; set; }
        public T_PRT_REF_SHARE_TYPE ShareTypeNavigation { get; set; }
    }
}
