using Acr.UserDialogs;
using E_Mart.Models;
using E_Mart.Shop;
using E_Mart.Utills;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart.CustomerLoginSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        APICall api = new APICall();
        public Login()
        {
            InitializeComponent();
        }
        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                await DisplayAlert("Error", "Please fillout all requried fields!", "OK");
                return;
            }
            try
            {
                UserDialogs.Instance.ShowLoading("Loading Please Wait...");

                CUSTOMER_tbl customer = new CUSTOMER_tbl();
                customer.CUSTOMER_EMAIL = txtEmail.Text;
                customer.CUSTOMER_PASSWORD = txtPassword.Text;

               
                var responseData = await api.CallApiPostAsync("api/CUSTOMER_tbl_API/loginchk", customer);

                if (responseData.CUSTOMER_NAME != null)
                {
                    App.LoggedInCustomer = responseData;
                    if (App.Cart.Count > 0)
                    {
                        UserDialogs.Instance.HideLoading();
                        await Navigation.PushAsync(new CartControls());
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                        App.Current.MainPage = new LoggedInCustomer.CustomerSideBar();
                    }         
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Oops", "Incorrect Email OR Password. Please Re-Enter !!", "OK");
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Error: " + ex.Message, "OK");
            }
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgotPassword());
        }

        private void btnShowPassword_Clicked(object sender, EventArgs e)
        {
            if (txtPassword.IsPassword == true)
            {
                txtPassword.IsPassword = false;
                btnShowPassword.Source = "HidePassword.png";
            }
            else
            {
                txtPassword.IsPassword = true;
                btnShowPassword.Source = "ShowPassword.png";
            }
        }
    }
}