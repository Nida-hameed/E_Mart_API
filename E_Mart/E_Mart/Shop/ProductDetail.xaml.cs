using E_Mart.Models;
using E_Mart.Shop;
using E_Mart.Utills;
using E_Mart.CustomerLoginSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart.Views.Shop
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetail : ContentPage
    {
        APICall api = new APICall();
        public static int idpro;
        public static int UID;

        PRODUCT_tbl Pro = new PRODUCT_tbl();
        public ProductDetail(PRODUCT_tbl Product, int? UID)
        {
            InitializeComponent();



            idpro = Product.PRODUCT_ID;

            Pro = Product;
            ProName.Text = Product.PRODUCT_NAME;
            ProDescription.Text = Product.PRODUCT_DESCRIPTION;
            ProPrice.Text = Product.PRODUCT_SALEPRICE.ToString();
            Proimage.Source = Product.ImageURL1;
            ProSpecification.Text = Product.PRODUCT_SPECIFICATIONS;

            btnWishlist.BackgroundColor = Color.FromHex("#ff6f61");

        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();
            if (await CheckFromWishlist(Pro, UID))
            {
                btnWishlist.BackgroundColor = Color.FromHex("#ff6f61");

            }
            else
            {
                btnWishlist.BackgroundColor = Color.FromHex("ffffff");

            }

        }

        async Task<bool> CheckFromWishlist(PRODUCT_tbl Product, int ID)
        {
            var responseData = await api.CallApiGetAsync<List<WISHLIST_tbl>>("api/WISHLIST_tbl_API/getlist/" + ID);

            var check = responseData.FirstOrDefault(x => x.PRODUCT_FID == Product.PRODUCT_ID);


            if (check == null)
            {
                return false;
            }
            else
            {
                return true;
            }


        }

        
        private async void Button_Clicked(object sender, EventArgs e)
        {
            var item = Pro;
            int Quantity = 0;
            var QtyRaw = await DisplayActionSheet("Select Quantity", "Close", "", "1", "2", "3", "4", "5", "6", "7", "10", "11", "12", "15");
            if (QtyRaw != "Close" && QtyRaw != null)
            {
                Quantity = int.Parse(QtyRaw);
            }
            else
            {
                await DisplayAlert("Message", "Please select at least 1 quantity. ", "OK");
                return;
            }
            int index = -1;
            for (int i = 0; i < App.Cart.Count; i++)
            {
                if (item.PRODUCT_ID == App.Cart[i].PRODUCT_FID)
                {
                    index = i;
                    var ques = await DisplayAlert("Message", item.PRODUCT_NAME + " is already entered in Cart do you want to increase the quantity?", "Yes", "No");
                    if (ques)
                    {
                        App.Cart[index].PRO_ORDER_QUANTITY += Quantity;
                        await DisplayAlert("Message", item.PRODUCT_NAME + " Quantity Increased!", "OK");
                    }
                }
            }
            if (index == -1)
            {
                App.Cart.Add(new ORDER_DETAIL_tbl { PRODUCT_FID = item.PRODUCT_ID, PRO_ORDER_QUANTITY = Quantity, PURCHASE_PRICE = item.PRODUCT_PURCHASEPRICE, SALE_PRICE = item.PRODUCT_SALEPRICE, PRODUCT_NAME = item.PRODUCT_NAME, ImageURL1 = item.ImageURL1 });
                await DisplayAlert("Message", item.PRODUCT_NAME + " is added to cart... ", "OK");
            }
        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CartControls());
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
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
                    WishListRequest wishlist = new WishListRequest();
                    wishlist.CUSTOMER_ID = App.LoggedInCustomer.CUSTOMER_ID;
                    wishlist.PRODUCT_ID = idpro;

                    var responseData = await api.CallApiPostAsync("api/WISHLIST_tbl_API/AddToWishlist", wishlist);

                    if (btnWishlist.BackgroundColor == Color.FromHex("#ff6f61"))
                    {
                        btnWishlist.BackgroundColor = Color.White;
                    }
                    else
                    {
                        btnWishlist.BackgroundColor = Color.FromHex("#ff6f61");
                    }
                }
            }
            catch (Exception ex)
            {
                //ProgressInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Error: " + ex.Message, "OK");
            }
        }
    }
}