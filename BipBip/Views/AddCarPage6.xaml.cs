using BipBip.Models;
using BipBip.Repositories;
using BipBip.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BipBip.Views
{
    public partial class AddCarPage6 : ContentPage
    {
        readonly private Vehicule _vehicule;
        readonly private VehiculeRepo _vehiculesRepo;
        readonly private DbService _dbService;
       

        public AddCarPage6(Vehicule vehicule)
        {
            InitializeComponent();
            _vehicule = vehicule;
            //r�cup�rer le chemin de la base de donn�es 
            string db = DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3");
            //charger la base de donn�es : juste la table vehicule
             _vehiculesRepo = new VehiculeRepo(db);
            //_dbService = new DbService(db);
        }

       
        private async void OnFinishButtonClicked(object sender, EventArgs e)
        {
            string anneeImmValue = AnneeImmatriculationEntry.Text;

            // Stockez la valeur dans l'objet Vehicule
            _vehicule.AnneeImmatriculation = int.Parse(anneeImmValue);
            //mettre le propri�taire du vehicule
            //User owner = await GetUserByIdAsync(UserSession.Id);
            _vehicule.Owner = UserSession.Id;
           
            System.Diagnostics.Debug.WriteLine(" nummImmc: " + _vehicule.Immatriculation + " " + " type: " + _vehicule.Type + " " + " marque: " + _vehicule.Marque + " " + " Modele: " + _vehicule.Modele + " " + " couleur: " + _vehicule.Couleur + " " + " annee: " + _vehicule.AnneeImmatriculation + " ");
            await _vehiculesRepo.AddVehicule(_vehicule);
            await DisplayAlert("Success", "Votre v�hicule a �t� enregistr� avec succ�s", "OK");
            await Navigation.PushAsync(new HomePage());
            //await App.VehiculeRepo.AddVehicule(_vehicule);
            //await Navigation.PushAsync(new MainPage());
        }
        private void OnPreviousButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }


    }
}
