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
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtContact.Text) || string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrEmpty(txtPassword.Text))
                {
                    await DisplayAlert("Error", "Please fillout all requried fields and Try Again!", "OK");
                    return;
                }
                if (txtPassword.Text != txtCPassword.Text)
                {
                    await DisplayAlert("Error", "Passwords do not match.", "OK");
                    return;
                }
                //ProgressInd.IsRunning = true;
                CUSTOMER_tbl cus = new CUSTOMER_tbl()
                {
                    CUSTOMER_NAME = txtName.Text,
                    CUSTOMER_CONTACT = txtContact.Text,
                    CUSTOMER_EMAIL = txtEmail.Text,
                    CUSTOMER_PASSWORD = txtPassword.Text,
                    CUSTOMER_ADDRESS = txtAddress.Text,
                };

                var addcustomer = await api.CallApiPostAsync("api/CUSTOMER_tbl_API/postcustomer", cus);
                if (addcustomer == null)
                {
                    await DisplayAlert("Message", "Email Already Existed.", "OK");
                    await Navigation.PushAsync(new Login());
                }
                else
                {
                    await DisplayAlert("Success", "Successfully Created!", "OK");
                    await Navigation.PushAsync(new Login());
                }

                //var httpClientHandler = new HttpClientHandler();
                //httpClientHandler.ServerCertificateCustomValidationCallback =
                //(message, certificate, chain, sslPolicyErrors) => true;
                //var client = new HttpClient(httpClientHandler);
                //var uri = App.APIBaseURL + "api/CUSTOMER_tbl_API/postcustomer";
                //string JsonData = JsonConvert.SerializeObject(cus);
                //StringContent StringData = new StringContent(JsonData, Encoding.UTF8, "application/json");

                //HttpResponseMessage responseMessage = await client.PostAsync(uri, StringData);
                //string responseData = await responseMessage.Content.ReadAsStringAsync();

                //if (responseData != "")
                //{
                //    await DisplayAlert("Message", "Email Already Existed.", "OK");
                //    await Navigation.PushAsync(new Login());
                //}
                //if (responseData == "")
                //{

                //    await DisplayAlert("Success", "Successfully Created!", "OK");
                //    await Navigation.PushAsync(new Login());
                //}

            }
            catch (Exception ex)
            {
                //ProgressInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Error: " + ex.Message, "OK");

            }
        }
    }
}