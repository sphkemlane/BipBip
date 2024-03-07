using BipBip.Models;
using System;
using Xamarin.Forms;

namespace BipBip.Views
{
    public partial class AddCarPage4 : ContentPage
    {
        private Vehicule _vehicule;
        public AddCarPage4(Vehicule vehicule)
        {
            InitializeComponent();
            _vehicule = vehicule;
        }
        private void OnNextButtonClicked(object sender, EventArgs e)
        {
            string modeleValue = ModeleVoitureEntry.Text;

            // Stockez la valeur dans l'objet Vehicule
            _vehicule.Modele = modeleValue;

            Navigation.PushAsync(new AddCarPage5(_vehicule));
        }

        private void OnPreviousButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

    }
}
