using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BipBip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UploadImage : ContentPage
    {
        public UploadImage()
        {

            InitializeComponent();

        }


        private async void btn_Clicked(object sender, EventArgs e)
        {
            double _scale = 0;
            _scale = gridImageOption.Scale;
            sl.IsEnabled = false;
            await gridImageOption.ScaleTo(0, 500);
            gridImageOption.IsVisible = true;
            await gridImageOption.ScaleTo(_scale, 300);
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            CloseDialog();
        }

        private void CloseDialog()
        {
            sl.IsEnabled = true;
            gridImageOption.IsVisible = false;
        }

        private async void ctrlCamera_Tapped(object sender, EventArgs e)
        {
            try
            {
                var result = await MediaPicker.CapturePhotoAsync();

                if (result != null)
                {
                    var stream = await result.OpenReadAsync();

                    img.Source = ImageSource.FromStream(() => stream);
                    confirmButton.IsVisible = true; // Rendre le bouton de confirmation visible
                }
                CloseDialog();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Demo", ex.Message, "OK");
            }
        }

        private async void ctrlGallery_Tapped(object sender, EventArgs e)
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Please pick a photo"
                });

                if (result != null)
                {
                    var stream = await result.OpenReadAsync();

                    img.Source = ImageSource.FromStream(() => stream);
                    confirmButton.IsVisible = true; // Rendre le bouton de confirmation visible



                }
                CloseDialog();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Demo", ex.Message, "OK");
            }
        }



        private async void OnConfirmButtonClicked(object sender, EventArgs e)
        {
            
        }







    }
}