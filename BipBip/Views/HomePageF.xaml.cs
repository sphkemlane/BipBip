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
    public partial class HomePageF : ContentPage
    {
        private readonly DbService _dbService;
        private readonly TripService _tripService;
        readonly private VehiculeRepo _vehiculesRepo;
        public HomePageF()
        {
            InitializeComponent();
            _dbService = new DbService(DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3"));
            _tripService = new TripService(DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3"));
            _vehiculesRepo = new VehiculeRepo(DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3"));
        }

        private async void OnPublierButtonClicked(object sender, EventArgs e)
        {
            int currentUserId = UserSession.Id;

    
            var vehicles = await _vehiculesRepo.GetVehiculeByUserIdAsync(currentUserId);
            
            if (vehicles.Any())
            {
                Trip _trip = new Trip();
                // Effectuez la redirection vers la page AddRide
                await Navigation.PushAsync(new DatePickerPage(_trip));
            }
            else {
                // L'utilisateur n'a pas de véhicule
                Vehicule _vehicule = new Vehicule();
                // Redirection vers AddCarPage1 pour ajouter un véhicule
                await Navigation.PushAsync(new AddCarPage1(_vehicule));
            }
        }
        

        private async void OnProfilButtonClicked(object sender, EventArgs e)
        {
            // Effectuez la redirection vers la page Profil
            await Navigation.PushAsync(new ProfilePage());
        }

        private async void OnMesTrajetsButtonClicked(object sender, EventArgs e)
        {
            // Effectuez la redirection vers la page MesTrajets
            await Navigation.PushAsync(new MesTrajets());
        }

        private async void OnMessagesButtonClicked(object sender, EventArgs e)
        {
            // Effectuez la redirection vers la page MesReservations
            await Navigation.PushAsync(new ChatPage());
        }

        private async void OnSearchButtonClicked(object sender, EventArgs e)
        {
            // Récupérer les valeurs des contrôles d'entrée
            string departure = DepartureEntry.Text;
            string destination = DestinationEntry.Text;
            DateTime selectedDate = DatePicker.Date;
            Console.WriteLine("date: " + selectedDate);
            int numberOfPersons = Convert.ToInt32(PassengerCountEntry.Text);

            List<Trip> matchingTrips = _tripService.SearchTrips(departure, destination, selectedDate, numberOfPersons);
            Console.WriteLine("matchingTrips: " + matchingTrips.Count);
            foreach (Trip trip in matchingTrips)
            {

                Console.WriteLine("price: " + trip.Price);

                trip.DriverName = _dbService.GetUserByIdAsync(trip.DriverId).Result.FirstName;
                trip.CarModel = _dbService.GetVehiculeByIdAsync(trip.VehicleId).Result.Modele;
            }
            // Naviguer vers la SearchResultsPage en passant les valeurs comme paramètres
            await Navigation.PushAsync(new SearchResultsPage(departure, destination, selectedDate, numberOfPersons, matchingTrips));
        }



        //deconnexion a mettre dans mon profil

        private void Deconnexion(object sender, EventArgs e)
        {
            UserSession.EndSession();
            Navigation.PopToRootAsync();
        }
    }
}