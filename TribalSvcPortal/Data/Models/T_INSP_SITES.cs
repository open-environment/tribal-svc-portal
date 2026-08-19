using System;
using System.Collections.Generic;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_INSP_SITES
    {
        public T_INSP_SITES()
        {
            T_INSP_ASSESS_LIFE = new HashSet<T_INSP_ASSESS_LIFE>();
        }

        public Guid SITE_IDX { get; set; }
        public string LAST_UPDATE_BY { get; set; }
        public DateTime? LAST_UPDATE_DT { get; set; }

        public virtual ICollection<T_INSP_ASSESS_BUILDING> T_INSP_ASSESS_BUILDING { get; set; }
        public virtual ICollection<T_INSP_ASSESS_ELECTRICAL> T_INSP_ASSESS_ELECTRICAL { get; set; }
        public virtual ICollection<T_INSP_ASSESS_FIRE> T_INSP_ASSESS_FIRE { get; set; }
        public virtual ICollection<T_INSP_ASSESS_LIFE> T_INSP_ASSESS_LIFE { get; set; }
        public virtual ICollection<T_INSP_ASSESS_MECHANICAL> T_INSP_ASSESS_MECHANICAL { get; set; }
        public virtual ICollection<T_INSP_ASSESS_PLUMBING> T_INSP_ASSESS_PLUMBING { get; set; }
    }
}
