using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BipBip.Models
{
    [Table("Vehicule")]
    public class Vehicule
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(15), Unique, NotNull]
        public string Immatriculation { get; set; }
        public string Type { get; set; }
        public string Marque { get; set; }
        public string Modele { get; set; }
        public string Couleur { get; set; }
        public int AnneeImmatriculation { get; set; }
    }
}
