using BipBip.Models;
using BipBip.Repositories;
using BipBip.Services;
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
    public partial class SelectCarPage : ContentPage
    {
        private Trip _trip;
        private TripService _tripService;
        private VehiculeRepo _vehiculeRepo;
        private int _selectedIndex = -1;
        public SelectCarPage(Trip trip)
        {
            InitializeComponent();
            _trip = trip;

            _tripService = new TripService(DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3"));
            _vehiculeRepo = new VehiculeRepo(DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3"));

            LoadUserCars();
        }

        private async void LoadUserCars()
        {
            // Asynchrone récupération des véhicules de l'utilisateur
            var vehicles = await _vehiculeRepo.GetVehiculeByUserIdAsync(UserSession.Id);

            // Assurez-vous que la mise à jour de l'interface utilisateur se fait sur le thread principal
            Device.BeginInvokeOnMainThread(() =>
            {
                carsListView.ItemsSource = vehicles;
            });
        }


        private void OnCarSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listView = sender as ListView;
            if (_selectedIndex != -1)
            {
                // Réinitialiser la couleur de fond de l'élément précédemment sélectionné
                SetItemBackgroundColor(_selectedIndex, Color.Default);
            }

            if (e.SelectedItemIndex != -1)
            {
                // Mettre à jour la couleur de fond de l'élément actuellement sélectionné
                SetItemBackgroundColor(e.SelectedItemIndex, Color.LightGray);
                _selectedIndex = e.SelectedItemIndex;
            }
        }

        private void SetItemBackgroundColor(int index, Color color)
        {
            var item = carsListView.ItemsSource.Cast<Vehicule>().ElementAt(index);
            var cell = carsListView.TemplatedItems[index] as ViewCell;
            if (cell != null)
            {
                var frame = cell.View as Frame;
                if (frame != null)
                {
                    frame.BackgroundColor = color;
                }
            }
        }

        private async void OnContinueWithCarClicked(object sender, EventArgs e)
        {
            // Assurez-vous qu'une voiture est sélectionnée
            var selectedCar = carsListView.SelectedItem as Vehicule;
            if (selectedCar == null)
            {
                await DisplayAlert("Erreur", "Veuillez sélectionner une voiture.", "OK");
                return;
            }

            _trip.DriverId = UserSession.Id;
            _trip.VehicleId = selectedCar.Id;
            _tripService.AddTrip(_trip);

            System.Diagnostics.Debug.WriteLine(" " + _trip.DepartureTime + " " + " " + _trip.ArrivalTime + " " + " " + _trip.Departure + " " + " " + _trip.Arrival + " " + " " + _trip.AvailableSeats + " " + " " + _trip.Price + " ");
            await DisplayAlert("Success", "le trajet a été enregistré avec succès", "OK");
            await Navigation.PushAsync(new HomePageF());
        }
    }
}