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

namespace E_Mart.Seller
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

                SELLER_tbl seller = new SELLER_tbl();
                seller.SELLER_EMIAL = txtEmail.Text;
                seller.SELLER_PASSWORD = txtPassword.Text;

                var responseData = await api.CallApiPostAsync("api/SELLER_tbl_API/loginchk", seller);

                if (responseData.SELLER_NAME != null)
                {
                    App.LoggedInSeller = responseData;
                    await Navigation.PushAsync(new Add_Product());
                }
                else
                {
                    await DisplayAlert("Oops", "Incorrect Email OR Passwoed. please Re-Enter !!", "OK");
                }
            }
            catch (Exception ex)
            {
                //ProgressInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Error: " + ex.Message, "OK");
            }
        }
        private  void TapGestureRecognizer_Tapped(object sender, EventArgs e)
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