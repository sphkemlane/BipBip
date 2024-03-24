using BipBip.Models;
using BipBip.Services;
using BipBip.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BipBip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {

        public ProfilePage()
        {
            InitializeComponent();
            setWelcomeMessage();
            NavigationPage.SetHasNavigationBar(this, false);


            //Affecter l'URI de l'image � la Source de l'�l�ment Image



            //-- 
            BindingContext = new ProfileViewModel(); // Assignation de votre ViewModel comme BindingContext

            MessagingCenter.Subscribe<AddBioPage, string>(this, "BioUpdated", (sender, bioText) =>
            {
                // Mettre � jour la bio sur la page de profil
                // Par exemple, en actualisant un label avec la nouvelle bio
                // bioLabel.Text = arg;
                Debug.WriteLine("R�ception du message BioUpdated avec le texte: " + bioText);

                var viewModel = BindingContext as ProfileViewModel;
                if (viewModel != null)
                {
                    viewModel.UserBio = bioText;
                    SessionService.UserBio = bioText;

                }
            });




        }






        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await LoadUserInformation();

            // Chargement de la biographie et de l'image de profil depuis SessionService
            if (!string.IsNullOrEmpty(SessionService.UserBio))
            {
                var viewModel = BindingContext as ProfileViewModel;
                if (viewModel != null)
                {
                    viewModel.UserBio = SessionService.UserBio;
                    // Mettez � jour l'interface utilisateur si n�cessaire, par exemple:
                    // bioLabel.Text = SessionService.UserBio;
                }
            }

            if (!string.IsNullOrEmpty(SessionService.UserProfileImage))
            {
                img.Source = ImageSource.FromFile(SessionService.UserProfileImage); // Chargez l'image directement � partir du chemin du fichier
            }




            // D�terminez le chemin de la base de donn�es
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Users.db3");
            var dbService = new DbService(dbPath);

            // Utilisez le nom d'utilisateur stock� dans la session pour r�cup�rer l'email
            string username = UserSession.UserName; // Assurez-vous que UserSession.UserName est correctement initialis�
            var email = await dbService.GetUserEmailByUsernameAsync(username);

            if (!string.IsNullOrEmpty(email))
            {
                // Mettez � jour l'interface utilisateur avec l'email r�cup�r�
                Device.BeginInvokeOnMainThread(() =>
                {
                    LabelEmail.Text = email;


                });
            }

        }

        private async Task LoadUserInformation()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Users.db3");
            var dbService = new DbService(dbPath);

            // Assumer que UserSession.Id contient l'ID de l'utilisateur actuellement connect�
            int userId = UserSession.Id;

            // Supposer que vous avez une m�thode pour r�cup�rer un utilisateur par son ID
            var user = await dbService.GetUserByIdAsync(userId);
            if (user != null)
            {
                // Mise � jour de l'interface utilisateur avec les informations charg�es
                Device.BeginInvokeOnMainThread(() =>
                {
                    LabelNom.Text = user.Name;
                    LabelPr�nom.Text = user.FirstName;
                    LabelTel.Text = user.phoneNumber; // Assurez-vous que vous avez un champ pour le t�l�phone
                                                      // Mettez � jour d'autres champs au besoin
                });
            }
            var email = await dbService.GetUserEmailByUsernameAsync(user.Name);

            if (!string.IsNullOrEmpty(email))
            {
                // Mettez � jour l'interface utilisateur avec l'email r�cup�r�
                Device.BeginInvokeOnMainThread(() =>
                {
                    LabelEmail.Text = email;
                });
            }
        }



        private void setWelcomeMessage()
        {
            welcomeLabel.Text = $" {UserSession.FirstName} ";
            LabelNom.Text = $" {UserSession.UserName} ";
            LabelPr�nom.Text = $" {UserSession.FirstName} ";
            LabelEmail.Text = $"   ";
        }

        //Button revenir en arri�re
        public async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        //Photo




        // Naviguer vers la page d'ajout de voiture
        private async void OnAddCarButtonClicked(object sender, EventArgs e)
        {
            Vehicule _vehicule = new Vehicule();

            await Navigation.PushAsync(new AddCarPage1(_vehicule));
        }

        // Naviguer vers la page des pr�f�rences
        private async void OnPreferencesButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PreferencesPage());
        }


        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            // Assumer que UserSession.Id contient l'ID de l'utilisateur actuellement connect�
            int userId = UserSession.Id; // Assurez-vous que cela est correctement initialis� ailleurs dans votre application

            // R�cup�rer les valeurs saisies par l'utilisateur
            string name = LabelNom.Text;
            string firstName = LabelPr�nom.Text;
            string phoneNumber = LabelTel.Text;

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Users.db3");
            var dbService = new DbService(dbPath);

            // Appeler la m�thode UpdateUserInformationAsync pour mettre � jour l'utilisateur
            bool wasUpdateSuccessful = await dbService.UpdateUserInformationAsync(userId, name, firstName, phoneNumber);

            // Afficher un message � l'utilisateur pour indiquer si la mise � jour a r�ussi ou non
            if (wasUpdateSuccessful)
            {
                // Mise � jour r�ussie, afficher un message ou naviguer vers une autre page
                await DisplayAlert("Succ�s", "Votre profil a �t� mis � jour avec succ�s.", "OK");

            }
            else
            {
                // Mise � jour �chou�e, afficher un message d'erreur
                await DisplayAlert("Erreur", "La mise � jour du profil a �chou�. Veuillez r�essayer.", "OK");
            }
        }


        //AddPHOTO

        private async void btn_Clicked(object sender, EventArgs e)
        {
            double _scale = 0;
            _scale = gridImageOption.Scale;
            sl.IsEnabled = false;
            await gridImageOption.ScaleTo(0, 500);
            gridImageOption.IsVisible = true;
            await gridImageOption.ScaleTo(_scale, 300);
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            CloseDialog();
        }

        private void CloseDialog()
        {
            sl.IsEnabled = true;
            gridImageOption.IsVisible = false;
        }


        private async Task<string> SaveImageToFile(Stream imageStream)
        {
            var fileName = $"{Guid.NewGuid()}.jpg";
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await imageStream.CopyToAsync(fileStream);
            }

            return filePath;
        }




        private async void ctrlCamera_Tapped(object sender, EventArgs e)
        {
            try
            {
                var result = await MediaPicker.CapturePhotoAsync();

                if (result != null)
                {
                    var stream = await result.OpenReadAsync();

                    img.Source = ImageSource.FromStream(() => stream);
                    //SessionService.UserProfileImage = img.Source;

                    var filePath = await SaveImageToFile(stream);
                    SessionService.UserProfileImage = filePath; // Assumez que vous avez un UserProfileImagePath
                    img.Source = ImageSource.FromFile(filePath); // Chargez l'image depuis le chemin du fichier et affectez-la � la source de l'image

                    //confirmButton.IsVisible = true; // Rendre le bouton de confirmation visible
                }
                CloseDialog();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Demo", ex.Message, "OK");
            }
        }

        private async void ctrlGallery_Tapped(object sender, EventArgs e)
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Please pick a photo"
                });

                if (result != null)
                {
                    var stream = await result.OpenReadAsync();

                    img.Source = ImageSource.FromStream(() => stream);
                    //confirmButton.IsVisible = true; // Rendre le bouton de confirmation visible
                    //SessionService.UserProfileImage = img.Source;
                    var filePath = await SaveImageToFile(stream);
                    SessionService.UserProfileImage = filePath; // Assumez que vous avez un UserProfileImagePath
                    img.Source = ImageSource.FromFile(filePath); // Chargez l'image depuis le chemin du fichier et affectez-la � la source de l'image


                }
                CloseDialog();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Demo", ex.Message, "OK");
            }
        }



        //AddPhoto
    }
}