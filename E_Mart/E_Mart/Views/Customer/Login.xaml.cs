﻿using E_Mart.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart.Views.Customer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }
        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, certificate, chain, sslPolicyErrors) => true;

            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                await DisplayAlert("Error", "Please fillout all requried fields!", "OK");
                return;
            }
            try
            {

                ProgressInd.IsRunning = true;

                
                var client = new HttpClient(httpClientHandler); 
                var uri = App.APIBaseURL + "api/CUSTOMER_tbl_API/loginchk ? Customer.CUSTOMER_EMAIL = " + txtEmail.Text + " & Customer.CUSTOMER_PASSWORD = " + txtPassword.Text ;
                string jsonData = JsonConvert.SerializeObject(new { CUSTOMER_EMAIL = txtEmail.Text, CUSTOMER_PASSWORD = txtPassword.Text });
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(uri, content);
               
                var customer = result;


                App.Current.MainPage = new CustomerSideBar();
                
            }



            catch (Exception ex)
            {
                ProgressInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Error: " + ex.Message, "OK");

            }


        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new Register());
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new StartUpPage();
        }
    }
}