using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WarehouseAppManager.DAL.Encje
{
    class Magazyn
    {
        //id, nazwa, id_lokalizacji
        public int? Id { get; set; }
        public string Nazwa { get; set; }
        public int? IdLokalizacji { get; set; }

        public Magazyn(MySqlDataReader reader)
        {
            Id = int.Parse(reader["id"].ToString());
            Nazwa = reader["nazwa"].ToString();
            IdLokalizacji = int.Parse(reader["id_lokalizacji"].ToString());
        }
    }
}
