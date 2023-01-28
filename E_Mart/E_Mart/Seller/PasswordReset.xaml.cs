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

namespace E_Mart.Seller 
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

                SELLER_tbl Seller = App.passwordreset;
                Seller.SELLER_PASSWORD = txtpassword.Text;

                var modifiedlist = await api.CallApiPostAsync("api/SELLER_tbl_API/editSeller", Seller);
                

                if (modifiedlist !=null)
                {
                    MailProvider.SendEmail(Seller.SELLER_EMIAL, "Password Resetted" , "Dear " + Seller.SELLER_NAME + "!!Your password has been successfully changed.<br/> Regards: E-Mart");
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Successfully", " Your Password has been successfully resetted " + Seller.SELLER_EMIAL + " again", "OK");
                    App.code = 0;
                    App.passwordreset = null;
                    App.Current.MainPage = new NavigationPage(new Seller.Login());
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Error", "Somthing went wrong!!", "OK");
                    App.Current.MainPage = new NavigationPage(new Seller.Login());
                }

            }
            catch
            {

            }

        }
    }
}