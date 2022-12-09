
using E_Mart.Models;
using E_Mart.Utills;
using E_Mart.Utills.Request;
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

        private async void btnConfirm_Clicked(object sender, EventArgs e)
        {
            APICall api = new APICall();

            var OrderDetailRequest = new List<ORDER_DETAIL_tbl>();

            try
            {
                OrderRequest request = new OrderRequest()
                {
                    order = new ORDER_tbl
                    {
                        CUSTOMER_FID = App.LoggedInCustomer.CUSTOMER_ID,
                        ORDER_STATUS = "Booked",
                        PAYMENT_MODE = "COD",
                        ORDER_DATE = DateTime.Now,

                    },
                  
                    orderDetail = App.Cart,


                };
                var responseData = await api.CallApiPostAsync("api/order/PostOrderandOrderDetail", request);






                //Models.ORDER_tbl order = new Models.ORDER_tbl()
                //{
                //    ORDER_DATE = DateTime.Now.Date,
                //    ORDER_STATUS = "Booked",
                //    PAYMENT_MODE = "CashOnDelivery",
                //    CUSTOMER_FID = App.LoggedInCustomer.CUSTOMER_ID,
                //    SHOP_FID = App.SelectdShop.SHOP_ID,
                //};

                //var responseData = await api.CallApiPostAsync<ORDER_tbl>("api/");

                //foreach (var item in App.Cart)
                //{

                //    item.ORDER_FID = responseData.ORDER_ID;
                //    item.PRO_ORDER_QUANTITY = item.PRO_ORDER_QUANTITY * -1;

                //}

                await Navigation.PushAsync(new Success());


            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", "Something went wrong this may be a problem with internet or application please ensure that you have a working internet connection. \nError Details : " + ex.Message, "OK");
            }
        }
    }
}