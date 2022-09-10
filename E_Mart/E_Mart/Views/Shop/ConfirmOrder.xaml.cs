
using E_Mart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart.Shop
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmOrder : ContentPage
    {
        public ConfirmOrder()
        {
            InitializeComponent();
        }

        private async void btnCurrentLocation_Clicked(object sender, EventArgs e)
        {
            try
            {

                Models.ORDER_tbl order = new Models.ORDER_tbl()
                {
                    ORDER_DATE = DateTime.Now.Date,
                    ORDER_STATUS = "Booked",
                    PAYMENT_MODE = "CashOnDelivery",
                    //SHOP_FID = ,
                    CUSTOMER_FID= App.LoggedInCustomer.CUSTOMER_ID
                };



                foreach (var item in App.Cart)
                {
                    
                    //item.ORDER_FID = response.ORDER_ID;
                    item.PRO_ORDER_QUANTITY = item.PRO_ORDER_QUANTITY * -1;
                   
                }

                //await Navigation.PushAsync(new Success());
               

            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", "Somthing went wrong this may be a problem with internet or application please ensure that you have a working internet connection and GPS enabled. \nError Details : " + ex.Message, "OK");
            }
        }
    }
}