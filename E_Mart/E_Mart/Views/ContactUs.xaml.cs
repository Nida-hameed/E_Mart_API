using E_Mart.Models;
using E_Mart.Shop;
using E_Mart.Utills;
using E_Mart.Views.Customer;
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
	public partial class ContactUs : ContentPage
	{
        APICall api = new APICall();
        public ContactUs ()
		{
			InitializeComponent ();
               
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
                MESSAGE_tbl msg = new MESSAGE_tbl();
                msg.NAME = txtName.Text;
                msg.EMAIL = txtEmail.Text;
                msg.SUBJECT = txtSubject.Text;
                msg.MESSAGE_BODY = txtMessage.Text;
                var responseData = await api.CallApiPostAsync("api/MESSAGE_API/postMessage", msg);
                if (responseData != null) 
                {
                    await DisplayAlert("Message", "Greetings \n Dear user your message has been sent.We will contact you soon.\n Regards: E-Mart", "OK");
                }
                else
                {
                    await Navigation.PushAsync(new Login());
                }
            }
            catch (Exception ex)
            {
                //ProgressInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Error: " + ex.Message, "OK");
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        //private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        //{

        //}
    }
}