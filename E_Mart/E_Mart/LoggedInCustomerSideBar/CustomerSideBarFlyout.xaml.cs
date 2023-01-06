using E_Mart.Customer;
using E_Mart.Views;
using E_Mart.Views.Shop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Views.E_Mart;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart.LoggedInCustomer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerSideBarFlyout : ContentPage
    {
        public ListView ListView;

        public CustomerSideBarFlyout()
        {
            InitializeComponent();

            BindingContext = new CustomerSideBarFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class CustomerSideBarFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<CustomerSideBarFlyoutMenuItem> MenuItems { get; set; }

            public CustomerSideBarFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<CustomerSideBarFlyoutMenuItem>(new[]
                {
                    new CustomerSideBarFlyoutMenuItem { Id = 0, Title = "Home", TargetType= typeof(Home) },
                    new CustomerSideBarFlyoutMenuItem { Id = 1, Title = "Shops",TargetType= typeof(Shops)   },
                    new CustomerSideBarFlyoutMenuItem { Id = 2, Title = "Contact Us" , TargetType= typeof(ContactUs)},
                    new CustomerSideBarFlyoutMenuItem { Id = 3, Title = "Login & Register", TargetType= typeof(CustomerLoginSystem.Login)},
                    new CustomerSideBarFlyoutMenuItem { Id = 4, Title = "Wishlist", TargetType= typeof(Wishlist)},
                    new CustomerSideBarFlyoutMenuItem { Id = 5, Title = "B-Pair", TargetType= typeof(BPairItems.BPairItems)},
                    new CustomerSideBarFlyoutMenuItem { Id = 6, Title = "Seller Account", TargetType= typeof(Seller.Login)},
                    new CustomerSideBarFlyoutMenuItem { Id = 7, Title = "Settings", TargetType= typeof(Settingspage)},
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