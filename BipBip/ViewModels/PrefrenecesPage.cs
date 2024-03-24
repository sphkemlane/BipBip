using BipBip.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.IO;
using System.Threading.Tasks;
using System.ComponentModel;
using BipBip.Views; // Assurez-vous d'inclure cet espace de noms pour INotifyPropertyChanged

namespace BipBip.ViewModels
{
    public class CarpoolPreferencesViewModel : BindableObject, INotifyPropertyChanged // Ajoutez INotifyPropertyChanged si nécessaire
    {
        // Événement PropertyChanged pour la notification de changement de propriété
        public event PropertyChangedEventHandler PropertyChanged;

        // Propriétés privées
        private bool _smoking;
        private bool _music;
        private bool _conversation;
        private bool _eating;

        // Commande
        public ICommand SavePreferencesCommand { get; private set; }

        public CarpoolPreferencesViewModel()
        {
            SavePreferencesCommand = new Command(async () => await SavePreferences());
        }

        // Propriétés publiques avec notification de changement
        public bool Smoking
        {
            get => _smoking;
            set
            {
                if (_smoking != value)
                {
                    _smoking = value;
                    OnPropertyChanged(nameof(Smoking));
                }
            }
        }

        public bool Music
        {
            get => _music;
            set
            {
                if (_music != value)
                {
                    _music = value;
                    OnPropertyChanged(nameof(Music));
                }
            }
        }

        public bool Conversation
        {
            get => _conversation;
            set
            {
                if (_conversation != value)
                {
                    _conversation = value;
                    OnPropertyChanged(nameof(Conversation));
                }
            }
        }

        public bool Eating
        {
            get => _eating;
            set
            {
                if (_eating != value)
                {
                    _eating = value;
                    OnPropertyChanged(nameof(Eating));
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task SavePreferences()
        {
            // Logique pour sauvegarder les préférences
            Debug.WriteLine("Sauvegarde des préférences");
            Debug.WriteLine("Tentative de sauvegarde des préférences.");


            var carpoolPreferences = new CarpoolPreference
            {
                Smoking = this.Smoking,
                Music = this.Music,
                Conversation = this.Conversation,
                Eating = this.Eating
            };

            try
            {
                var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CarpoolPreferences.db3");
                var db = new SQLiteAsyncConnection(dbPath);
                await db.CreateTableAsync<CarpoolPreference>();
                await db.InsertOrReplaceAsync(carpoolPreferences);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur lors de la sauvegarde des préférences : {ex.Message}");
            }
        }
        public async Task LoadPreferences()
        {
            try
            {
                var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CarpoolPreferences.db3");
                var db = new SQLiteAsyncConnection(dbPath);

                // Supposons que vous ayez un seul enregistrement de préférences, récupérez-le.
                var preferences = await db.Table<CarpoolPreference>().FirstOrDefaultAsync();
                if (preferences != null)
                {
                    Smoking = preferences.Smoking;
                    Music = preferences.Music;
                    Conversation = preferences.Conversation;
                    Eating = preferences.Eating;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur lors du chargement des préférences : {ex.Message}");
            }
        }

    }

}


