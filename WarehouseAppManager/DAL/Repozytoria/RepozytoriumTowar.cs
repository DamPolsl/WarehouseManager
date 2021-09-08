using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseAppManager.DAL.Repozytoria
{
    using Encje;
    using MySql.Data.MySqlClient;

    class RepozytoriumTowar
    {
        private const string SELECT_TOWARY = "SELECT towar.*, produkt.nazwa FROM `towar`, `produkt` where towar.id_produktu like produkt.id";
        private const string INSERT_TOWAR = "INSERT INTO `towar` (`ilość`, `jednostka`, `regał`, `id_magazynu`, `id_produktu`) VALUES ";

        public static List<Towar> GetTowary()
        {
            List<Towar> towary = new List<Towar>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(SELECT_TOWARY, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read()) towary.Add(new Towar(reader));
                connection.Close();
            }
            return towary;
        }

        public static bool AddTowar(Towar towar)
        {
            bool success = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{INSERT_TOWAR} {towar.ToInsert()}", connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) success = true;
                connection.Close();
            }
            return success;
        }
        
        public static bool EdytujTowar(Towar towar)
        {
            bool success = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string query = $"update towar set ilość = {towar.Ilosc} where id_produktu = {towar.Id} " +
                    $"and regał like '{towar.Regal}' and id_magazynu = {towar.IdMagazynu}";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) success = true;
                connection.Close();
            }
            return success;
        }
        public static int OdejmijTowar(Towar towar)
        {
            int result = 0;
            using (var connection = DBConnection.Instance.Connection)
            {
                int nowa_ilosc = towar.Ilosc - towar.IloscDoKoszyka;
                if(nowa_ilosc == 0)
                {
                    //usun towar z tablicy
                    string query = $"delete from towar where id_produktu = {towar.Id} and regał like '{towar.Regal}' and id_magazynu = {towar.IdMagazynu}";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    connection.Open();
                    var n = command.ExecuteNonQuery();
                    if (n == 1) result = 1;
                    connection.Close();
                }
                else
                {
                    //zmien ilosc na nowa ilosc
                    string query = $"update towar set ilość = {nowa_ilosc} where id_produktu = {towar.Id} " +
                    $"and regał like '{towar.Regal}' and id_magazynu = {towar.IdMagazynu}";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    connection.Open();
                    var n = command.ExecuteNonQuery();
                    if (n == 1) result = 2;
                    connection.Close();
                }
            }
            return result;
        }
    }
}
