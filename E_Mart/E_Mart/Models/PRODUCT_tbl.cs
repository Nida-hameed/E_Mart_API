namespace E_Mart.Models
{

    public partial class PRODUCT_tbl
    {
      

        public int PRODUCT_ID { get; set; }

        public int Quantity { get; set; }


        public string PRODUCT_NAME { get; set; }

        public string PRODUCT_IMAGE { get; set; }
        public string ImageURL1 { get; set; }
        public string ImageURL2 { get; set; }
        public string ImageURL3 { get; set; }
        public string ImageURL4 { get; set; }

        public string PRODUCT_DESCRIPTION { get; set; }
        
        
        public string PRODUCT_SPECIFICATIONS { get; set; }


        public decimal PRODUCT_PURCHASEPRICE { get; set; }

        public decimal PRODUCT_SALEPRICE { get; set; }

        public string PRODUCT_AVAILABILITY { get; set; }

        public int PRO_CATEGORY_FID { get; set; }

    }
}
