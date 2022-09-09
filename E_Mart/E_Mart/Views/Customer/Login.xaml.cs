using E_Mart.Models;
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
                ProgressInd.IsRunning = true;

                var responseData = await api.CallApiGetAsync<CUSTOMER_tbl> ("api/CUSTOMER_tbl_API/loginchk ? Customer.CUSTOMER_EMAIL = " + txtEmail.Text + " & Customer.CUSTOMER_PASSWORD = " + txtPassword.Text);
                if (responseData != null)
                {
                    App.LoggedInCustomer = responseData;
                }
                // App.Current.MainPage = new CustomerSideBar(); 
                await Navigation.PushAsync(new Home());
            }
            catch (Exception ex)
            {
                ProgressInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Error: " + ex.Message, "OK");
            }
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new Register());
        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            //App.Current.MainPage = new StartUpPage();
            await Navigation.PopAsync();
        }
    }
}