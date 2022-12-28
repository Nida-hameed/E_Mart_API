using E_Mart.Models;
using E_Mart.Services;
using E_Mart.Views;
using Plugin.FirebasePushNotification;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart
{
    public partial class App : Application
    {
         //public static string APIBaseURL = "https://mall.damascuschefknives.com/";
        public static string APIBaseURL = "https://mall.bollywoodlounge.nu/";

        public static CUSTOMER_tbl LoggedInCustomer = null;
       
        public static SELLER_tbl LoggedInSeller = null;


        public static List<ORDER_DETAIL_tbl> Cart = new List<ORDER_DETAIL_tbl>();
        
        public static decimal? Total = 0;

        public static SHOP_tbl SelectdShop = null;
        
       // public static PRODUCT_tbl Selectd = null;

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new StartUpPage();

            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            };
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {

                System.Diagnostics.Debug.WriteLine("Received");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }

            };
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
