using E_Mart.Models;
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


                ProgressInd.IsRunning = true;


                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback =
                    (message, certificate, chain, sslPolicyErrors) => true;


                CUSTOMER_tbl cus = new CUSTOMER_tbl()
                {

                    CUSTOMER_NAME = txtName.Text,
                    CUSTOMER_CONTACT = txtContact.Text,
                    CUSTOMER_EMAIL = txtEmail.Text,
                    CUSTOMER_PASSWORD = txtPassword.Text,
                    CUSTOMER_ADDRESS = txtAddress.Text,

                };

                var uri = App.APIBaseURL + "api/CUSTOMER_tbl_API/postcustomer";
                var client = new HttpClient();
                string JsonData = JsonConvert.SerializeObject(cus);
                StringContent StringData = new StringContent(JsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PostAsync(uri, StringData);
                string responseData = await responseMessage.Content.ReadAsStringAsync();




                //await DisplayAlert("message", response.Message, "ok");
                if (responseData != "")
                {
                    await DisplayAlert("Info", "Email Already Existed.", "ok");
                    await Navigation.PushAsync(new Login());
                }
                if (responseData == "")
                {

                    await DisplayAlert("Success", "Successfully Created.", "ok");
                    await Navigation.PushAsync(new Login());
                }

            }
            catch (Exception ex)
            {
                ProgressInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Error: " + ex.Message, "OK");

            }
        }
    }
}