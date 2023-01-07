using Acr.UserDialogs;
using E_Mart.CustomerLoginSystem;
using E_Mart.Models;
using E_Mart.Seller;
using E_Mart.Shop;
using E_Mart.Utills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactUs : ContentPage
	{
        public ICommand TapCommand => new Command<string>(OpenTapped);

        public ContactUs ()
		{
			InitializeComponent ();
            BindingContext = this;

        }
        void OpenTapped(string url)
        {
            Launcher.OpenAsync(url);
            //Device.OpenUri(new Uri(url));
        }

        private void btnmessage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Message());
        }
    }
}