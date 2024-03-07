using BipBip.Models;
using System;
using Xamarin.Forms;

namespace BipBip.Views
{
    public partial class AddCarPage3 : ContentPage
    {
        private Vehicule _vehicule;
        public AddCarPage3(Vehicule vehicule)
        {
            InitializeComponent();
            _vehicule = vehicule;
        }
        private void OnNextButtonClicked(object sender, EventArgs e)
        {
            string marqueValue = MarqueVoitureEntry.Text;

            // Stockez la valeur dans l'objet Vehicule
            _vehicule.Marque = marqueValue;

            Navigation.PushAsync(new AddCarPage4(_vehicule));
        }

        private void OnPreviousButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

    }
}
