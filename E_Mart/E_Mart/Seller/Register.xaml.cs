﻿using Acr.UserDialogs;
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
                    STATUS=true,
                };

                var responseData = await api.CallApiPostAsync<SELLER_tbl>("api/SELLER_tbl_API/postseller",seller);


                if (responseData != null)
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

        //private void txtAddress_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (e.NewTextValue.Length < 15)
        //    {
        //        lblAddress.IsVisible = true;
        //        lblAddress.Text = "Provide Complete Address for Product Selling purpuse!";
        //        lblAddress.TextColor = Color.Red;
        //    }
        //    else
        //    {
        //        lblAddress.Text = "Valid Address";
        //        lblAddress.TextColor = Color.Green;
        //    }
        //}
    }
}