namespace E_Mart.Models
{

    public partial class ORDER_DETAIL_tbl
    {
     
       

        public int PRODUCT_FID { get; set; }

        public int PRO_ORDER_QUANTITY { get; set; }

        public decimal PURCHASE_PRICE { get; set; }

        public decimal SALE_PRICE { get; set; }

        public string PRODUCT_NAME { get; set; }
        public string ImageURL1 { get; set; }

        public PRODUCT_tbl PRODUCT_tbl { get; set; }


    }
}
