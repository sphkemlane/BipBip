using BipBip.Models;
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
    public partial class DatePickerPage : ContentPage
    {
        private Trip _trip;
        public DatePickerPage(Trip trip)
        {
            InitializeComponent();
            _trip = trip;
        }

        private async void OnContinueClicked(object sender, EventArgs e)
        {
            var chosenDate = datePicker.Date;

            _trip.DepartureTime = chosenDate;

            // Vérifier si la date choisie est dans le futur
            if (chosenDate < DateTime.Today)
            {
                // Si la date est passée, afficher un message et ne pas naviguer à la page suivante
                await DisplayAlert("Date invalide", "La date doit être dans le futur.", "OK");
            }
            else
            {
                // Si la date est valide, sauvegarder et naviguer à la page suivante
                App.Current.Properties["ChosenDate"] = chosenDate;
                await Navigation.PushAsync(new TimePickerPage(_trip));
            }
        }
    }
}