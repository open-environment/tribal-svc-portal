using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TribalSvcPortal.Data.Models
{
    public partial class T_OD_REF_WASTE_TYPE
    {
        public T_OD_REF_WASTE_TYPE()
        {
            T_OD_DUMP_ASSESSMENT_CONTENT = new HashSet<T_OD_DUMP_ASSESSMENT_CONTENT>();
        }

        public Guid REF_WASTE_TYPE_IDX { get; set; }
        public string REF_WASTE_TYPE_NAME { get; set; }
        public string REF_WASTE_TYPE_CAT { get; set; }
        public int? REF_WASTE_HAZFACT_SUBSCORE { get; set; }
        public string MODIFY_USER_ID { get; set; }
        public DateTime? MODIFY_DT { get; set; }
        [NotMapped]
        public bool IS_CHECKED { get; set; }
        public ICollection<T_OD_DUMP_ASSESSMENT_CONTENT> T_OD_DUMP_ASSESSMENT_CONTENT { get; set; }
    }
}
