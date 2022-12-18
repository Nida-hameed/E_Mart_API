using E_Mart.Models;
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
    public partial class ViewProduct : ContentPage
    {
        public ViewProduct(ITEM_tbl item)
        {
            InitializeComponent();
            ProImage.Source = item.ImageURL;
            ProName.Text = item.ITEM_NAME;
            ProDetail.Text = item.ITEM_DETAIL;
            ProPrice.Text = item.ITEM_PRICE.ToString();

        }
       
    }
}