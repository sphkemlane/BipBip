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


    }
}
