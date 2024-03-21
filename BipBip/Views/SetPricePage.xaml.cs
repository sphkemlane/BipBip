using BipBip.Models;
using BipBip.Repositories;
using BipBip.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BipBip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetPricePage : ContentPage
    {
        private Trip _trip;
        private TripRepo _tripRepo;
        private TripService _tripService;

        public SetPricePage(Trip trip)
        {
            InitializeComponent();
            _trip = trip;

            //récupérer le chemin de la base de données 
            string db = DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3");
            _tripService = new TripService(db); // Make sure this line is added to instantiate TripService

            _tripRepo = new TripRepo(db);
            NavigationPage.SetHasNavigationBar(this, false);


        }
        private void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            priceLabel.Text = $"{e.NewValue} €";
            _trip.Price = (int)e.NewValue; // Mise à jour du prix dans le modèle de données Trip

            App.Current.Properties["SelectedPrice"] = _trip.Price;
            _trip.DriverId = UserSession.Id;
            
            //_tripRepo.InsertTrip(_trip);
            _tripService.AddTrip(_trip);
        }
        private async void OnContinueClicked(object sender, EventArgs e)
        {
            
            System.Diagnostics.Debug.WriteLine(" " + _trip.DepartureTime + " " + " " + _trip.ArrivalTime + " " + " " + _trip.Departure + " " + " " + _trip.Arrival + " " + " " + _trip.AvailableSeats + " " + " " + _trip.Price + " ");
            await DisplayAlert("Success", "le trajet a été enregistré avec succès", "OK");
            await Navigation.PushAsync(new HomePageF());

        }
    }
}