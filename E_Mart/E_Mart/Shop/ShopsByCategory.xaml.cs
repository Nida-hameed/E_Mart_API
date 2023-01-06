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
    public partial class ShopsByCategory : ContentPage
    {
     

        APICall api = new APICall();
        public ShopsByCategory(SHP_CATEGORY_tbl s)
        {
            InitializeComponent();
            Title = s.SHP_CATEGORY_NAME;
            LoadData(s.SHP_CATEGORY_ID);
            
        }


        private async void LoadData(int sHP_CATEGORY_ID)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Loading Please Wait...");
                var responseData = await api.CallApiGetAsync<List<SHOP_tbl>>("api/SHOP_tbl_API/category/" + sHP_CATEGORY_ID);
                ListData.ItemsSource = responseData;
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
            var selected = currentSelected.FirstOrDefault() as SHOP_tbl;
            //App.Current.MainPage = new NavigationPage(new Products(selected));
            await Navigation.PushAsync(new Products(selected));

        }


    }
}