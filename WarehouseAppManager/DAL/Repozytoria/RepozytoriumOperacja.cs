using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseAppManager.DAL.Repozytoria
{
    using MySql.Data.MySqlClient;
    using Encje;
    class RepozytoriumOperacja
    {
        const string INSERT_OPERACJA = "insert into operacja (id_operacji, typ_operacji, data, id_pracownika) values ";
        const string INSERT_TOWAR_OPERACJI = "insert into towar_operacji (id_operacji, numer_pozycji, ilość, id_produktu, id_magazynu, regał) values ";
        public static int GetNextId()
        {
            using (var connection = DBConnection.Instance.Connection)
            {
                int max_id = 0;
                MySqlCommand command = new MySqlCommand($"select max(id_operacji) as max_id from operacja", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string s = reader["max_id"].ToString();
                    if (s != "")
                        max_id = int.Parse(reader["max_id"].ToString());
                }
                connection.Close();
                return max_id + 1;
            }
        }
        public static bool NowaOperacja(Operacja operacja)
        {
            bool success = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{INSERT_OPERACJA} {operacja.ToInsert()}", connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) success = true;
                connection.Close();
            }
            return success;
        }
        public static bool NowyTowarOperacji(Operacja operacja)
        {
            bool success = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{INSERT_TOWAR_OPERACJI} {operacja.TowarToInsert()}", connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) success = true;
                connection.Close();
            }
            return success;
        }
    }
}
