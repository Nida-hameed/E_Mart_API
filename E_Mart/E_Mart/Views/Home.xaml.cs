using E_Mart.Models;
using E_Mart.Utills;
using E_Mart.User;
using E_Mart.Views.Shop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;
using E_Mart;
using System.Windows.Input;
using Xamarin.Essentials;

namespace Views.E_Mart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        APICall api = new APICall();
        public ICommand TapCommand => new Command<string>(OpenWhatsaap);
        public Home()
        {
            InitializeComponent();
            LoadData();
            BindingContext = this;
        }

        void OpenWhatsaap(string url)
        {
            Launcher.OpenAsync(url);
            //Device.OpenUri(new Uri(url));
        }
        private async void LoadData()
        {

            try
            {
                UserDialogs.Instance.ShowLoading("Loading Please Wait...");
                var responseData = await api.CallApiGetAsync<List<SHOP_tbl>>("api/SHOP_tbl_API/getshops");
                CollBrand.ItemsSource = responseData;


                var responseData1 = await api.CallApiGetAsync<List<PRODUCT_tbl>>("api/PRODUCT_tbl_API/recentproducts");
                CollNewestProducts.ItemsSource = responseData1; 
                
                var responseData2 = await api.CallApiGetAsync<List<PRODUCT_tbl>>("api/PRODUCT_tbl_API/featuredproducts");
                CollMostSelling.ItemsSource = responseData2;
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Error: " + ex.Message, "OK");
            }
        }
            private async void CollBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                await UpdateSelectionDataAsync(e.CurrentSelection);
            }
            async Task UpdateSelectionDataAsync(IEnumerable<object> currentSelected)
            {
                var selected = currentSelected.FirstOrDefault() as SHOP_tbl;
                await Navigation.PushAsync(new Products(selected));
            }


            private async void CollNewestProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                await UpdateSelectionDataAsync2(e.CurrentSelection);
            }

            private async void CollMostSelling_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                await UpdateSelectionDataAsync2(e.CurrentSelection);
            }
            async Task UpdateSelectionDataAsync2(IEnumerable<object> currentSelected)
            {
                var selected = currentSelected.FirstOrDefault() as PRODUCT_tbl;
                await Navigation.PushAsync(new ProductDetail(selected,App.LoggedInCustomer.CUSTOMER_ID));
            }

        
    }
}