using App_git.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace App_git.Views
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