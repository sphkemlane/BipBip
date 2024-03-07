using BipBip.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace BipBip.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}