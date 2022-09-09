using E_Mart.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart.Views.Shop
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Shops : ContentPage
    {
        public Shops()
        {
            InitializeComponent();
            LoadData();
            LoadData1();
        }
        private async void LoadData1()
        {
            try
            {
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, certificate, chain, sslPolicyErrors) => true;
                var client = new HttpClient(httpClientHandler);
                var uri = App.APIBaseURL + "api/AllPurpose";
                var result = await client.GetStringAsync(uri);
                List<SHP_CATEGORY_tbl> list = JsonConvert.DeserializeObject<List<SHP_CATEGORY_tbl>>(result);
                ShopCategories.ItemsSource = list;

            }

            catch (Exception ex)
            {

                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Error: " + ex.Message, "OK");
            }
        }
        private async void LoadData()
        {
            try
            {
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, certificate, chain, sslPolicyErrors) => true;
                var client = new HttpClient(httpClientHandler);
                var uri = App.APIBaseURL + "api/SHOP_tbl_API/getshops";
                var result = await client.GetStringAsync(uri);
                List<SHOP_tbl> list = JsonConvert.DeserializeObject<List<SHOP_tbl>>(result);
                
                ListData.ItemsSource = list;
            }

            catch (Exception ex)
            {

                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Error: " + ex.Message, "OK");
            }
        }
           
       
        private async void collectionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await UpdateSelectionDataAsync(e.CurrentSelection);
        }
        async Task UpdateSelectionDataAsync(IEnumerable<object> currentSelected)
        {
            var selected = currentSelected.FirstOrDefault() as SHP_CATEGORY_tbl;
            App.Current.MainPage =  new NavigationPage(new ShopsByCategory(selected));
            
        } 
        private async void collectionList1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await UpdateSelectionDataAsync1(e.CurrentSelection);
        }
        async Task UpdateSelectionDataAsync1(IEnumerable<object> currentSelected)
        {
            var selected = currentSelected.FirstOrDefault() as SHOP_tbl;
            App.Current.MainPage =  new NavigationPage(new Products(selected));      
        }
    }
}