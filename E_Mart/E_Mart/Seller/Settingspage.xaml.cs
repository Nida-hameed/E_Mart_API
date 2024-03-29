﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using E_Mart.LoggedInCustomer;
using E_Mart.Seller;
using E_Mart.User;
using E_Mart.Utills;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart.Seller
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settingspage : ContentPage
    {
        APICall api = new APICall();
        public Settingspage()
        {
            InitializeComponent();
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var select = e.Item as string;

            if (select == "Delete Account")
            {
                var q = await DisplayAlert("Successfully", "Are you sure to Delete your account permanently!", "Yes", "No");
                if (q)
                {
                    try
                    {
                        UserDialogs.Instance.ShowLoading("Loading Please Wait...");

                        var result = await api.CallApiGetAsync<bool>("api/SELLER_tbl_API/deleteseller/" + App.LoggedInSeller.SELLER_ID);

                        if (result == true)
                        {
                            UserDialogs.Instance.HideLoading();
                            await DisplayAlert("Success", " Permanently Deleted your account. You wont be able to login with this account .Thank You.", "OK");
                            App.LoggedInCustomer = null;
                            App.Current.MainPage = new NavigationPage(new Login());
                        }
                        else
                        {
                            UserDialogs.Instance.HideLoading();
                            await DisplayAlert("Error", "Somthing went wrong!!", "OK");
                        }
                    }
                    catch (Exception ex)
                    {
                        UserDialogs.Instance.HideLoading();
                        await DisplayAlert("message", "Error Occured please try again:" + ex.Message, "ok");

                    }
                }

            }

      
            if (select == "Login to another Account")
            {
                await Navigation.PushAsync(new Seller.Login());
            }
            if (select == "Logout")
            {
                bool q = await DisplayAlert("Message", "Are you sure to log out!", "Yes", "No");
                if (q)
                {
                    App.LoggedInSeller = null;
                    // App.Current.MainPage = new NavigationPage(new Seller.Login());
                    App.Current.MainPage = new UserSideBar();
                }
            }
            if (select == "Manage Profile")
            {
                await Navigation.PushAsync(new Seller.Editprofile());
            }
        }
    }
}