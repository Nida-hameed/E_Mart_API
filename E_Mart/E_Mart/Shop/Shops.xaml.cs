using Acr.UserDialogs;
using E_Mart.Models;
using E_Mart.Utills;
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
        APICall api = new APICall();
        public Shops()
        {
            InitializeComponent();
            LoadData();
        }
        private async void LoadData()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Loading Please Wait...");

                var list = await api.CallApiGetAsync<List<SHP_CATEGORY_tbl>>("api/AllPurpose");
                ShopCategories.ItemsSource = list;
                
                var Shoplist = await api.CallApiGetAsync<List<SHOP_tbl>>("api/SHOP_tbl_API/getshops");
                ListData.ItemsSource = Shoplist;
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
            var selected = currentSelected.FirstOrDefault() as SHP_CATEGORY_tbl;
            //App.SelectdShop = selected;
            await Navigation.PushAsync(new ShopsByCategory(selected));
            

        } 
        private async void collectionList1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await UpdateSelectionDataAsync1(e.CurrentSelection);
        }
        async Task UpdateSelectionDataAsync1(IEnumerable<object> currentSelected)
        {
            var selected = currentSelected.FirstOrDefault() as SHOP_tbl;
            //App.SelectdShop = selected;

            //App.Current.MainPage =  new NavigationPage(new Products(selected));
            await Navigation.PushAsync(new Products(selected));
        }
    }
}