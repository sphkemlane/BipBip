using BipBip.Models;
using BipBip.Repositories;
using System;
using Xamarin.Forms;

namespace BipBip.Views
{
    public partial class AddCarPage6 : ContentPage
    {
        private Vehicule _vehicule;
        public AddCarPage6(Vehicule vehicule)
        {
            InitializeComponent();
            _vehicule = vehicule;
        }
        private async void OnFinishButtonClicked(object sender, EventArgs e)
        {
            string anneeImmValue = AnneeImmatriculationEntry.Text;

            // Stockez la valeur dans l'objet Vehicule
            _vehicule.AnneeImmatriculation = int.Parse(anneeImmValue);
            System.Diagnostics.Debug.WriteLine(" nummImmc: " + _vehicule.Immatriculation + " " + " type: " + _vehicule.Type + " " + " marque: " + _vehicule.Marque + " " + " Modele: " + _vehicule.Modele + " " + " couleur: " + _vehicule.Couleur + " " + " annee: " + _vehicule.AnneeImmatriculation + " ");
            //await App.VehiculeRepo.AddVehicule(_vehicule);
            //await Navigation.PushAsync(new MainPage());
        }
        private void OnPreviousButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }


    }
}
