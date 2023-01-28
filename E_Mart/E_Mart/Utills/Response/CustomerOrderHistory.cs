using E_Mart.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Mart.Utills.Response
{
    public class CustomerOrderHistory
    {
        public int PRO_ORDER_QUANTITY { get; set; }
        public decimal PURCHASE_PRICE { get; set; }
        public string PRODUCT_NAME { get; set; }
        public string ImageURL1 { get; set; }
    }
}
