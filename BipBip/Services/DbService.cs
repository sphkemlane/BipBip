using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using BipBip.Models;
using System.Threading.Tasks;

namespace BipBip.Services
{
    internal class DbService
    {
        readonly SQLiteAsyncConnection _connection;

        public DbService(string Path)
        {
            _connection = new SQLiteAsyncConnection(Path);
            _connection.CreateTableAsync<User>().Wait();
            _connection.CreateTableAsync<Discussion>().Wait();
            _connection.CreateTableAsync<Message>().Wait();
        }

        public Task<List<User>> GetUserAsync()
        {
            return _connection.Table<User>().ToListAsync();
        }

        public Task<int> SaveUserAsync(User user)
        {
            if (user.Id == 0)
            {
                return _connection.InsertAsync(user);
            }
            else
            {
                return _connection.UpdateAsync(user);
            }
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            return _connection.Table<User>().Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        //--
        public async Task<string> GetUserEmailByUsernameAsync(string username)
        {
            var user = await _connection.Table<User>().Where(x => x.Name == username).FirstOrDefaultAsync();
            return user?.Email;
        }


        //Methode pour recuperer l'utilisateur par son id
        public Task<User> GetUserByIdAsync(int id)
        {
            return _connection.Table<User>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        //Methode pour recuperer le vehicule par son id
        public Task<Vehicule> GetVehiculeByIdAsync(int id)
        {
            return _connection.Table<Vehicule>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task DeleteAllTablesAsync()
        {
            // Liste de toutes les tables que vous souhaitez supprimer
            string[] tablesToDelete = { "Reservation", "Trip", "User", "Vehicule", "Message", "Discussion", "Rating" }; // Ajoutez ici toutes les tables que vous avez dans votre base de données

            foreach (var table in tablesToDelete)
            {
                string dropTableQuery = $"DROP TABLE IF EXISTS {table};";
                await _connection.ExecuteAsync(dropTableQuery);
            }
        }

        public Task<int> UpdateUserAsync(User user)
        {
            return _connection.UpdateAsync(user);
        }


        public Task<List<Discussion>> GetDiscussionsAsync(int userId)
        {
            return _connection.Table<Discussion>()
                              .Where(d => d.UserIdSender == userId || d.UserIdReceiver == userId)
                              .OrderByDescending(d => d.LastMessageDate)
                              .ToListAsync();
        }


        public Task<Discussion> GetDiscussionAsync(int userIdSender, int userIdReceiver)
        {
            return _connection.Table<Discussion>()
                              .Where(d => d.UserIdSender == userIdSender && d.UserIdReceiver == userIdReceiver)
                              .FirstOrDefaultAsync();
        }

        public Task<List<Discussion>> GetDiscussionsAsync()
        {
            return _connection.Table<Discussion>().ToListAsync();
        }

        public Task<int> SaveDiscussionAsync(Discussion discussion)
        {
            if (discussion.Id != 0)
            {
                return _connection.UpdateAsync(discussion);
            }
            else
            {
                return _connection.InsertAsync(discussion);
            }
        }

        public Task<int> UpdateDiscussionAsync(Discussion discussion)
        {
            return _connection.UpdateAsync(discussion);
        }


        public Task<int> SaveMessageAsync(Message message)
        {
            if (message.Id != 0)
            {
                return _connection.UpdateAsync(message);
            }
            else
            {
                return _connection.InsertAsync(message);
            }
        }

        public Task<List<Message>> GetMessagesForDiscussionAsync(int userIdSender, int userIdReceiver)
        {
            return _connection.Table<Message>()
                .Where(m => (m.UserIdSender == userIdSender && m.UserIdReceiver == userIdReceiver) ||
                            (m.UserIdSender == userIdReceiver && m.UserIdReceiver == userIdSender))
                .OrderBy(m => m.Date)
                .ToListAsync();
        }



    }
}
