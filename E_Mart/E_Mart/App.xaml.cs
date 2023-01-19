using E_Mart.Models;
using E_Mart.Services;
using E_Mart.Views;
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

        //public static SHOP_tbl SelectdShop = null;
        
        public static ITEM_tbl SelectedItem = null;
       
        public static CUSTOMER_tbl passwardreset = null;
        
        public static int code = 0;

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new StartUpPage();
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
