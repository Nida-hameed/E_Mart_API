using Acr.UserDialogs;
using E_Mart.Models;
using E_Mart.Utills;
using E_Mart.Utills.Response;
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
    public partial class Products : ContentPage
    {
        APICall api = new APICall();
        public Products(SHOP_tbl shp)
        {
            InitializeComponent();
            Title = shp.SHOP_NAME;
            LoadData(shp.SHOP_ID); 
        }
        private async void LoadData(int sHOP_ID)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Loading Please Wait...");
                var responseData = await api.CallApiGetAsync<GetCategoryAndProductsByShopIdResponse>("api/shop/GetCategoryAndProductsByShopId/" + sHOP_ID);
                ListData.ItemsSource = responseData.Products;
                ProductCtegories.ItemsSource = responseData.Categories;
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Error: " + ex.Message, "OK");
            }
        }
        private async void collectionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await UpdateSelectionDataAsync(e.CurrentSelection);
        }
        async Task UpdateSelectionDataAsync(IEnumerable<object> currentSelected)
        {
            var selected = currentSelected.FirstOrDefault() as PRO_CATEGORY_tbl;
            await Navigation.PushAsync(new ProcductsByCategory(selected));
        }
        private async void collectionList1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await UpdateSelectionDataAsync1(e.CurrentSelection);
        }
        async Task UpdateSelectionDataAsync1(IEnumerable<object> currentSelected)
        {
            var selected = currentSelected.FirstOrDefault() as PRODUCT_tbl;
            var UserId = 0;
            if(App.LoggedInCustomer != null)
            {
                UserId = App.LoggedInCustomer.CUSTOMER_ID;
            }
            await Navigation.PushAsync(new ProductDetail(selected, UserId));
            
        }
    }
   
}