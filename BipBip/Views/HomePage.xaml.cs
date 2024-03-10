using BipBip.Models;
using BipBip.Repositories;
using BipBip.Services;
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
    public partial class HomePage : ContentPage
    {
        private readonly DbService _dbService;
        private VehiculeRepo _repo;
        public HomePage()
        {
            InitializeComponent();
            setWelcomeMessage();
            _dbService = new DbService(DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3"));
            _repo = new VehiculeRepo(DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3"));
            LoadVehicules();
        }

        private void setWelcomeMessage()
        {
            welcomeLabel.Text = $"Bienvenue, {UserSession.FirstName} {UserSession.UserName} sur votre espace utilisateur votre identifiant est {UserSession.Id}";
        }

        private async void OnCTClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PreferencesPage());
        }

        private async void OnAddCarClicked(object sender, EventArgs e)
        {
            Vehicule vehicule = new Vehicule();
            await Navigation.PushAsync(new AddCarPage1(vehicule));
        }

        private async void OnATClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddRide());
        }

        private async void OnMPClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MessagePage());
        }
        private void Deconnexion(object sender, EventArgs e)
        {
            UserSession.EndSession();
            Navigation.PopToRootAsync();
        }

        private async void LoadVehicules()
        {
            var vehicule = await _repo.GetVehiculeByUserIdAsync(UserSession.Id);
            var count = 0;
            //affichage de toutes les vehic fel home page
            foreach (var v in vehicule)
            {
                count++;
                var vehiculelabel = new Label
                {
                    Text = $"marque: {v.Marque} modele: {v.Modele}",
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
                };
                vListLayout = this.FindByName<StackLayout>("vListLayout");
                vListLayout.Children.Add(vehiculelabel);
            }
            await DisplayAlert("taille", count.ToString(), "ok");
        }
    }
}