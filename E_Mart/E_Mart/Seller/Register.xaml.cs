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



        //---------------Validations------------------------------//


        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            var EmailPattern = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
            if (Regex.IsMatch(e.NewTextValue, EmailPattern))
            {

                txtEmail.Text = "Valid Email";
                txtEmail.TextColor = Color.Green;
            }
            else
            {
                txtEmail.Text = "InValid Email! Email must contain @ and .com";
                txtEmail.TextColor = Color.Red;
            }
        }

        private void txtPassword_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (e.NewTextValue.Length < 8)
            {
                txtPassword.IsVisible = true;
                txtPassword.Text = "Password should be of at least 8 charaters";
                txtPassword.TextColor = Color.Red;
            }

            var PasswordPattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])";
            if (Regex.IsMatch(e.NewTextValue, PasswordPattern))
            {
                txtPassword.Text = "Strong Password!";
                txtPassword.TextColor = Color.Green;
            }

            else
            {
                txtPassword.Text = "Weak Password! Password should contain Uppercase Letter , Lowercase Letter, Number(s) and special characters [$@$!%*#?&]";
                txtPassword.TextColor = Color.Red;
            }
        }

        private void txtContact_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (e.NewTextValue.Length < 11 || e.NewTextValue.Length > 13)
            {
                txtContact.IsVisible = true;
                txtContact.Text = "InValid Phone! Missing digit(s)";
                txtContact.TextColor = Color.Red;
            }

            else
            {
                txtContact.Text = "Valid Phone";
                txtContact.TextColor = Color.Green;
            }
        }

        //private void txtAddress_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (e.NewTextValue.Length < 15)
        //    {
        //        txtAddress.IsVisible = true;
        //        txtAddress.Text = "Provide Complete Address for Product Selling purpuse!";
        //        txtAddress.TextColor = Color.Red;
        //    }
        //    else
        //    {
        //        txtAddress.Text = "Valid Address";
        //        txtAddress.TextColor = Color.Green;
        //    }
        //}
    }
}