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
using E_Mart.LoggedInSeller;

namespace E_Mart.Seller
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Editprofile : ContentPage
    {
        APICall api = new APICall();
        //private MediaFile _mediaFile;

        public Editprofile()
        {
            InitializeComponent();
           
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadData();
        }
        private void LoadData()
        {
            txtName.Text = App.LoggedInSeller.SELLER_NAME;
            txtAddress.Text = App.LoggedInSeller.SELLER_ADDRESS;
            txtContact.Text = App.LoggedInSeller.SELLER_PHONE;
            txtPassword.Text = App.LoggedInSeller.SELLER_PASSWORD;
            txtEmail.Text = App.LoggedInSeller.SELLER_EMIAL;
            txtCity.Text = App.LoggedInSeller.SELLER_CITY;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            try
            {


                var seller = new SELLER_tbl
                {
                    SELLER_ID = App.LoggedInSeller.SELLER_ID,
                    SELLER_EMIAL = txtEmail.Text,
                    SELLER_NAME = txtName.Text,
                    SELLER_PHONE = txtContact.Text,
                    SELLER_PASSWORD = txtPassword.Text,
                    SELLER_ADDRESS = txtAddress.Text,
                    SELLER_CITY = txtCity.Text,

                };

                var editedseller= await api.CallApiPostAsync("api/SELLER_tbl_API/editSeller", seller);

                LoadData();
                if (editedseller != null)
                {                  
                    await DisplayAlert("Success", "Successfully Update Profile", "OK");

                    App.LoggedInSeller = seller;
                    LoadData();

                    App.Current.MainPage = new SellerSideBar();
                }
                else
                {
                    await DisplayAlert("Error", "Somthing went wrong!!", "OK");
                }

           

            }
            catch (Exception ex)
            {
                await DisplayAlert("message", "Error Occured please try again:" + ex.Message, "ok");
            }

        }

    }
}