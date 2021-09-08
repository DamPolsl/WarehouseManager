using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseAppManager.DAL.Repozytoria
{
    using Encje;
    using MySql.Data.MySqlClient;
    class RepozytoriumProdukt
    {
        private const string SELECT_PRODUKTY = "select * from `produkt`";
        private const string INSERT_PRODUKT = "INSERT INTO `produkt` (nazwa) VALUES ";
        public static List<Produkt> GetProdukty()
        {
            List<Produkt> produkty = new List<Produkt>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(SELECT_PRODUKTY, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read()) produkty.Add(new Produkt(reader));
                connection.Close();
            }
            return produkty;
        }
        public static bool DodajProdukt(Produkt produkt)
        {
            bool success = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{INSERT_PRODUKT} ('{produkt.Nazwa}')", connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) success = true;
                produkt.Id = (int)command.LastInsertedId;
                connection.Close();
            }
            return success;
        }
    }
}
