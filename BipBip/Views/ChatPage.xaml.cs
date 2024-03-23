using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BipBip.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BipBip.Services;
using System;

namespace BipBip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        public ObservableCollection<Discussion> Discussions { get; set; }
        private readonly DbService _dbService;


        public ChatPage()
        {
            InitializeComponent();
            _dbService = new DbService(DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3"));
            Discussions = new ObservableCollection<Discussion>();
            BindingContext = this;
            LoadDiscussionsAsync(); // Assuming UserSession.Id holds the current user's ID
        }

       

        private async Task LoadDiscussionsAsync()
        {
            // Logic to load discussions from the SQLite database
            // For each discussion associated with the userId, add to the Discussions collection
            var discussions = await _dbService.GetDiscussionsAsync(UserSession.Id);
            Console.WriteLine("Loading discussions : ..... : " + discussions.Count);
            foreach (var discussion in discussions)
            {
                int receiverId = discussion.UserIdReceiver != UserSession.Id ? discussion.UserIdReceiver : discussion.UserIdSender;
                discussion.UserName = _dbService.GetUserByIdAsync(receiverId).Result.FirstName;
                Discussions.Add(discussion);
            }
        }

        private async void OnDiscussionSelected(object sender, SelectionChangedEventArgs e)
        {
            var selectedDiscussion = e.CurrentSelection.FirstOrDefault() as Discussion;
            if (selectedDiscussion != null)
            {
                // Navigate to the chat detail page for the selected discussion
                await Navigation.PushAsync(new ChatDetailPage(selectedDiscussion));
            }

        // Deselect the item
        ((CollectionView)sender).SelectedItem = null;
        }
    }
}
