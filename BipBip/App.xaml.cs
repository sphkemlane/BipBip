using BipBip.Services;
using BipBip.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BipBip
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            MainPage = new NavigationPage(new RegidterPage());
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
