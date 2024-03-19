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
            Trip _trip = new Trip();
            // Effectuez la redirection vers la page AddRide
            await Navigation.PushAsync(new DatePickerPage(_trip));
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
            await Navigation.PushAsync(new MessagePage());
        }

        private async void OnSearchButtonClicked(object sender, EventArgs e)
        {
            // Récupérer les valeurs des contrôles d'entrée
            string departure = DepartureEntry.Text;
            string destination = DestinationEntry.Text;
            DateTime selectedDate = DatePicker.Date;
            Console.WriteLine("date: " + selectedDate);
            int numberOfPersons = Convert.ToInt32(PassengerCountEntry.Text);


            Vehicule _vehicule = new Vehicule()
            {
                Immatriculation = "AA-123-BB",
                Modele = "Peugeot 208",
                Marque = "Peugeot",
                Type = "Voiture",
                Couleur = "Rouge",
                AnneeImmatriculation = 2015,
                Owner = 0
            };
            _vehiculesRepo.AddVehicule(_vehicule);

            /*Trip newTrip = new Trip
            {
                DriverId = 1,
                VehicleId = 1,
                Departure = "Lyon",
                Arrival = "Marseille",
                DepartureTime = DateTime.Now, 
                AvailableSeats = 3,
                Price = 25.0,
            };
            Trip newTrip1 = new Trip
            {
                DriverId = 1,
                VehicleId = 1,
                Departure = "Lyon",
                Arrival = "Marseille",
                DepartureTime = DateTime.Now.AddMinutes(45),
                AvailableSeats = 3,
                Price = 35.0,
            };
            _tripService.AddTrip(newTrip);
            _tripService.AddTrip(newTrip1);*/

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