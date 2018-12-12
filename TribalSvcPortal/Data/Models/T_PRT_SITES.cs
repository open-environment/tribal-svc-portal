using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_PRT_SITES
    {
        public T_PRT_SITES()
        {
            T_PRT_SITE_INTERESTS = new HashSet<T_PRT_SITE_INTERESTS>();
        }

        public Guid SITE_IDX { get; set; }
        public string ORG_ID { get; set; }
        public string SITE_NAME { get; set; }
        public string EPA_ID { get; set; }
        public decimal? LATITUDE { get; set; }
        public decimal? LONGITUDE { get; set; }
        public string SITE_ADDRESS { get; set; }
        public string CREATE_USER_ID { get; set; }
        public DateTime? CREATE_DT { get; set; }
        public string MODIFY_USER_ID { get; set; }
        public DateTime? MODIFY_DT { get; set; }

        public T_PRT_ORGANIZATIONS ORG_ { get; set; }
        public ICollection<T_PRT_SITE_INTERESTS> T_PRT_SITE_INTERESTS { get; set; }
    }
}
