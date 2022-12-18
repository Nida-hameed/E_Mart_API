using E_Mart.Models;
using E_Mart.Shop;
using E_Mart.Utills;
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

                CUSTOMER_tbl customer = new CUSTOMER_tbl();
                customer.CUSTOMER_EMAIL = txtEmail.Text;
                customer.CUSTOMER_PASSWORD = txtPassword.Text;

                var responseData = await api.CallApiPostAsync("api/CUSTOMER_tbl_API/loginchk", customer);

                if (responseData.CUSTOMER_NAME != null)
                {
                    App.LoggedInCustomer = responseData;
                    if (App.Cart.Count > 0)
                    {
                        await Navigation.PushAsync(new CartControls());
                    }
                    else
                    {
                        App.Current.MainPage = new Customer.CustomerSideBar();
                    }         
                }
                else
                {
                    await DisplayAlert("Oops", "Incorrect Email OR Passwoed. Please Re-Enter !!", "OK");
                }
            }
            catch (Exception ex)
            {
                //ProgressInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Error: " + ex.Message, "OK");
            }
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //App.Current.MainPage = new NavigationPage(new Register());
            await Navigation.PushAsync(new Register());
        }

    }
}