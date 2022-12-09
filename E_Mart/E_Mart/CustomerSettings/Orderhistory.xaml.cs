using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using E_Mart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using E_Mart.Utills;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart.CustomerSettings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Orderhistory : ContentPage
    {
        APICall api = new APICall();
        public Orderhistory()
        {
            InitializeComponent();
            LoadData();
        }
        private async void LoadData()
        {


            var orderslist = await api.CallApiGetAsync<List<ORDER_tbl>>("api/Customer/Orderhistory/?id=" + App.LoggedInCustomer.CUSTOMER_ID);
            if (orderslist == null)
            {
                lblname.IsVisible = true;
                lblname.Text = "You have no booked orders yet!";
            }
            else
            {
                ListData.ItemsSource = orderslist;
            }
          
            //UserDialogs.Instance.HideLoading();
        }

        private async void ListData_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selected = e.SelectedItem as ORDER_tbl;
            var actionSheet = await DisplayActionSheet("Options", "Cancel", null, "Cancel Order", "View Invoice");
            if (actionSheet == "Cancel Order")
            {
                var q = await DisplayAlert("Successfully", "Are you sure to Cancel your order No:" + selected.ORDER_ID, "Yes", "No");
                if (q)
                {
                    
                    var Order = new ORDER_tbl
                    {
                        ORDER_ID = selected.ORDER_ID,
                        ORDER_STATUS = "Cancelled",
                        PAYMENT_MODE = selected.PAYMENT_MODE,
                        CUSTOMER_FID = selected.CUSTOMER_FID,
                        ORDER_DATE = selected.ORDER_DATE
                    };

                    var modifiedlist = await api.CallApiPutAsync("api/Orders/" , selected.ORDER_ID, Order);

                    LoadData();
                    if (modifiedlist !=null)
                    {
                        //MailProvider.SenttoMail(App.LoggedInCustomer.CUSTOMER_EMAIL, "Oder Cancellation", "Dear " + App.LoggedInCustomer.CUSTOMER_NAME + "!!Your order has been successfull cancelled.<br/> Regards Readrix Team");
                        await DisplayAlert("Successfully", " Cancelled your order No:" + selected.ORDER_ID, "OK");
                    }
                    else
                    {
                     
                        await DisplayAlert("Error", "Somthing went wrong!!", "OK");
                    }
                } 
            }
            //if (actionSheet == "View Invoice")
            //{
            //   await Navigation.PushAsync(new Invoice(selected));
            //}
        }
    }
}