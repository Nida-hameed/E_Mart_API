using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Mart.Models
{
    [Serializable]
    public partial class WISHLIST_tbl
    {

        public int WISHLIST_ID { get; set; }
        public int CUSTOMER_FID { get; set; }
        public int PRODUCT_FID { get; set; }

    }
}
