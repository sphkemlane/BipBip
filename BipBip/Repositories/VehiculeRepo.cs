using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using BipBip.Models;
using SQLite;
using System.Threading.Tasks;

namespace BipBip.Repositories
{
    public class VehiculeRepo
    {
        SQLiteAsyncConnection _connection;

        public string statusMessage { get; set; }
        public VehiculeRepo(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath);
            _connection.CreateTableAsync<Vehicule>().Wait();
        }

        public async Task AddVehicule(Vehicule vehicule)
        {
            try
            {
                await _connection.InsertAsync(new Vehicule
                {
                    Immatriculation = vehicule.Immatriculation,
                    Type = vehicule.Type,
                    Marque = vehicule.Marque,
                    Modele = vehicule.Modele,
                    Couleur = vehicule.Couleur,
                    AnneeImmatriculation = vehicule.AnneeImmatriculation

                });
                statusMessage = $"{vehicule.Immatriculation} ajouté";
            }
            catch (Exception ex)
            {
                statusMessage = $"Erreur : {ex.Message}";
            }
        }

        public async Task<List<Vehicule>> GetVehiculesAsync()
        {
            try
            {
                return await _connection.Table<Vehicule>().ToListAsync();
            }
            catch (Exception ex)
            {
                statusMessage = $"Erreur : {ex.Message}";
            }
            return new List<Vehicule>();
        }

        public async Task<List<Vehicule>> GetVehiculeByUserIdAsync(int id)
        {
            return await _connection.Table<Vehicule>().Where(x => x.Owner == id).ToListAsync();
        }
    }
}
