using E_Mart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Mart.Utills.Response
{
    public class GetCategoryAndProductsByShopIdResponse
    {
        public List<PRO_CATEGORY_tbl> Categories { get; set; }
        public List<PRODUCT_tbl> Products { get; set; }
    }
}