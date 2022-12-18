using E_Mart.Models;
using E_Mart.Utills;
using E_Mart.Utills.Response;
using E_Mart.Views.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart.Views.Customer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Wishlist : ContentPage
    {
        APICall api = new APICall();
        public static int ID = App.LoggedInCustomer.CUSTOMER_ID;
        public Wishlist()
        {
            InitializeComponent();
            LoadData();
            //int ID = App.LoggedInCustomer.CUSTOMER_ID;
        }
        private async void LoadData()
        {
            try
            {
                var responseData = await api.CallApiGetAsync<List<WISHLIST_tbl>>("api/WISHLIST_tbl_API/getlist/" + ID);
                ListData.ItemsSource = responseData.Select(X => X.PRODUCT_tbl).ToList();
            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Error: " + ex.Message, "OK");
            }
        }
        private async void collectionList1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await UpdateSelectionDataAsync1(e.CurrentSelection);
        }
        async Task UpdateSelectionDataAsync1(IEnumerable<object> currentSelected)
        {
            var selected = currentSelected.FirstOrDefault() as PRODUCT_tbl;
            await Navigation.PushAsync(new ProductDetail(selected));

        }
    }
}