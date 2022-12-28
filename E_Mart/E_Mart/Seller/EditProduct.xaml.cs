using Acr.UserDialogs;
using E_Mart.Models;
using E_Mart.Utills;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart.Seller
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProduct : ContentPage
    {
        APICall api = new APICall();
        
        public static bool isnewpictureselected = false;
        private MediaFile _mediaFile;
        public string image;
        public static int ItemId;

        public EditProduct(ITEM_tbl item)
        {
            InitializeComponent();

            ItemId = item.ITEM_ID;

            ItemImage.Source = item.ImageURL;
            txtItemName.Text = item.ITEM_NAME;
            txtItemDetail.Text = item.ITEM_DETAIL;
            txtItemPrice.Text = item.ITEM_PRICE.ToString();

        }
        private async void btneditProduct_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Loading Please Wait...");

                if (isnewpictureselected == true)
                {
                    var content = new MultipartFormDataContent();
                    content.Add(new StreamContent(_mediaFile.GetStream()), "\"file\"", $"\"{_mediaFile.Path}\"");

                    string ResponseMessage = await api.CallApiPostimageAsync("api/ITEM_tbl/postimage", content);
                    image = ResponseMessage.Substring(1, ResponseMessage.Length - 2);

                }

                var Item = new ITEM_tbl
                {
                    ITEM_ID = ItemId,
                    ITEM_NAME = txtItemName.Text,
                    ITEM_DETAIL = txtItemDetail.Text,
                    ITEM_PRICE = decimal.Parse(txtItemPrice.Text),
                    ImageURL = image,
                };

                var modifiedlist = await api.CallApiPostAsync<ITEM_tbl>("api/PRODUCT/EditItem", Item);
              
                if (modifiedlist != null)
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Success", "Successfully Update Profile", "OK");
                    await Navigation.PushAsync(new Manage_Products());
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Error", "Somthing went wrong! Please try again later.", "OK");
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("message", "Error Occured please try again later:" + ex.Message, "ok");
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                var response = await DisplayActionSheet("Select Image Source", "Close", "", "From Gallery", "From Camera");
                if (response == "From Camera")
                {
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsTakePhotoSupported)
                    {
                        await DisplayAlert("Error", "Phone is not Take Photo Supported", "OK");
                        return;
                    }

                    var mediaOptions = new StoreCameraMediaOptions()
                    {
                        PhotoSize = PhotoSize.Medium
                    };

                    var SelectedImg = await CrossMedia.Current.TakePhotoAsync(mediaOptions);

                    if (SelectedImg == null)
                    {
                        await DisplayAlert("Error", "Error Taking Image...", "OK");
                        return;
                    }
                    _mediaFile = SelectedImg;

                    ItemImage.Source = SelectedImg.Path;
                    isnewpictureselected = true;
                }
                if (response == "From Gallery")
                {
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        await DisplayAlert("Error", "Phone is not Pick Photo Supported", "OK");
                        return;
                    }

                    var mediaOptions = new PickMediaOptions()
                    {
                        PhotoSize = PhotoSize.Medium
                    };

                    var SelectedImg = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

                    if (SelectedImg == null)
                    {
                        await DisplayAlert("Error", "Error Picking Image...", "OK");
                        return;
                    }
                    _mediaFile = SelectedImg;

                    ItemImage.Source = SelectedImg.Path;
                    isnewpictureselected = true;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", "Something Went wrong \n" + ex.Message, "OK");

            }
        }
    }
}