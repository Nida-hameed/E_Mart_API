using Acr.UserDialogs;
using E_Mart.CustomerLoginSystem;
using E_Mart.Models;
using E_Mart.Utills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Message : ContentPage
    {
        APICall api = new APICall();
        public Message()
        {
            InitializeComponent();
        }

        private async void btnSend_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtMessage.Text) || string.IsNullOrEmpty(txtSubject.Text))
            {
                await DisplayAlert("Error", "Please fillout all requried fields!", "OK");
                return;
            }
            try
            {
                UserDialogs.Instance.ShowLoading("Loading Please Wait...");

                MESSAGE_tbl msg = new MESSAGE_tbl();
                msg.NAME = txtName.Text;
                msg.EMAIL = txtEmail.Text;
                msg.SUBJECT = txtSubject.Text;
                msg.MESSAGE_BODY = txtMessage.Text;

                var responseData = await api.CallApiPostAsync("api/MESSAGE_API/postMessage", msg);
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Message", "Greetings \n Dear user your message has been sent.We will contact you soon.\n Regards: E-Mart", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Error: " + ex.Message, "OK");
            }
        }
    }

}