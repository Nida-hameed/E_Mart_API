﻿using Newtonsoft.Json;
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
    public partial class Editprofile : ContentPage
    {
        APICall api = new APICall();
        public static bool isnewpictureselected = false;
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
            txtName.Text = App.LoggedInCustomer.CUSTOMER_NAME;
            txtAddress.Text = App.LoggedInCustomer.CUSTOMER_ADDRESS;
            txtContact.Text = App.LoggedInCustomer.CUSTOMER_CONTACT;
            txtPassword.Text = App.LoggedInCustomer.CUSTOMER_PASSWORD;
            txtEmail.Text = App.LoggedInCustomer.CUSTOMER_EMAIL;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            try
            {


                var customer = new CUSTOMER_tbl
                {
                    CUSTOMER_ID = App.LoggedInCustomer.CUSTOMER_ID,
                    CUSTOMER_EMAIL = txtEmail.Text,
                    CUSTOMER_NAME = txtName.Text,
                    CUSTOMER_CONTACT = txtContact.Text,
                    CUSTOMER_PASSWORD = txtPassword.Text,
                    CUSTOMER_ADDRESS = txtAddress.Text,

                };

                var editedcustomer= await api.CallApiPutAsync("api/CUSTOMER_tbl_API/", customer.CUSTOMER_ID, customer);

                LoadData();
                if (editedcustomer != null)
                {                  
                    await DisplayAlert("Success", "Successfully Update Profile", "OK");

                    App.LoggedInCustomer = customer;
                    LoadData();

                    App.Current.MainPage = new Views.Customer.CustomerSideBar();
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