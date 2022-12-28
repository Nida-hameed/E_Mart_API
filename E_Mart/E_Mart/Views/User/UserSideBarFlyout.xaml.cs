using E_Mart.Seller;
using E_Mart.Views.Shop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart.Views.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserSideBarFlyout : ContentPage
    {
        public ListView ListView;

        public UserSideBarFlyout()
        {
            InitializeComponent();

            BindingContext = new UserSideBarFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class UserSideBarFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<UserSideBarFlyoutMenuItem> MenuItems { get; set; }

            public UserSideBarFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<UserSideBarFlyoutMenuItem>(new[]
                {
                    new UserSideBarFlyoutMenuItem { Id = 0, Title = "Home", TargetType= typeof(Home) },
                    new UserSideBarFlyoutMenuItem { Id = 1, Title = "Shops",TargetType= typeof(Shops)   },
                    new UserSideBarFlyoutMenuItem { Id = 2, Title = "Contact Us" , TargetType= typeof(ContactUs)},
                    new UserSideBarFlyoutMenuItem { Id = 3, Title = "Login & Register", TargetType= typeof(CustomerLoginSystem.Login)},
                    new UserSideBarFlyoutMenuItem { Id = 4, Title = "B-Pair", TargetType= typeof(BPairItems.BPairItems)},
                    new UserSideBarFlyoutMenuItem { Id = 5, Title = "Seller Account", TargetType= typeof(Seller.Login)},

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