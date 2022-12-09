using E_Mart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Mart.Utills.Request
{
    public class OrderRequest
    {
        public  ORDER_tbl order{ get; set; }
        public List <ORDER_DETAIL_tbl> orderDetail{ get; set; }
    }
    //public class OrderDetaiRequest
    //{
    //    public int PRODUCT_ID { get; set; }
    //    public int PRODUCT_QUANTITY { get; set; }
    //    public int PRODUCT_ { get; set; }
    //}



}
