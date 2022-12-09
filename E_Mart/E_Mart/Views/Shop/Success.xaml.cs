using E_Mart.Views.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart.Shop
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Success : ContentPage
    {
        public Success()
        {
            InitializeComponent();
            App.Cart.Clear();
            
        }

        private void btnOrderCompleted_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new CustomerSideBar();
        }
    }
}