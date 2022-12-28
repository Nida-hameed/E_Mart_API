using Acr.UserDialogs;
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
    public partial class Register : ContentPage
    {
        APICall api = new APICall();
        public Register()
        {
            InitializeComponent();
        }
        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtContact.Text) || string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtCity.Text))
                {

                    await DisplayAlert("Error", "Please fillout all requried fields and Try Again!", "OK");
                    return;
                }

                if (txtPassword.Text != txtCPassword.Text)
                {
                    await DisplayAlert("Error", "Passwords do not match.", "OK");
                    return;
                }


                UserDialogs.Instance.ShowLoading("Loading Please Wait...");

                SELLER_tbl seller = new SELLER_tbl()
                {

                    SELLER_NAME = txtName.Text,
                    SELLER_PHONE = txtContact.Text,
                    SELLER_EMIAL = txtEmail.Text,
                    SELLER_PASSWORD = txtPassword.Text,
                    SELLER_ADDRESS = txtAddress.Text,
                    SELLER_CITY = txtCity.Text,

                };

                var responseData = await api.CallApiGetAsync<SELLER_tbl>("api/SELLER_tbl_API/postseller");


                if (responseData == null)
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Message", "Email Already Existed.", "OK");
                    await Navigation.PushAsync(new Login());
                }
                if (responseData != null)
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Success", "Successfully Created!", "OK");
                    await Navigation.PushAsync(new Login());
                }

            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Error: " + ex.Message, "OK");

            }
        }
    }
}