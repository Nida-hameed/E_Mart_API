using E_Mart.Models;
using E_Mart.Utills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart.Seller
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Manage_Products : ContentPage
    {
        APICall api = new APICall();
        public Manage_Products()
        {
            InitializeComponent();
            LoadData();

        }
        private async void LoadData()
        {
            int id = App.LoggedInSeller.SELLER_ID;

            var ProductsList = await api.CallApiGetAsync<List<ITEM_tbl>>("api/ITEM_tbl_API/getlist/" + id);

            ListData.ItemsSource = ProductsList;

        }


        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var Selected = e.Item as ITEM_tbl;
            int itemID = Selected.ITEM_ID;


            //var item = await api.CallApiGetAsync<List<ITEM_tbl>>("api/ITEM_tbl_API/getItem/" + itemID);


            var Choice = await DisplayActionSheet("Options", "Cancel", "OK", "Delete", "View", "Edit");

            if (Choice == "View")
            {

                await DisplayAlert("Detail", "" +
                    //"\nITEM_ID: " + Selected.ITEM_ID +
                    "\nITEM_IMAGE: " + Selected.ImageURL +
                    "\nITEM_NAME: " + Selected.ITEM_NAME +                   
                    "\nITEM_DETAIL: " + Selected.ITEM_DETAIL +
                    "\nITEM_PRICE: " + Selected.ITEM_PRICE +
                    "\nSELLER: " + App.LoggedInSeller.SELLER_NAME, "", "OK");

            }
            if (Choice == "Edit")
            {
                await Navigation.PushAsync(new EditProduct(Selected));

            }
            if (Choice == "Delete")
            {

                var Confirm = await DisplayAlert("Confirmation", "Are you sure you want to delete " + Selected.ITEM_NAME, "Yes", "No");
                if (Confirm)
                {

                    LoadData();
                    ITEM_tbl Item = new ITEM_tbl();
                    Item.ITEM_ID = Selected.ITEM_ID;
                    Item.SELLER_FID = App.LoggedInSeller.SELLER_ID;
                    var result = await api.CallApiDeleteAsync("api/ITEM_tbl_API/DeleteItem"+ Item);
                    await DisplayAlert("Message", Selected.ITEM_NAME + "Deleted Permanently", "OK");

                }
            }
        }
    }
}

