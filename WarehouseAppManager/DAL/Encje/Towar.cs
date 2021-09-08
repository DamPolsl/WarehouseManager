using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WarehouseAppManager.DAL.Encje
{
    class Towar
    {
        //id, ilosc, jednostka, nazwa, regał, id_magazynu
        public int? Id { get; set; }
        public int Ilosc { get; set; }
        public string Jednostka { get; set; }
        public string Nazwa { get; set; }
        public string Regal { get; set; }
        public int? IdMagazynu { get; set; }
        public int DodanaIlosc { get; set; }
        public int IloscDoKoszyka { get; set; }

        public Towar (MySqlDataReader reader)
        {
            Id = int.Parse(reader["id_produktu"].ToString());
            Ilosc = int.Parse(reader["ilość"].ToString());
            Jednostka = reader["jednostka"].ToString();
            Nazwa = reader["nazwa"].ToString();
            Regal = reader["regał"].ToString();
            IdMagazynu = int.Parse(reader["id_magazynu"].ToString());
            IloscDoKoszyka = 0;
        }
        public Towar (int ilosc, string jednostka, string nazwa, string regal, int? idmagazynu)
        {
            Id = null;
            Ilosc = ilosc;
            Jednostka = jednostka;
            Nazwa = nazwa;
            Regal = regal;
            IdMagazynu = idmagazynu;
            IloscDoKoszyka = 0;
        }
        public Towar (Towar towar)
        {
            Id = towar.Id;
            Ilosc = towar.Ilosc;
            Jednostka = towar.Jednostka;
            Nazwa = towar.Nazwa;
            Regal = towar.Regal;
            IdMagazynu = towar.IdMagazynu;
            IloscDoKoszyka = towar.IloscDoKoszyka;
        }
        public override string ToString()
        {
            return Nazwa.ToString();
        }
        public string ToInsert()
        {
            return $"('{Ilosc}', '{Jednostka}', '{Regal}', '{IdMagazynu}', '{Id}')";
        }
        public override bool Equals(object obj)
        {
            var towar = obj as Towar;
            if (towar is null) return false;
            if (Id != towar.Id) return false;
            if (Regal.ToLower() != towar.Regal.ToLower()) return false;
            if (IdMagazynu != towar.IdMagazynu) return false;
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
