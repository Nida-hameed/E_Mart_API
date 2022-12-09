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
    public partial class BPairItems : ContentPage
    {
        APICall api = new APICall();
        public BPairItems()
        {
            InitializeComponent();
            LoadData();
        }
        private async void LoadData()
        {

            try
            {
                var responseData = await api.CallApiGetAsync<List<ITEM_tbl>>("api/ITEM_tbl_API/");
                ListData.ItemsSource = responseData;

            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Error: " + ex.Message, "OK");
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (App.LoggedInSeller == null)
            {
                var q = await DisplayAlert("Message", "You have to login to sell your product.\n\nLog in Now?", "Yes", "No");
                if (q)
                {
                    await Navigation.PushAsync(new Login());
                }
            }
            else
            {
                await Navigation.PushAsync(new Add_Product());

           }
        }
    }
}