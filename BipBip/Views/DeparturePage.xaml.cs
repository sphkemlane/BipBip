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
    public partial class DeparturePage : ContentPage
    {
        private Trip _trip;
        public DeparturePage(Trip trip)
        {
            InitializeComponent();
            BindingContext = new DepartureViewModel(map); // Assignation de votre ViewModel comme BindingContext
            _trip = trip;
        }


        //private async void OnContinueClicked(object sender, System.EventArgs e)
        //{

        //    await Navigation.PushAsync(new ArrivalPage());
        //}

        private async void OnContinueClicked(object sender, System.EventArgs e)
        {
            if (BindingContext is DepartureViewModel viewModel && viewModel.IsLocationSelected)
            {
                // Assurez-vous que viewModel.SearchText contient l'adresse mise à jour
                _trip.Departure = viewModel.SearchText;

                // Ensuite, naviguez à la page suivante, par exemple ArrivalPage
                // et passez l'objet _trip mis à jour
                await Navigation.PushAsync(new ArrivalPage(_trip));
            }
            else
            {
                // Si aucune adresse n'est sélectionnée ou si SearchText est vide,
                // affichez une alerte ou une indication à l'utilisateur.
                await DisplayAlert("Adresse manquante", "Veuillez entrer et sélectionner une adresse valide.", "OK");
            }
        }
    }
}