using Acr.UserDialogs;
using E_Mart.Models;
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
using static System.Net.Mime.MediaTypeNames;

namespace E_Mart.CustomerLoginSystem
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

                UserDialogs.Instance.ShowLoading("Loading Please Wait...");

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
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Message", "Email Already Existed.", "OK");
                    await Navigation.PushAsync(new Login());
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
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
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Error: " + ex.Message, "OK");

            }
        }



        //---------------Validations------------------------------//
        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            var EmailPattern = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
            if (Regex.IsMatch(e.NewTextValue, EmailPattern))
            {

                lblEmail.Text = "Valid Email";
                lblEmail.TextColor = Color.Green;
            }
            else
            {
                lblEmail.Text = "InValid Email! Email must contain @ and .com";
                lblEmail.TextColor = Color.Red;
            }
        }

        private void txtPassword_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (e.NewTextValue.Length < 8)
            {
                lblPassword.IsVisible = true;
                lblPassword.Text = "Password should be of at least 8 charaters";
                lblPassword.TextColor = Color.Red;
            }

            var PasswordPattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])";
            if (Regex.IsMatch(e.NewTextValue, PasswordPattern))
            {
                lblPassword.Text = "Strong Password!";
                lblPassword.TextColor = Color.Green;
            }

            else
            {
                lblPassword.Text = "Weak Password! Password should contain Uppercase Letter , Lowercase Letter, Number(s) and special characters [$@$!%*#?&]";
                lblPassword.TextColor = Color.Red;
            }
        }

        private void txtContact_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (e.NewTextValue.Length < 11 || e.NewTextValue.Length > 13)
            {
                lblPhone.IsVisible = true;
                lblPhone.Text = "InValid Phone! Missing digit(s)";
                lblPhone.TextColor = Color.Red;
            }

            else
            {
                lblPhone.Text = "Valid Phone";
                lblPhone.TextColor = Color.Green;
            }
        }

        private void txtAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length < 15)
            {
                lblAddress.IsVisible = true;
                lblAddress.Text = "Provide Complete Address for Product delivery purpuse!";
                lblAddress.TextColor = Color.Red;
            }
            else
            {
                lblAddress.Text = "Valid Address";
                lblAddress.TextColor = Color.Green;
            }
        }


    }
}