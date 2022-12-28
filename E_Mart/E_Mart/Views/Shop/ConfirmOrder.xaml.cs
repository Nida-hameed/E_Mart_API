using Acr.UserDialogs;
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
                UserDialogs.Instance.ShowLoading("Loading Please Wait...");
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

                if (responseData != null)
                {
                    UserDialogs.Instance.HideLoading();
                    await Navigation.PushAsync(new Success());
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Message", "Something went wrong this may be a problem with internet or application please ensure that you have a working internet connection.", "OK");
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Message", "Something went wrong this may be a problem with internet or application please ensure that you have a working internet connection. \nError Details : " + ex.Message, "OK");
            }
        }
    }
}