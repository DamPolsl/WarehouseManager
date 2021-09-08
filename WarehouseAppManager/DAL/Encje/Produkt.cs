using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseAppManager.DAL.Encje
{
    class Produkt
    {
        public int? Id { get; set; }
        public string Nazwa { get; set; }
        public Produkt(MySqlDataReader reader)
        {
            Id = int.Parse(reader["id"].ToString());
            Nazwa = reader["nazwa"].ToString();
        }
        public Produkt(string nazwa)
        {
            Id = null;
            Nazwa = nazwa;
        }
        public Produkt(Produkt produkt)
        {
            Id = produkt.Id;
            Nazwa = produkt.Nazwa;
        }
        public override bool Equals(object obj)
        {
            var produkt = obj as Produkt;
            if (produkt is null) return false;
            if (Nazwa != produkt.Nazwa) return false;
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
