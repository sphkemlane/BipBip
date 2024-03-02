using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace BipBip.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddRide : ContentPage
	{
		public AddRide ()
		{
			InitializeComponent ();
		}

        private async void OnFacebookTapped(Object sender, EventArgs e)
        {
            await Launcher.OpenAsync(new Uri("https://www.facebook.com/"));
        }

        private async void OnInstagramTapped(Object sender, EventArgs e)
        {
            await Launcher.OpenAsync(new Uri("https://www.instagram.com/"));
        }

        private async void OnTwitterTapped(Object sender, EventArgs e)
        {
            await Launcher.OpenAsync(new Uri("https://www.twitter.com/"));
        }
        
    }
}