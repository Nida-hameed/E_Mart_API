using E_Mart.Services;
using E_Mart.Views;
using E_Mart.Views.Customer;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart
{
    public partial class App : Application
    {
        public static string APIBaseURL = "https://mall.damascuschefknives.com/";
     
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new Register();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
