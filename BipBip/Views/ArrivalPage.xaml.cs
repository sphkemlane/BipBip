using BipBip.Models;
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
    public partial class ArrivalPage : ContentPage
    {
        private Trip _trip;

        public ArrivalPage(Trip trip)
        {
            InitializeComponent();
            /*BindingContext = new ArrivalViewModel(map);*/ // Assignation de votre ViewModel comme BindingContext
            _trip = trip;
            var viewModel = new ArrivalViewModel(map);
            BindingContext = viewModel;
            NavigationPage.SetHasNavigationBar(this, false);

        }

        //private async void OnContinueClicked(object sender, System.EventArgs e)
        //{

        //    await Navigation.PushAsync(new PassengerCountPage());
        //}
        private async void OnContinueClicked(object sender, EventArgs e)
        {
            if (BindingContext is ArrivalViewModel viewModel && viewModel.IsLocationSelected)
            {
                // Mettre à jour l'objet _trip avec l'adresse de destination
                _trip.Arrival = viewModel.SearchText;


                Application.Current.Properties["ArrivalAddress"] = _trip.Arrival;
                await Application.Current.SavePropertiesAsync();

                // Passez à la page suivante, en passant l'objet _trip pour maintenir l'état
                await Navigation.PushAsync(new PassengerCountPage(_trip));
            }
            else
            {
                // Si aucune adresse n'est sélectionnée ou si SearchText est vide,
                // affichez une alerte à l'utilisateur.
                await DisplayAlert("Adresse manquante", "Veuillez entrer et sélectionner une adresse valide.", "OK");
            }
        }
    }
}