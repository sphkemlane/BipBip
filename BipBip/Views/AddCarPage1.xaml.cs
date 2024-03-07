using BipBip.Models;
using System;
using Xamarin.Forms;

namespace BipBip.Views
{
    public partial class AddCarPage1 : ContentPage
    {
        private Vehicule _vehicule;
        public AddCarPage1(Vehicule vehicule)
        {
            InitializeComponent();
            _vehicule = vehicule;
        }
        private void OnNextButtonClicked(object sender, EventArgs e)
        {
            string immatriculationValue = ImmatriculationEntry.Text;
                                        
            // Stockez la valeur dans l'objet Vehicule
            _vehicule.Immatriculation = immatriculationValue;

            Navigation.PushAsync(new AddCarPage2(_vehicule));
        }

        private void OnEscapeButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
