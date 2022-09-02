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
                var uri = App.APIBaseURL + "api/SHP_CATEGORY_tbl_API";
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


                //foreach (var item in list)
                //{
                //    var uri2 = App.APIBaseURL + "api/api/PRODUCT_tbl_API/?id=" + item.SHOP_FID;
                //    var result2 = await client.GetStringAsync(uri2);
                //    PRODUCT_tbl list2 = JsonConvert.DeserializeObject<PRODUCT_tbl>(result2);

                //    list2.PRODUCT_NAME = item.PRODUCT_NAME;
                //    list2.SHOP_ID = item.SHOP_ID;
                //    list2.PRODUCT_SALEPRICE = item.PRODUCT_SALEPRICE;
                //    RefinedList.Add(list2);
                //}
                //ListData.ItemsSource = RefinedList;
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
            var selected = currentSelected.FirstOrDefault() as SHOP_tbl;
            await Navigation.PushAsync(new Products(selected));
        }
    }
}