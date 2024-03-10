using SQLite;
using System;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using BipBip.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BipBip.Repositories
{
    class MessageRepo
    {
        readonly SQLiteAsyncConnection _connection;
        public string StatusMessage { get; set; }
        public MessageRepo(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath);
            _connection.CreateTableAsync<Message>().Wait();
        }


        public async Task AddMessage(Message message)
        {
            try
            {

                await _connection.InsertAsync(new Message
                {
                    Date = message.Date,
                    Content = message.Content,
                    Status = message.Status,
                    UserIdSender = message.UserIdSender,
                    UserIdReceiver = message.UserIdReceiver


                });
                StatusMessage = $"message ajouté";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Erreur : {ex.Message}";
            }
        }

        public async Task<List<Message>> GetMessageAsync()
        {
            try
            {
                return await _connection.Table<Message>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Erreur : {ex.Message}";
            }
            return new List<Message>();
        }

        //methode pour recuperer les message recus d'un utilisateur connecte
        public async Task<List<Message>> GetMessageByUserIdAsync(int id)
        {
            return await _connection.Table<Message>().Where(x => x.UserIdReceiver == id).ToListAsync();
        }

        //methode pour recuperer les message envoyes par un utilisateur connecte
        public async Task<List<Message>> GetMessageSentByUserIdAsync(int id)
        {
            return await _connection.Table<Message>().Where(x => x.UserIdSender == id).ToListAsync();
        }
    }
}