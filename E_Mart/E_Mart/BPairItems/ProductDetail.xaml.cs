using Acr.UserDialogs;
using E_Mart.Models;
using E_Mart.Shop;
using E_Mart.Utills;
using E_Mart.Utills.Response;
using E_Mart.Views.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart.BPairItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetail : ContentPage
    {
        APICall api = new APICall();
        public static int sellerid;
        SELLER_tbl seller = new SELLER_tbl();

        //ITEM_tbl pro = new ITEM_tbl();
        public ProductDetail(ITEM_tbl item)
        {
            InitializeComponent();
            sellerid = item.SELLER_FID;

            //pro = item;
            ProName.Text = item.ITEM_NAME;
            ProDetail.Text = item.ITEM_DETAIL;
            ProPrice.Text = item.ITEM_PRICE.ToString();
            Proimage.Source = item.ImageURL;
           

        }
        private async void btnContactSeller_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Loading Please Wait...");
            var responseData = await api.CallApiGetAsync<List<SELLER_tbl>>("api/SELLER_tbl_API/contactSeller/" + sellerid);
            if (responseData != null)
            {
                UserDialogs.Instance.HideLoading();

                var seller = responseData.FirstOrDefault();

                await DisplayAlert("Detail", "" +
                  "\n Name: " + seller.SELLER_NAME +
                  "\n Email: " + seller.SELLER_EMIAL +
                  "\n Contact: " + seller.SELLER_PHONE, "", "OK");

            }
        }

    }
}