using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace E_Mart.Models
{
    [Serializable]

    public partial class SELLER_tbl
    {
       

        public int SELLER_ID { get; set; }
        
        public string SELLER_NAME { get; set; }

        public string SELLER_PHONE { get; set; }

        public string SELLER_EMIAL { get; set; }

        public string SELLER_PASSWORD { get; set; }

        public string SELLER_CITY { get; set; }

        public string SELLER_ADDRESS { get; set; }

        public bool STATUS { get; set; }
    }
}
