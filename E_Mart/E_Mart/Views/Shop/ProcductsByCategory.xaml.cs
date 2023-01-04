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
    public partial class ProcductsByCategory : ContentPage
    {
        APICall api = new APICall();
        public ProcductsByCategory(PRO_CATEGORY_tbl p )
        {
            InitializeComponent();
            LoadData(p.PRO_CATEGORY_ID);
            Title = p.PRO_CATEGORY_NAME;
        }

        private async void LoadData(int pRO_CATEGORY_ID)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Loading Please Wait...");
                var responseData =await api.CallApiGetAsync<List<PRODUCT_tbl>>("api/PRODUCT_tbl_API/ProductByCategory/" + pRO_CATEGORY_ID);
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
            var selected = currentSelected.FirstOrDefault() as PRODUCT_tbl;
            await Navigation.PushAsync(new ProductDetail(selected, App.LoggedInCustomer.CUSTOMER_ID));
        }


    }
}