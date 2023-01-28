using E_Mart.Seller;
using E_Mart.Views;
using E_Mart.Views.Shop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Views.E_Mart;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart.LoggedInSeller
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellerSideBarFlyout : ContentPage
    {
        public ListView ListView;

        public SellerSideBarFlyout()
        {
            InitializeComponent();

            BindingContext = new SellerSideBarFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class SellerSideBarFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<SellerSideBarFlyoutMenuItem> MenuItems { get; set; }

            public SellerSideBarFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<SellerSideBarFlyoutMenuItem>(new[]
                {
                    new SellerSideBarFlyoutMenuItem { Id = 0, Title = "Home", TargetType= typeof(Home) },
                    new SellerSideBarFlyoutMenuItem { Id = 1, Title = "Shops",TargetType= typeof(Shops)   },
                    new SellerSideBarFlyoutMenuItem { Id = 2, Title = "Contact Us" , TargetType= typeof(ContactUs)},
                    new SellerSideBarFlyoutMenuItem { Id = 3, Title = "Login & Register", TargetType= typeof(Login)},
                    new SellerSideBarFlyoutMenuItem { Id = 4, Title = "B-Pair", TargetType= typeof(BPairItems.BPairItems)},
                    new SellerSideBarFlyoutMenuItem { Id = 5, Title = "Seller Account", TargetType= typeof(Seller.Login)},
                    new SellerSideBarFlyoutMenuItem { Id = 6, Title = "Manage Products", TargetType= typeof(Seller.Manage_Products)},
                    new SellerSideBarFlyoutMenuItem { Id = 6, Title = "Settings", TargetType= typeof(Seller.Settingspage)},
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}