using BipBip.Models;
using System;
using Xamarin.Forms;

namespace BipBip.Views
{
    public partial class AddCarPage2 : ContentPage
    {
        private Vehicule _vehicule;
        public AddCarPage2(Vehicule vehicule)
        {
            InitializeComponent();
            _vehicule = vehicule;
        }
        private void OnNextButtonClicked(object sender, EventArgs e)
        {
            if (VoitureRadioButton.IsChecked)
            {
                _vehicule.Type = VoitureRadioButton.Value.ToString();
            }
            else if (ScooterRadioButton.IsChecked)
            {
                _vehicule.Type = ScooterRadioButton.Value.ToString();
            }
            Navigation.PushAsync(new AddCarPage3(_vehicule));
        }

        private void OnPreviousButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

    }
}
