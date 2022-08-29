namespace E_Mart.Models
{


    public partial class ITEM_tbl
    {

        public int ITEM_ID { get; set; }

        public string ITEM_NAME { get; set; }

        public decimal ITEM_PRICE { get; set; }

        public string ITEM_DETAIL{ get; set; }
  
        public string ITEM_IMAGE { get; set; }

        public int SELLER_FID { get; set; }



    }
}
