using Acr.UserDialogs;
using E_Mart.Models;
using E_Mart.Utills;
using E_Mart.Utills.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart.Customer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderHistory : ContentPage
    {
        APICall api = new APICall();
        public static int Orderid;
        public OrderHistory(ORDER_tbl order)
        {
            InitializeComponent();
            Orderid = order.ORDER_ID;
            LoadData();
        }
        private async void LoadData()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Loading Please Wait...");
                var responseData = await api.CallApiGetAsync<CustomerOrderHistory>("api/Customer/Orderhistory/" + Orderid);
                 OrdersList.ItemsSource = responseData.OrderDetail;
                 OrdersList.ItemsSource = responseData.ProductDetail;
               
                //foreach (var item in responseData)
                //{
                //    OrdersList.ItemsSource = item.OrderDetail;
                //    OrdersList.ItemsSource = item.ProductDetail;
                //}


                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Error: " + ex.Message, "OK");
            }
        }
    }
}