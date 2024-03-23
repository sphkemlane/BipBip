using Xamarin.Forms;
using BipBip.Models;
using System.Collections.ObjectModel;
using BipBip.Services;
using System;
using System.Linq;
using Xamarin.Forms.Xaml;

namespace BipBip.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatDetailPage : ContentPage
    {
        public ObservableCollection<Message> Messages { get; set; }
        private readonly DbService _dbService;
        private readonly Discussion _discussion;

        public ChatDetailPage(Discussion discussion)
        {
            InitializeComponent();
            _discussion = discussion ?? throw new ArgumentNullException(nameof(discussion));
            Console.WriteLine("Discussion detail Id: " + discussion.Id);
            _dbService = new DbService(DependencyService.Get<IFileHelper>().GetLocalFilePath("Users.db3"));
            Messages = new ObservableCollection<Message>();
            BindingContext = this;

            // Load existing messages when the page is opened
            LoadMessagesAsync();
        }

        private async void LoadMessagesAsync()
        {
            var messages = await _dbService.GetMessagesForDiscussionAsync(_discussion.UserIdSender, _discussion.UserIdReceiver);
            foreach (var message in messages)
            {
                Messages.Add(message);
            }

            // Scroll to the latest message
            if (Messages.Any())
            {
                MessagesListView.ScrollTo(Messages.Last(), ScrollToPosition.End, animated: false);
            }
        }

        private async void OnSendMessage(object sender, EventArgs e)
        {
            var content = MessageEntry.Text?.Trim();
            if (!string.IsNullOrEmpty(content))
            {
                int receiverId = _discussion.UserIdReceiver != UserSession.Id ? _discussion.UserIdReceiver : _discussion.UserIdSender;
                var message = new Message
                {
                    Date = DateTime.UtcNow,
                    Content = content,
                    Status = "Sent",
                    UserIdSender = UserSession.Id,
                    UserIdReceiver = receiverId
                };

                Messages.Add(message);
                await _dbService.SaveMessageAsync(message);

                // Update discussion last message details
                _discussion.LastMessageDate = message.Date;
                Console.WriteLine("Discussion last message: " + _discussion.LastMessageDate);

                _discussion.LastMessagePreview = message.Content;
                Console.WriteLine("Discussion message preview: " + _discussion.LastMessagePreview);

                // Save or update the discussion
                var updateResult = await _dbService.UpdateDiscussionAsync(_discussion);

                if (updateResult == 1) // SQLite updates return the number of rows updated
                {
                    Console.WriteLine("Discussion updated successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to update discussion.");
                }

                MessageEntry.Text = string.Empty; // Clear the entry after sending
                MessagesListView.ScrollTo(message, ScrollToPosition.End, animated: true);
            }
        }
    }
}
