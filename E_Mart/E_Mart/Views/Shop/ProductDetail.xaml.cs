using E_Mart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart.Views.Shop
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetail : ContentPage
    {
        PRODUCT_tbl Pro = new PRODUCT_tbl();
        public ProductDetail(PRODUCT_tbl Product )
        {
            InitializeComponent();
            Pro = Product;
            ProName.Text = Product.PRODUCT_NAME;
            ProDescription.Text = Product.PRODUCT_DESCRIPTION;
            ProPrice.Text = Product.PRODUCT_SALEPRICE.ToString();
            Proimage.Source = Product.ImageURL1;
            ProSpecification.Text= Product.PRODUCT_SPECIFICATIONS;


        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
                var item = Pro;

                int Quantity = 0;
                var QtyRaw = await DisplayActionSheet("Select Quantity", "Close", "", "1", "2", "3", "4", "5", "6", "7", "10","11","12", "15");
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
                    App.Cart.Add(new ORDER_DETAIL_tbl { PRODUCT_FID = item.PRODUCT_ID, PRO_ORDER_QUANTITY = Quantity, PURCHASE_PRICE = item.PRODUCT_PURCHASEPRICE, SALE_PRICE = item.PRODUCT_SALEPRICE });
                    await DisplayAlert("Message", item.PRODUCT_NAME + " is added to cart... ", "OK");

                }
        }
    }
}