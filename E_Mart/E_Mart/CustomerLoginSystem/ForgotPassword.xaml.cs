using Acr.UserDialogs;
using Newtonsoft.Json;
using E_Mart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using E_Mart.Utills;
using E_Mart;
using E_Mart.Utils;

namespace E_Mart.CustomerLoginSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPassword : ContentPage
    {
        APICall api = new APICall();
        public ForgotPassword()
        {
            InitializeComponent();
        }
        private async void btnReset_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Loading Please Wait...");

                // Required Field Validator =======================================================================
                if (string.IsNullOrEmpty(txtEmailReset.Text))
                {
                    await DisplayAlert("Message", "Please Enter Email", "OK");
                    return;
                }

              
               
                var check = await api.CallApiGetAsync<CUSTOMER_tbl>("api/CUSTOMER_tbl_API/forgotpassword?email=" + txtEmailReset.Text);
                if (check == null)
                {
                    await DisplayAlert("Message", "The email you have entered is not registered.", "OK");
                    return;
                }

                MailProvider.SendEmail(check.CUSTOMER_EMAIL, "Password Forgot Request", "Dear " + check.CUSTOMER_NAME + "!< br />Your Account Credentials are :<br/>" + check.CUSTOMER_EMAIL + "<br/>"+ check.CUSTOMER_PASSWORD + " <br/> Regards: E-Mart");
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Message", "Your Login Details are sent to your email address please find that in your inbox", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Message", "Somthing went wrong this may be a problem with internet or application please ensure that you have a working internet connectiony . \nError Details : " + ex.Message, "OK");
            }
        }

        private async void btnPassReset_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Loading Please Wait...");
            // Required Field Validator =======================================================================
            if (string.IsNullOrEmpty(txtEmailReset.Text))
            {
                await DisplayAlert("Message", "Please Enter Email", "OK"); 
                UserDialogs.Instance.HideLoading();
                return;
            }
            var check = await api.CallApiGetAsync<CUSTOMER_tbl>("api/CUSTOMER_tbl_API/forgotpassword?email=" + txtEmailReset.Text);
            if (check.CUSTOMER_ID==0)
            {
                await DisplayAlert("Message", "The email you have entered is not registered.", "OK");
                UserDialogs.Instance.HideLoading();
                return;
            }

            Random random = new Random();
            int code = random.Next(1001, 9999);
            App.code = code;
            App.passwardreset = check;
            MailProvider.SendEmail(check.CUSTOMER_EMAIL, "Passward Reset Code", "Dear " + check.CUSTOMER_NAME + "!< br />Please verify this code " + code +".<br/> Regards: E-Mart");
            UserDialogs.Instance.HideLoading();
            await Navigation.PushAsync(new VerifyCode());
        }
    }
}