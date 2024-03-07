using BipBip.Models;
using System;
using Xamarin.Forms;

namespace BipBip.Views
{
    public partial class AddCarPage5 : ContentPage
    {
        private Vehicule _vehicule;
        public AddCarPage5(Vehicule vehicule)
        {
            InitializeComponent();
            _vehicule = vehicule;
        }
        private void OnNextButtonClicked(object sender, EventArgs e)
        {
            string couleurValue = CouleurVoitureEntry.Text;

            // Stockez la valeur dans l'objet Vehicule
            _vehicule.Couleur = couleurValue;

            Navigation.PushAsync(new AddCarPage6(_vehicule));
        }

        private void OnPreviousButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

    }
}
