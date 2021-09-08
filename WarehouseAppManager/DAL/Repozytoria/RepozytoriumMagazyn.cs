using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseAppManager.DAL.Repozytoria
{
    using Encje;
    using MySql.Data.MySqlClient;
    class RepozytoriumMagazyn
    {
        private const string SELECT_MAGAZYNY = "SELECT * FROM magazyn";

        public static List<Magazyn> GetMagazyny()
        {
            List<Magazyn> magazyny = new List<Magazyn>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(SELECT_MAGAZYNY, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read()) magazyny.Add(new Magazyn(reader));
            }
            return magazyny;
        }
    }
}
