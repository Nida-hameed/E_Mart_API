using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Mart.Models
{
    [Serializable]

    public partial class CUSTOMER_tbl
    {

        public int CUSTOMER_ID { get; set; }

        public string CUSTOMER_NAME { get; set; }

        public string CUSTOMER_CONTACT { get; set; }

        public string CUSTOMER_EMAIL { get; set; }

        public string CUSTOMER_PASSWORD { get; set; }

        public string CUSTOMER_ADDRESS { get; set; }
        public bool STATUS { get; set; }


       
    }
}
