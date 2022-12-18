using System;

namespace E_Mart.Models
{
   

    public partial class FEEDBACK_tbl
    {
      
        public int FEEDBACK_ID { get; set; }

        public int CUSTOMER_FID { get; set; }

        public string FEEDBACK_DESCRIPTION { get; set; }

        public DateTime FEEDBACK_DATE { get; set; }

        public virtual CUSTOMER_tbl CUSTOMER_tbl { get; set; }
    }
}
