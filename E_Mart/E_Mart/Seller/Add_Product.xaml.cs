using Acr.UserDialogs;
using E_Mart.Models;
using E_Mart.Utills;
using Firebase.Storage;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart.Seller
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Add_Product : ContentPage
    {
        public static string PicPath = "ImagePicker.png";

        private MediaFile _mediaFile;

        APICall api = new APICall();
        public Add_Product()
        {
            InitializeComponent();
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
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
                        await DisplayAlert("Error", "Error Picking Image...", "OK");
                        return;
                    }

                    PicPath = SelectedImg.Path;
                    PreviewPic.Source = SelectedImg.Path;
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
                    PicPath = SelectedImg.Path;
                    PreviewPic.Source = SelectedImg.Path;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", "Something Went wrong \n" + ex.Message, "OK");
            }
        }

        private async void btnAddProduct_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Loading Please Wait...");

                //var task = new FirebaseStorage("e-mart1.appspot.com", new FirebaseStorageOptions
                //{ ThrowOnCancel = true })
                //    .Child(PicPath);
                ////.PutAsync(await PicPath.OpenReadAsync());
                //var Image = task;


                var Content = new MultipartFormDataContent();
                Content.Add(new StreamContent(_mediaFile.GetStream()), "\"file\"", $"\"{_mediaFile.Path}\"");
                string ResponseMessage = await api.CallApiPostimageAsync("api/ITEM_tbl/postimage", Content);
                string Image = ResponseMessage.Substring(1, ResponseMessage.Length - 2);

                if (string.IsNullOrEmpty(txtItemName.Text) || string.IsNullOrEmpty(txtItemDetail.Text) || string.IsNullOrEmpty(txtItemPrice.Text))
                {
                    await DisplayAlert("Error", "Please fillout all requried fields and Try Again!", "OK");
                    return;
                }
                ITEM_tbl Item = new ITEM_tbl()
                {
                    ITEM_NAME = txtItemName.Text,
                    ITEM_DETAIL = txtItemDetail.Text,
                    ITEM_PRICE = decimal.Parse(txtItemPrice.Text),
                    ImageURL = Image,
                    SELLER_FID = App.LoggedInSeller.SELLER_ID,
                };
                var responseData = await api.CallApiPostAsync("api/ITEM_tbl_API/AddItem", Item);
                if(responseData != null)
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Success", "Product Added.", "OK");
                    await Navigation.PushAsync(new Manage_Products());
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Message", "Product not added! Please Check your internet connection or try again later.", "OK");
                }
               
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Erroe: " + ex.Message, "OK");

            }
        }
    }
}



