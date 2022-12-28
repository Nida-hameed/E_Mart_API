using E_Mart.Models;
using E_Mart.Views.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartUpPage : ContentPage
    {
        public StartUpPage()
        {
            InitializeComponent();
        }
        private void btnGetStarted_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new UserSideBar();
            //App.Current.MainPage = new NavigationPage( new Home());
        }
        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {

        }
    }
}