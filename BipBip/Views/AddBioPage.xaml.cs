using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BipBip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBioPage : ContentPage
    {
        public AddBioPage()
        {
            InitializeComponent();
        }

        private void SaveBio_Clicked(object sender, EventArgs e)
        {
            var bioText = bioEditor.Text;
            // Vous pouvez ajouter ici la logique d'enregistrement de la bio.
            Debug.WriteLine("Envoi du message BioUpdated avec le texte: " + bioText);
            MessagingCenter.Send(this, "BioUpdated", bioText);
            Navigation.PopModalAsync();
        }

    }
}