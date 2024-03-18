using BipBip.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BipBip.ViewModels
{
    public class ProfileViewModel : BaseViewModel // Héritage de BaseViewModel pour la gestion des propriétés et des notifications
    {
        public ICommand AddBioCommand { get; }
        //public ICommand ChoosePreferencesCommand { get; }

        public ICommand AddPhotoCommand { get; }

        public ProfileViewModel()
        {
            AddBioCommand = new Command(async () => await ExecuteAddBio());
            //ChoosePreferencesCommand = new Command(async () => await ExecuteChoosePreferences());
            AddPhotoCommand = new Command(async () => await ExecuteAddPhoto());
        }

        public async Task ExecuteAddBio()
        {
            var bioPage = new AddBioPage();
            await Application.Current.MainPage.Navigation.PushModalAsync(bioPage);
        }

        public async Task ExecuteAddPhoto()
        {
            var PhotoPage = new UploadImage();
            await Application.Current.MainPage.Navigation.PushModalAsync(PhotoPage);
        }

        private string _userBio;
        public string UserBio
        {
            get => _userBio;
            set
            {
                if (_userBio != value)
                {
                    _userBio = value;

                    OnPropertyChanged(nameof(UserBio)); // Ceci notifie la vue que la valeur a changé
                }
            }
        }


        //----------




    }
}
