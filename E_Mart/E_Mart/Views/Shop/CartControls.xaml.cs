using E_Mart.Models;
using E_Mart.Views.Customer;
using E_Mart.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using E_Mart.Views.Shop;

namespace E_Mart.Shop
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartControls : ContentPage
    {
        public CartControls()
        {
            InitializeComponent();
            try
            {
                UpdateCartAsync();
            }
            catch (Exception ex)
            {
                DisplayAlert("Message", "Something went wrong this may be a problem with internet or application please ensure that you have a working internet connection and GPS enabled. \nError Details : " + ex.Message, "OK");
            }
        }

        private void UpdateCartAsync()
        {

            List<Cartcell_VM> CartItems = new List<Cartcell_VM>();
            decimal? Amount = 0;
            lblcount.Text = App.Cart.Count().ToString();
            foreach (var item in App.Cart)
            {
                
                decimal? total = item.SALE_PRICE * (item.PRO_ORDER_QUANTITY);
                Amount += total;

                CartItems.Add(new Cartcell_VM
                {
                    ID = item.PRODUCT_FID,
                    image = item.ImageURL1,
                    Name = item.PRODUCT_NAME ,
                    Detail = "Rs. " + item.SALE_PRICE + " X  " + item.PRO_ORDER_QUANTITY + "      Total = " + total.ToString() + "Rs"
                });
            }


            App.Total = Amount;

            lblTotal.Text = Amount.ToString();
            DataList.ItemsSource = CartItems;

        }

        private async void btnRemove_Clicked(object sender, EventArgs e)
        {
            try
            {
                ImageButton btn = sender as ImageButton;
                var item = btn.CommandParameter as Cartcell_VM;

                for (int i = 0; i < App.Cart.Count; i++)
                {
                    if (App.Cart[i].PRODUCT_FID == item.ID)
                    {
                        var res = await DisplayAlert("Question", "Are you sure you want to remove " + item.Name + "?", "Yes", "No");
                        if (res)
                        {
                            App.Cart.RemoveAt(i);
                        }
                    }
                }

                UpdateCartAsync();

            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", "Somthing went wrong this may be a problem with internet or application please ensure that you have a working internet connection and GPS enabled. \nError Details : " + ex.Message, "OK");
            }

        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (App.Cart.Count < 1)
            {
                await DisplayAlert("Message", "Cart Page is Empty Please add at least one item in cart", "OK");
                return;
            }

            if (App.LoggedInCustomer == null)
            {
                var q = await DisplayAlert("Message", "You have to login for to place order.\n\nLog in Now?", "Yes", "No");
                if (q)
                {
                    await Navigation.PushAsync(new Login());
                }
            }
            else
            {
                await Navigation.PushAsync(new ConfirmOrder());
            }




        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Shops());
        }

        //private async void Button_Clicked_1(object sender, EventArgs e)
        //{
            
        //    if (App.Cart.Count < 1)
        //    {
        //        await DisplayAlert("Message", "Cart Page is Empty Please add at least one item in cart", "OK");
        //        return;
        //    }

        //    if (App.LoggedInCustomer == null)
        //    {
        //        var q = await DisplayAlert("Message", "You have to login for to place order.\n\nLog in Now?", "Yes", "No");
        //        if (q)
        //        {
        //            await Navigation.PushAsync(new Login());
        //        }
        //    }
        //    else
        //    {
        //        //await Navigation.PushAsync(new Payment());
        //    }
        //}
    }
}