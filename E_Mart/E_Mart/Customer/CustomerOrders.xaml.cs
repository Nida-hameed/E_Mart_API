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
using Acr.UserDialogs;

namespace E_Mart.Customer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerOrders : ContentPage
    {
        APICall api = new APICall();
        public CustomerOrders()
        {
            InitializeComponent();
            LoadData();
        }
        private async void LoadData()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Loading Please Wait...");

                var orderslist = await api.CallApiGetAsync<List<ORDER_tbl>>("api/Customer/Orders/" + App.LoggedInCustomer.CUSTOMER_ID);
                if (orderslist == null)
                {
                    UserDialogs.Instance.HideLoading();
                    lblname.IsVisible = true;
                    lblname.Text = "You have no booked orders yet!";
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    ListData.ItemsSource = orderslist;
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("message", "Error Occured please try again:" + ex.Message, "ok");
            }
        }

        private async void ListData_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var Selected = e.Item as ORDER_tbl; 
            await Navigation.PushAsync(new OrderHistory(Selected));
        }
    }
}