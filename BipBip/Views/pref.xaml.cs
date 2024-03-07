using BipBip.ViewModels;
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
    public partial class PreferencesPage : ContentPage
    {
        public PreferencesPage()
        {
            InitializeComponent();
            BindingContext = new CarpoolPreferencesViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = BindingContext as CarpoolPreferencesViewModel;
            if (viewModel != null)
            {
                await viewModel.LoadPreferences(); // Charge les préférences au démarrage
            }
        }
    }
}