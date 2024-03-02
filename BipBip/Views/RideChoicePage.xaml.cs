using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BipBip.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RideChoicePage : ContentPage
	{
		public RideChoicePage ()
		{
			InitializeComponent ();
		}

		private async void OnFrameTapped(Object sender, EventArgs e) 
		{
			await Navigation.PushAsync(new RegidterPage ());
		}
	}
}