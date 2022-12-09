using E_Mart.Models;
using E_Mart.Utills;
using E_Mart.Views.Customer;
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

namespace E_Mart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        APICall api = new APICall();
        public Home()
        {
            InitializeComponent();
            LoadData();
        }
        private async void LoadData()
        {

            try
            {
                var responseData = await api.CallApiGetAsync<List<SHOP_tbl>>("api/SHOP_tbl_API/getshops");
                CollBrand.ItemsSource = responseData;


                var responseData1 = await api.CallApiGetAsync<List<PRODUCT_tbl>>("api/PRODUCT_tbl_API/recentproducts");
                CollNewestProducts.ItemsSource = responseData1;

            }
            catch (Exception ex)
            {

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
                await Navigation.PushAsync(new ProductDetail(selected));
            }

        
    }
}