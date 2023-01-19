using E_Mart.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Mart.Utills.Response
{
    public class CustomerOrderHistory
    {
        public List<ORDER_DETAIL_tbl> OrderDetail{ get; set; }
        public List<PRODUCT_tbl> ProductDetail{ get; set; }
    }
}
