using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Mart.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using E_Mart.Utills;
using E_Mart.Utils;

namespace E_Mart.CustomerLoginSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasswordReset : ContentPage
    {
        APICall api = new APICall();
        public PasswordReset()
        {
            InitializeComponent();
        }

        private async void btnReset_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Loading Please Wait...");
                // Required Field Validator =======================================================================
                if (string.IsNullOrEmpty(txtpassword.Text))
                {
                    await DisplayAlert("Message", "Please Enter new Password", "OK");
                    UserDialogs.Instance.HideLoading();
                    return;
                }

                CUSTOMER_tbl customer = App.passwardreset;
                customer.CUSTOMER_PASSWORD = txtpassword.Text;

                var modifiedlist = await api.CallApiPostAsync("api/CUSTOMER_tbl_API/editCustomer", customer);
                

                if (modifiedlist !=null)
                {
                    MailProvider.SendEmail(customer.CUSTOMER_EMAIL, "Password Resetted" , "Dear " + customer.CUSTOMER_NAME + "!!Your password has been successfully changed.<br/> Regards: E-Mart");
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Successfully", " Your Password has been successfully resetted " + customer.CUSTOMER_EMAIL + " again", "OK");
                    App.code = 0;
                    App.passwardreset = null;
                    App.Current.MainPage = new NavigationPage(new Login());
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Error", "Somthing went wrong!!", "OK");
                    App.Current.MainPage = new NavigationPage(new Login());
                }

            }
            catch
            {

            }

        }
    }
}