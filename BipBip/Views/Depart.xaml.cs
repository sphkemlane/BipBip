using BipBip.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BipBip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Depart : ContentView
    {
        private readonly VilleService _service = new VilleService();
        public ObservableCollection<string> Villes { get; set; }
        public string SelectedVille { get; set; }

        public Depart()
        {
            InitializeComponent();
            Console.WriteLine("hello");
            Villes = new ObservableCollection<string>();
            LoadVilles();
        }

        private async void LoadVilles()
        {
            var villes = await _service.ChargerVilles();
            foreach (var ville in villes)
            {
                Villes.Add(ville);
                Console.WriteLine(ville);
                Console.WriteLine("hello");
            }
        }

        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            SelectedVille = picker.SelectedItem.ToString();
            // Vous pouvez faire quelque chose avec la ville sélectionnée ici
        }
    }
}