using BipBip.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace BipBip.ViewModels
{
    public class DepartureViewModel : INotifyPropertyChanged
    {
        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged();
            }
        }

        public Map Map { get; set; } // Propriété pour stocker la référence à la carte


        // Commande exécutée lors de la recherche
        public ICommand SearchCommand { get; protected set; }

        // Commande pour continuer après la sélection de l'adresse
        //public ICommand ContinueCommand { get; protected set; }

        // Indique si un emplacement est sélectionné
        public bool IsLocationSelected { get; private set; }

        public DepartureViewModel(Map map)
        {
            Map = map;
            SearchCommand = new Command(OnSearch);
            //ContinueCommand = new Command(OnContinue, () => IsLocationSelected);
        }

        private async void OnSearch()
        {
            // Implémentez la logique pour rechercher l'adresse et déplacer la carte.
            // Utilisez une API comme Google Places API pour obtenir des suggestions d'adresses.
            // Vérifiez si le texte de recherche n'est pas vide
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                try
                {
                    var geoCoder = new Geocoder();
                    var positions = await geoCoder.GetPositionsForAddressAsync(SearchText);

                    // Récupérez la première position (latitude et longitude) de la liste des positions
                    var position = positions.FirstOrDefault();

                    if (position != null)
                    {
                        // Effacez les anciens marqueurs
                        Map.Pins.Clear();



                        // Créez un nouveau marqueur à la position trouvée
                        var pin = new Pin
                        {
                            Label = "Adresse sélectionnée",
                            Position = new Position(position.Latitude, position.Longitude),
                            Type = PinType.Place
                        };

                        // Ajoutez le marqueur à la carte
                        Map.Pins.Add(pin);

                        // Centrez la carte sur la position trouvée
                        Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromKilometers(1)));

                        // Indiquez que l'emplacement est sélectionné
                        IsLocationSelected = true;
                        OnPropertyChanged(nameof(IsLocationSelected));
                    }
                    else
                    {
                        // Gérer le cas où aucune position n'est trouvée pour l'adresse saisie
                        // Peut-être afficher un message d'erreur à l'utilisateur
                    }
                }
                catch (Exception ex)
                {
                    // Gérer les exceptions, telles que les problèmes de réseau ou les erreurs de géocodage
                }
            }

        }

        //private void OnContinue()
        //{
        //    // Navigation vers la prochaine page du processus

        //}

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}