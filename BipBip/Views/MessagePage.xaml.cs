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
    public partial class MessagePage : ContentPage
    {
        private Message _message;
        private MessageRepo _messageRepo;
        private DbService _dbService;

  

        public MessagePage()
        {
            InitializeComponent();
            string db = DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3");
            _messageRepo = new MessageRepo(db);
            _message = new Message(); 
        }

        private async void SendMessageOnClick(object sender, EventArgs e)
        {
            string Content = MessageEntry.Text;
            _message.Content = Content;
            _message.Date = DateTime.Now;
            _message.UserIdSender = UserSession.Id;
            await _messageRepo.AddMessage(_message);
            await DisplayAlert("Success", "Votre message a été envoyé avec succès", "OK");
            await Navigation.PushAsync(new HomePage());
        }
    }
}